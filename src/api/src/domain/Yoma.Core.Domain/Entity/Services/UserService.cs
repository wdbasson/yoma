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
using Microsoft.AspNetCore.Http;
using Amazon.S3.Model;

namespace Yoma.Core.Domain.Entity.Services
{
    public class UserService : IUserService
    {
        #region Class Variables
        private readonly AppSettings _appSettings;
        private readonly KeycloakAuthenticationOptions _keycloakAuthenticationOptions;
        private readonly IS3ObjectService _s3ObjectService;
        private readonly IGenderService _genderService;
        private readonly ICountryService _countryService;
        private readonly UserValidator _userValidator;
        private readonly UserProfileRequestValidator _userProfileRequestValidator;
        private readonly IRepository<User> _userRepository;
        #endregion

        #region Constructor
        public UserService(IOptions<AppSettings> appSettings,
            IOptions<KeycloakAuthenticationOptions> keycloakAuthenticationOptions,
            IS3ObjectService s3ObjectService,
            IGenderService genderService,
            ICountryService countryService,
            UserValidator userValidator,
            UserProfileRequestValidator userProfileRequestValidator,
            IRepository<User> userRepository)
        {
            _appSettings = appSettings.Value;
            _keycloakAuthenticationOptions = keycloakAuthenticationOptions.Value;
            _s3ObjectService = s3ObjectService;
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

            result.PhotoURL = GetPhotoURL(result.PhotoId);

            return result;
        }

        public User? GetByEmailOrNull(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email));
            email = email.Trim();

            var result = _userRepository.Query().SingleOrDefault(o => o.Email == email);
            if (result == null) return result;

            result.PhotoURL = GetPhotoURL(result.PhotoId);

            return result;
        }

        public User GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var result = _userRepository.Query().SingleOrDefault(o => o.Id == id);

            if (result == null)
                throw new ArgumentOutOfRangeException(nameof(id), $"User with id '{id}' does not exist");

            result.PhotoURL = GetPhotoURL(result.PhotoId);

            return result;
        }

        public async Task<User> UpdateProfile(string? email, UserProfileRequest request)
        {
            var result = GetByEmail(email);

            await _userProfileRequestValidator.ValidateAndThrowAsync(request);

            var emailUpdated = !string.Equals(result.Email, request.Email, StringComparison.CurrentCultureIgnoreCase);
            if (emailUpdated)
                if (GetByEmailOrNull(request.Email) != null)
                    throw new ValidationException($"An user with the specified email address '{request.Email}' already exists");

            result.Email = request.Email;
            if (emailUpdated) result.EmailConfirmed = false;
            result.FirstName = request.FirstName;
            result.Surname = request.Surname;
            result.SetDisplayName();
            result.PhoneNumber = request.PhoneNumber;
            result.CountryId = request.CountryId;
            result.CountryOfResidenceId = request.CountryOfResidenceId;
            result.GenderId = request.GenderId;
            result.DateOfBirth = request.DateOfBirth;

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                await _userRepository.Update(result);
                result.DateModified = DateTimeOffset.Now;

                await updateKeycloak(result, request.ResetPassword);

                scope.Complete();
            }

            result.PhotoURL = GetPhotoURL(result.PhotoId);

            return result;
        }

        public async Task<User> Upsert(User request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            await _userValidator.ValidateAndThrowAsync(request);

            // check if user exists
            var isNew = !request.Id.HasValue;
            var result = !request.Id.HasValue ? new User { Id = Guid.NewGuid() } : GetById(request.Id.Value);

            var existingByEmail = GetByEmailOrNull(result.Email);
            if (existingByEmail != null && (isNew || result.Id != existingByEmail.Id))
                throw new ValidationException($"An user with the specified email address '{request.Email}' already exists");

            //profile fields updatable via UpdateProfile; Keycloak is source of truth
            if (isNew)
            {
                //ensure user exists in keycloak
                using var httpClient = new KeycloakHttpClient(_keycloakAuthenticationOptions.AuthServerUrl,
                    _appSettings.AdminKeyCloak.Username, _appSettings.AdminKeyCloak.Password);
                using var usersApi = ApiClientFactory.Create<UsersApi>(httpClient);
                var kcUser = (await usersApi.GetUsersAsync(_keycloakAuthenticationOptions.Realm, username: result.Email, exact: true)).SingleOrDefault();
                if (kcUser == null)
                    throw new InvalidOperationException($"User with email '{result.Email}' does not exist in Keycloak");

                result.Email = request.Email;
                result.EmailConfirmed = request.EmailConfirmed;
                result.FirstName = request.FirstName;
                result.Surname = request.Surname;
                result.SetDisplayName();
                result.PhoneNumber = request.PhoneNumber;
                result.CountryId = request.CountryId;
                result.CountryOfResidenceId = request.CountryOfResidenceId;
                result.PhotoId = request.PhotoId;
                result.GenderId = request.GenderId;
                result.DateOfBirth = request.DateOfBirth;
            }

            result.EmailConfirmed = request.EmailConfirmed;
            result.PhotoId = request.PhotoId;
            result.DateLastLogin = request.DateLastLogin;
            result.ExternalId = request.ExternalId;
            result.ZltoWalletId = request.ZltoWalletId;
            result.ZltoWalletCountryId = request.ZltoWalletCountryId;
            result.TenantId = request.TenantId;

            result.PhotoURL = GetPhotoURL(result.PhotoId);

            if (isNew)
                result = await _userRepository.Create(result);
            else
            {
                await _userRepository.Update(result);
                result.DateModified = DateTimeOffset.Now;
            }

            return result;
        }

        public async Task<User> UpsertPhoto(string? email, IFormFile file)
        {
            var result = GetByEmail(email);
            var currentPhotoId = result.PhotoId;

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                Core.Models.S3Object? s3Object = null;
                try
                {
                    s3Object = await _s3ObjectService.Create(file, Core.FileTypeEnum.Photos);
                    result.PhotoId = s3Object.Id;
                    await _userRepository.Update(result);

                    if (currentPhotoId.HasValue)
                        await _s3ObjectService.Delete(currentPhotoId.Value);

                    scope.Complete();
                }
                catch 
                {
                    if (s3Object != null)
                        await _s3ObjectService.Delete(s3Object.Id);

                    throw;
                }
            }

            result.PhotoURL = GetPhotoURL(result.PhotoId);

            return result;
        }
        #endregion

        #region Private Members
        private string? GetPhotoURL(Guid? id)
        {
            if (!id.HasValue) return null;
            return _s3ObjectService.GetURL(id.Value);
        }

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
