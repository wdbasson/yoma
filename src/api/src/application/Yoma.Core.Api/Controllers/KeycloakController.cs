using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Mvc;
using FS.Keycloak.RestApiClient.Api;
using FS.Keycloak.RestApiClient.Client;
using FS.Keycloak.RestApiClient.Model;
using Microsoft.Extensions.Options;
using Yoma.Core.Domain.Core.Extensions;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Yoma.Core.Domain.Core.Models;
using Yoma.Core.Domain.Entity.Interfaces;
using Yoma.Core.Domain.Keycloak;
using Yoma.Core.Domain.Keycloak.Models;
using Yoma.Core.Domain.Entity.Models;
using Yoma.Core.Domain.Core;
using Yoma.Core.Domain.Lookups.Interfaces;

namespace Yoma.Core.Api.Controllers
{
    [Route("api/v1/keycloak")] //TODO: pending new keycloak instance (v3 route)
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class KeycloakController : Controller
    {
        #region Class Variables
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;
        private readonly KeycloakAuthenticationOptions _keycloakAuthenticationOptions;
        private readonly IUserService _userService;
        private readonly IGenderService _genderService;
        private readonly ICountryService _countryService;
        #endregion

        #region Constructors
        public KeycloakController(ILogger<KeycloakController> logger,
          IOptions<AppSettings> appSettings,
          IOptions<KeycloakAuthenticationOptions> keycloakAuthenticationOptions,
          IUserService userService,
          IGenderService genderService,
          ICountryService countryService)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _keycloakAuthenticationOptions = keycloakAuthenticationOptions.Value;
            _userService = userService;
            _genderService = genderService;
            _countryService = countryService;
        }
        #endregion

        #region Public Members
        [HttpPost("webhook")]
        public IActionResult ReceiveKeyCloakEvent([FromBody] WebhookRequest payload)
        {
            var authorized = false;

            try
            {
                if (payload == null)
                    throw new ArgumentNullException(nameof(payload));

                if (HttpContext == null)
                    throw new ArgumentNullException(nameof(HttpContext), $"{nameof(HttpContext)} is null");

                // basic authentication
                var authHeader = AuthenticationHeaderValue.Parse(HttpContext.Request.Headers["Authorization"]);

                if (authHeader.Parameter == null)
                    throw new ArgumentNullException(nameof(HttpContext), $"{nameof(HttpContext.Request.Headers)}.Authorization is null");

                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                var username = credentials[0];
                var password = credentials[1];

                if (username != _appSettings.WebhookAdminKeyCloak.Username || password != _appSettings.WebhookAdminKeyCloak.Password)
                    return StatusCode(StatusCodes.Status403Forbidden);

                authorized = true;
                return StatusCode(StatusCodes.Status200OK);
            }
            finally
            {
                Response.OnCompleted(async () =>
                {
                    if (!authorized) return;

                    _logger.LogInformation("Received Keycloak Event: " + JsonConvert.SerializeObject(payload, Formatting.Indented));

                    Enum.TryParse<WebhookRequestEventType>(payload.type, true, out var type);

                    switch (type)
                    {
                        case WebhookRequestEventType.Register:
                        case WebhookRequestEventType.UpdateProfile:
                            _logger.LogInformation($"{type} event processing");

                            await UpdateUserProfile(type, payload);

                            _logger.LogInformation($"{type} event processed");
                            break;

                        case WebhookRequestEventType.Login:
                            _logger.LogInformation($"{type} event processing");

                            await UpdateUserProfile(type, payload);

                            _logger.LogInformation($"{type} event processed");
                            break;

                        default:
                            _logger.LogInformation($"Unknown event type of '{payload.type}' receive. Processing skipped");
                            return;
                    }
                });
            }
        }
        #endregion

        #region Private Members
        private async Task UpdateUserProfile(WebhookRequestEventType type, WebhookRequest payload)
        {
            if (string.IsNullOrEmpty(payload?.details?.username))
            {
                _logger.LogError($"Webhook payload contains no associated Keycloak username");
                return;
            }

            var timeout = 15000;
            var startTime = DateTime.Now;
            UserRepresentation? kcUser = null;

            using var httpClient = new KeycloakHttpClient(_keycloakAuthenticationOptions.AuthServerUrl,
                _appSettings.AdminKeyCloak.Username, _appSettings.AdminKeyCloak.Password);
            using (var usersApi = ApiClientFactory.Create<UsersApi>(httpClient))
            {
                while (true)
                {
                    _logger.LogInformation($"Trying to find the Keycloak user with username '{payload?.details?.username}'");
                    kcUser = (await usersApi.GetUsersAsync(_keycloakAuthenticationOptions.Realm, username: payload?.details?.username, exact: true)).SingleOrDefault();
                    if (kcUser != null)
                    {
                        _logger.LogInformation($"KeyCloak user with username '{payload?.details?.username}' found");
                        break;
                    }

                    if ((DateTime.Now - startTime).TotalMilliseconds >= timeout) break;

                    _logger.LogInformation($"KeyCloak user with username '{payload?.details?.username}' not found, sleep for 1000ms and try again");
                    Thread.Sleep(1000);
                }
            }

            if (kcUser == null)
            {
                _logger.LogError($"Failed to retrieve the Keycloak user with username '{payload?.details.username}'");
                return;
            }

            var user = _userService.GetByEmailOrNull(kcUser.Username);

            switch (type)
            {
                case WebhookRequestEventType.Register:
                case WebhookRequestEventType.UpdateProfile:
                    if (user == null)
                    {
                        if (type == WebhookRequestEventType.UpdateProfile)
                        {
                            _logger.LogError($"{type}: Failed to retrieve the Yoma user with username '{payload?.details.username}'");
                            return;
                        }
                        user = new User { Id = Guid.NewGuid() };
                    }

                    user.Email = kcUser.Email.Trim();
                    user.FirstName = kcUser.FirstName.Trim();
                    user.Surname = kcUser.LastName.Trim();
                    user.EmailConfirmed = kcUser.EmailVerified.HasValue && kcUser.EmailVerified.Value;
                    user.PhoneNumber = kcUser.Attributes[CustomAttributes.PhoneNumber.ToDescription()].FirstOrDefault()?.Trim();

                    var sGender = kcUser.Attributes[CustomAttributes.Gender.ToDescription()].FirstOrDefault()?.Trim();
                    if (!string.IsNullOrEmpty(sGender))
                    {
                        var gender = _genderService.GetByNameOrNull(sGender);

                        if (gender == null)
                            _logger.LogError($"Failed to parse Keycloak '{CustomAttributes.Gender}' with value '{sGender}'");
                        else
                            user.GenderId = gender.Id;
                    }

                    var sCountryOfOrigin = kcUser.Attributes[CustomAttributes.CountryOfOrigin.ToDescription()].FirstOrDefault()?.Trim();
                    if (!string.IsNullOrEmpty(sCountryOfOrigin))
                    {
                        var country = _countryService.GetByNameOrNull(sCountryOfOrigin);

                        if (country == null)
                            _logger.LogError($"Failed to parse Keycloak '{CustomAttributes.CountryOfOrigin}' with value '{sCountryOfOrigin}'");
                        else
                            user.CountryId = country.Id;
                    }

                    var sCountryOfResidence = kcUser.Attributes[CustomAttributes.CountryOfResidence.ToDescription()].FirstOrDefault()?.Trim();
                    if (!string.IsNullOrEmpty(sCountryOfResidence))
                    {
                        var country = _countryService.GetByNameOrNull(sCountryOfResidence);

                        if (country == null)
                            _logger.LogError($"Failed to parse Keycloak '{CustomAttributes.CountryOfOrigin}' with value '{sCountryOfResidence}'");
                        else
                            user.CountryOfResidenceId = country.Id;
                    }

                    var sDateOfBirth = kcUser.Attributes[CustomAttributes.DateOfBirth.ToDescription()].FirstOrDefault()?.Trim();
                    if (!string.IsNullOrEmpty(sDateOfBirth))
                    {
                        if (!DateTime.TryParse(sDateOfBirth, out var dateOfBirth))
                            _logger.LogError($"Failed to parse Keycloak '{CustomAttributes.DateOfBirth}' with value '{sDateOfBirth}'");
                        else
                            user.DateOfBirth = dateOfBirth;
                    }

                    if (type == WebhookRequestEventType.UpdateProfile) break;

                    //add newly registered user to the default "User" role
                    List<RoleRepresentation>? kcRoles = null;
                    using (var rolesApi = ApiClientFactory.Create<RoleContainerApi>(httpClient))
                    {
                        kcRoles = rolesApi.GetRoles(_keycloakAuthenticationOptions.Realm);
                    }

                    using (var rolesMapperApi = ApiClientFactory.Create<RoleMapperApi>(httpClient))
                    {
                        rolesMapperApi.PostUsersRoleMappingsRealmById(_keycloakAuthenticationOptions.Realm, kcUser.Id,
                          kcRoles.Where(o => string.Equals(o.Name, Constants.Role_User, StringComparison.InvariantCultureIgnoreCase)).ToList());
                    }

                    //TODO: AriesCloudApi tenant / wallet creation
                    break;

                case WebhookRequestEventType.Login:
                    if (user == null)
                    {
                        _logger.LogError($"{type}: Failed to retrieve the Yoma user with username '{payload?.details.username}'");
                        return;
                    }

                    //updated here after email verification a login event is raised
                    user.EmailConfirmed = kcUser.EmailVerified.HasValue && kcUser.EmailVerified.Value;
                    user.DateLastLogin = DateTime.Now;

                    break;

                default: //event not supported
                    _logger.LogError($"Failed to retrieve the Keycloak user with username '{payload?.details.username}'");
                    return;
            }

            user.ExternalId = Guid.Parse(kcUser.Id);

            await _userService.Upsert(user);
        }
    }
    #endregion
}

