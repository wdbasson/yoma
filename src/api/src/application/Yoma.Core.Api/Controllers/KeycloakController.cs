using Microsoft.AspNetCore.Mvc;
using Yoma.Core.Domain.Entity.Interfaces;
using Yoma.Core.Domain.Entity.Models;
using Yoma.Core.Domain.Core;
using Yoma.Core.Domain.Lookups.Interfaces;
using Yoma.Core.Domain.Entity.Extensions;
using Microsoft.AspNetCore.Authorization;
using Yoma.Core.Domain.Keycloak.Models;
using Yoma.Core.Infrastructure.Keycloak;
using Yoma.Core.Domain.Keycloak.Interfaces;

namespace Yoma.Core.Api.Controllers
{
    [Route("api/v1/keycloak")] //TODO: pending new keycloak instance (v3 route)
    [ApiController]
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class KeycloakController : Controller
    {
        #region Class Variables
        private readonly ILogger _logger;
        private readonly IKeycloakClient _keycloakClient;
        private readonly IUserService _userService;
        private readonly IGenderService _genderService;
        private readonly ICountryService _countryService;
        #endregion

        #region Constructors
        public KeycloakController(ILogger<KeycloakController> logger,
          IKeycloakClientFactory keycloakClientFactory,
          IUserService userService,
          IGenderService genderService,
          ICountryService countryService)
        {
            _logger = logger;
            _keycloakClient = keycloakClientFactory.CreateClient();
            _userService = userService;
            _genderService = genderService;
            _countryService = countryService;
        }
        #endregion

        #region Public Members
        [HttpPost("webhook")]
        public IActionResult ReceiveKeyCloakEvent([FromBody] KeycloakWebhookRequest payload)
        {
            var authorized = false;

            try
            {
                if (payload == null)
                    throw new ArgumentNullException(nameof(payload));

                authorized = _keycloakClient.AuthenticateWebhook(HttpContext);

                return authorized ? StatusCode(StatusCodes.Status200OK) : StatusCode(StatusCodes.Status403Forbidden);
            }
            finally
            {
                Response.OnCompleted(async () =>
                {
                    if (!authorized) return;

                    var sType = payload.type;
                    _logger.LogInformation($"{sType} event received");

                    Enum.TryParse<WebhookRequestEventType>(sType, true, out var type);

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
                            _logger.LogInformation($"Unknown event type of '{sType}' receive. Processing skipped");
                            return;
                    }
                });
            }
        }
        #endregion

        #region Private Members
        private async Task UpdateUserProfile(WebhookRequestEventType type, KeycloakWebhookRequest payload)
        {
            if (string.IsNullOrEmpty(payload?.details?.username))
            {
                _logger.LogError($"Webhook payload contains no associated Keycloak username");
                return;
            }

            _logger.LogInformation($"Trying to find the Keycloak user with username '{payload?.details?.username}'");
            var kcUser = await _keycloakClient.GetUser(payload?.details?.username);
            if (kcUser == null)
            {
                _logger.LogError($"Failed to retrieve the Keycloak user with username '{payload?.details.username}'");
                return;
            }

            var userRequest = _userService.GetByEmailOrNull(kcUser.Username)?.ToUserRequest();

            switch (type)
            {
                case WebhookRequestEventType.Register:
                case WebhookRequestEventType.UpdateProfile:
                    if (userRequest == null)
                    {
                        if (type == WebhookRequestEventType.UpdateProfile)
                        {
                            _logger.LogError($"{type}: Failed to retrieve the Yoma user with username '{payload?.details.username}'");
                            return;
                        }
                        userRequest = new UserRequest();
                    }

                    userRequest.Email = kcUser.Email.Trim();
                    userRequest.FirstName = kcUser.FirstName.Trim();
                    userRequest.Surname = kcUser.LastName.Trim();
                    userRequest.EmailConfirmed = kcUser.EmailVerified;
                    userRequest.PhoneNumber = kcUser.PhoneNumber;

                    if (!string.IsNullOrEmpty(kcUser.Gender))
                    {
                        var gender = _genderService.GetByNameOrNull(kcUser.Gender);

                        if (gender == null)
                            _logger.LogError($"Failed to parse Keycloak '{CustomAttributes.Gender}' with value '{kcUser.Gender}'");
                        else
                            userRequest.GenderId = gender.Id;
                    }

                       if (!string.IsNullOrEmpty(kcUser.CountryOfOrigin))
                    {
                        var country = _countryService.GetByNameOrNull(kcUser.CountryOfOrigin);

                        if (country == null)
                            _logger.LogError($"Failed to parse Keycloak '{CustomAttributes.CountryOfOrigin}' with value '{kcUser.CountryOfOrigin}'");
                        else
                            userRequest.CountryId = country.Id;
                    }

                    if (!string.IsNullOrEmpty(kcUser.CountryOfResidence))
                    {
                        var country = _countryService.GetByNameOrNull(kcUser.CountryOfResidence);

                        if (country == null)
                            _logger.LogError($"Failed to parse Keycloak '{CustomAttributes.CountryOfResidence}' with value '{kcUser.CountryOfResidence}'");
                        else
                            userRequest.CountryOfResidenceId = country.Id;
                    }

                    if (!string.IsNullOrEmpty(kcUser.DateOfBirth))
                    {
                        if (!DateTime.TryParse(kcUser.DateOfBirth, out var dateOfBirth))
                            _logger.LogError($"Failed to parse Keycloak '{CustomAttributes.DateOfBirth}' with value '{kcUser.DateOfBirth}'");
                        else
                            userRequest.DateOfBirth = dateOfBirth;
                    }

                    if (type == WebhookRequestEventType.UpdateProfile) break;

                    //add newly registered user to the default "User" role
                    await _keycloakClient.EnsureRoles(kcUser.Id, new List<string> { Constants.Role_User });

                    //TODO: AriesCloudApi tenant / wallet creation
                    break;

                case WebhookRequestEventType.Login:
                    if (userRequest == null)
                    {
                        _logger.LogError($"{type}: Failed to retrieve the Yoma user with username '{payload?.details.username}'");
                        return;
                    }

                    //updated here after email verification a login event is raised
                    userRequest.EmailConfirmed = kcUser.EmailVerified;
                    userRequest.DateLastLogin = DateTime.Now;

                    break;

                default: //event not supported
                    _logger.LogError($"Failed to retrieve the Keycloak user with username '{payload?.details.username}'");
                    return;
            }

            userRequest.ExternalId = kcUser.Id;

            await _userService.Upsert(userRequest);
        }
    }
    #endregion
}

