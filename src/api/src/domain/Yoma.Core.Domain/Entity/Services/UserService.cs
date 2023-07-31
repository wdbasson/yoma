using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Entity.Extensions;
using Yoma.Core.Domain.Entity.Interfaces;
using Yoma.Core.Domain.Entity.Models;
using FS.Keycloak.RestApiClient.Api;
using FS.Keycloak.RestApiClient.Client;
using FS.Keycloak.RestApiClient.Model;
using Keycloak.AuthServices.Authentication;
using Microsoft.Extensions.Options;
using Yoma.Core.Domain.Core.Models;
using Yoma.Core.Domain.Keycloak;
using Yoma.Core.Domain.Core.Extensions;
using Yoma.Core.Domain.Lookups.Interfaces;
using Yoma.Core.Domain.Exceptions;
using Yoma.Core.Domain.Entity.Validators;
using FluentValidation;
using System.Transactions;

namespace Yoma.Core.Domain.Entity.Services
{
    public class UserService : IUserService
    {
        #region Class Variables
        private readonly AppSettings _appSettings;
        private readonly KeycloakAuthenticationOptions _keycloakAuthenticationOptions;
        private readonly IGenderService _genderService;
        private readonly ICountryService _countryService;
        private readonly UserValidator _userValidator;
        private readonly UserProfileRequestValidator _userProfileRequestValidator;
        private readonly IRepository<User> _userRepository;
        #endregion

        #region Constructor
        public UserService(IOptions<AppSettings> appSettings,
            IOptions<KeycloakAuthenticationOptions> keycloakAuthenticationOptions,
            IGenderService genderService,
            ICountryService countryService,
            UserValidator userValidator,
            UserProfileRequestValidator userProfileRequestValidator,
            IRepository<User> userRepository)
        {
            _appSettings = appSettings.Value;
            _keycloakAuthenticationOptions = keycloakAuthenticationOptions.Value;
            _genderService = genderService;
            _countryService = countryService;
            _userValidator = userValidator;
            _userProfileRequestValidator = userProfileRequestValidator;
            _userRepository = userRepository;
        }
        #endregion

        #region Public Members
        public User GetByEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));

            var result = GetByEmailOrNull(email);
            if (result == null)
                throw new ArgumentOutOfRangeException(nameof(email), $"User with email '{email}' does not exist");

            return result;
        }

        public User? GetByEmailOrNull(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));
            email = email.Trim();

            return _userRepository.Query().SingleOrDefault(o => o.Email == email);
        }

        public User GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var result = _userRepository.Query().SingleOrDefault(o => o.Id == id);

            if (result == null)
                throw new ArgumentOutOfRangeException(nameof(id), $"User with id '{id}' does not exist");

            return result;
        }

        public async Task<User> UpdateProfile(string? email, UserProfileRequest request)
        {
            var user = GetByEmail(email);

            await _userProfileRequestValidator.ValidateAndThrowAsync(request);

            var emailUpdated = !string.Equals(user.Email, request.Email, StringComparison.CurrentCultureIgnoreCase);
            if (emailUpdated)
                if (GetByEmailOrNull(request.Email) != null)
                    throw new ValidationException($"An user with the specified email address '{request.Email}' already exists");

            user.Email = request.Email;
            if (emailUpdated) user.EmailConfirmed = false;
            user.FirstName = request.FirstName;
            user.Surname = request.Surname;
            user.SetDisplayName();
            user.PhoneNumber = request.PhoneNumber;
            user.CountryId = request.CountryId;
            user.CountryOfResidenceId = request.CountryOfResidenceId;
            user.GenderId = request.GenderId;
            user.DateOfBirth = request.DateOfBirth;

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
            await _userRepository.Update(user);
            user.DateModified = DateTimeOffset.Now;

            await updateKeycloak(user, request.ResetPassword);

                scope.Complete();
            }

            return user;
        }

        public async Task<User> Upsert(User request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            await _userValidator.ValidateAndThrowAsync(request);

            // check if user exists
            var isNew = !request.Id.HasValue;
            var user = !request.Id.HasValue ? new User { Id = Guid.NewGuid() } : GetById(request.Id.Value);

            var existingByEmail = GetByEmailOrNull(user.Email);
            if (existingByEmail != null && (isNew || user.Id != existingByEmail.Id))
                throw new ValidationException($"An user with the specified email address '{request.Email}' already exists");

            //profile fields updatable via UpdateProfile; Keycloak is source of truth
            if (isNew)
            {
                //ensure user exists in keycloak
                using var httpClient = new KeycloakHttpClient(_keycloakAuthenticationOptions.AuthServerUrl,
                    _appSettings.AdminKeyCloak.Username, _appSettings.AdminKeyCloak.Password);
                using var usersApi = ApiClientFactory.Create<UsersApi>(httpClient);
                var kcUser = (await usersApi.GetUsersAsync(_keycloakAuthenticationOptions.Realm, username: user.Email, exact: true)).SingleOrDefault();
                if (kcUser == null)
                    throw new InvalidOperationException($"User with email '{user.Email}' does not exist in Keycloak");

            user.Email = request.Email;
            user.EmailConfirmed = request.EmailConfirmed;
            user.FirstName = request.FirstName;
            user.Surname = request.Surname;
                user.SetDisplayName();
            user.PhoneNumber = request.PhoneNumber;
            user.CountryId = request.CountryId;
            user.CountryOfResidenceId = request.CountryOfResidenceId;
            user.PhotoId = request.PhotoId;
            user.GenderId = request.GenderId;
            user.DateOfBirth = request.DateOfBirth;
            }

            user.EmailConfirmed = request.EmailConfirmed;
            user.PhotoId = request.PhotoId;
            user.DateLastLogin = request.DateLastLogin;
            user.ExternalId = request.ExternalId;
            user.ZltoWalletId = request.ZltoWalletId;
            user.ZltoWalletCountryId = request.ZltoWalletCountryId;
            user.TenantId = request.TenantId;

            user.SetDisplayName();

            if (isNew)
                user = await _userRepository.Create(user);
            else
            {
                await _userRepository.Update(user);
                user.DateModified = DateTimeOffset.Now;
            }

            return user;
        }
        #endregion

        #region Private Members
        private async Task updateKeycloak(User user, bool resetPassword)
        {
            using var httpClient = new KeycloakHttpClient(_keycloakAuthenticationOptions.AuthServerUrl, _appSettings.AdminKeyCloak.Username, _appSettings.AdminKeyCloak.Password);
            using var userApi = ApiClientFactory.Create<UserApi>(httpClient);

            var request = new UserRepresentation
            {
                Id = user.ExternalId.ToString(),
                FirstName = user.FirstName,
                LastName = user.Surname,
                Attributes = new Dictionary<string, List<string>>(),
                Username = user.Email,
                Email = user.Email,
                EmailVerified = user.EmailConfirmed
            };

            if (!string.IsNullOrEmpty(user.PhoneNumber))
                request.Attributes.Add(CustomAttributes.PhoneNumber.ToDescription(), new List<string> { { user.PhoneNumber } });

            if (user.GenderId.HasValue)
                request.Attributes.Add(CustomAttributes.Gender.ToDescription(), new List<string> { { _genderService.GetById(user.GenderId.Value).Name } });

            if (user.CountryId.HasValue)
                request.Attributes.Add(CustomAttributes.CountryOfOrigin.ToDescription(), new List<string> { { _countryService.GetById(user.CountryId.Value).Name } });

            if (user.CountryOfResidenceId.HasValue)
                request.Attributes.Add(CustomAttributes.CountryOfResidence.ToDescription(), new List<string> { { _countryService.GetById(user.CountryOfResidenceId.Value).Name } });

            var dateOfBirth = user.DateOfBirth?.ToString("yyyy/MM/dd");
            if (!string.IsNullOrEmpty(dateOfBirth))
                request.Attributes.Add(CustomAttributes.DateOfBirth.ToDescription(), new List<string> { { dateOfBirth } });

            try
            {
                // update user details
                await userApi.PutUsersByIdAsync(_keycloakAuthenticationOptions.Realm, user.ExternalId.ToString(), request);

                // send verify email 
                if (!user.EmailConfirmed)
                    await userApi.PutUsersSendVerifyEmailByIdAsync(_keycloakAuthenticationOptions.Realm, user.ExternalId.ToString());

                // send forgot password email
                if (resetPassword)
                    await userApi.PutUsersExecuteActionsEmailByIdAsync(_keycloakAuthenticationOptions.Realm, user.ExternalId.ToString(), requestBody: new List<string> { "UPDATE_PASSWORD" });
            }
            catch (Exception ex)
            {
                throw new TechnicalException($"Error updating user {user.Id} in Keycloak", ex);
            }
        }
        #endregion
    }
}
