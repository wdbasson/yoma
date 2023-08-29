using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Transactions;
using Yoma.Core.Domain.Core.Helpers;
using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Entity.Extensions;
using Yoma.Core.Domain.Entity.Helpers;
using Yoma.Core.Domain.Entity.Interfaces;
using Yoma.Core.Domain.Entity.Models;
using Yoma.Core.Domain.Entity.Validators;
using Yoma.Core.Domain.IdentityProvider.Interfaces;
using Yoma.Core.Domain.Lookups.Interfaces;

namespace Yoma.Core.Domain.Entity.Services
{
    public class UserProfileService : IUserProfileService
    {
        #region Class Variables
        private readonly IIdentityProviderClient _identityProviderClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IGenderService _genderService;
        private readonly ICountryService _countryService;
        private readonly IOrganizationService _organizationService;
        private readonly UserProfileRequestValidator _userProfileRequestValidator;
        private readonly IRepositoryValueContainsWithNavigation<User> _userRepository;
        #endregion

        #region Constructor
        public UserProfileService(IHttpContextAccessor httpContextAccessor,
            IIdentityProviderClientFactory identityProviderClientFactory,
            IUserService userService,
            IGenderService genderService,
            ICountryService countryService,
            IOrganizationService organizationService,
            UserProfileRequestValidator userProfileRequestValidator,
            IRepositoryValueContainsWithNavigation<User> userRepository)
        {
            _identityProviderClient = identityProviderClientFactory.CreateClient();
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _genderService = genderService;
            _countryService = countryService;
            _organizationService = organizationService;
            _userProfileRequestValidator = userProfileRequestValidator;
            _userRepository = userRepository;
        }
        #endregion

        #region Public Members
        public UserProfile Get()
        {
            var username = HttpContextAccessorHelper.GetUsername(_httpContextAccessor, false);

            var user = _userService.GetByEmail(username);

            var result = user.ToProfile();

            result.AdminsOf = _organizationService.ListAdminsOf();

            return result;
        }

        public async Task<UserProfile> UpsertPhoto(IFormFile file)
        {
            var username = HttpContextAccessorHelper.GetUsername(_httpContextAccessor, false);

            var user = await _userService.UpsertPhoto(username, file);

            var result = user.ToProfile();

            result.AdminsOf = _organizationService.ListAdminsOf();

            return result;
        }

        public async Task<UserProfile> Update(UserProfileRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            await _userProfileRequestValidator.ValidateAndThrowAsync(request);

            var username = HttpContextAccessorHelper.GetUsername(_httpContextAccessor, false);

            var user = _userService.GetByEmail(username);

            if (!user.ExternalId.HasValue)
                throw new InvalidOperationException($"External id expected for user with id '{user.Id}'");

            var emailUpdated = !string.Equals(user.Email, request.Email, StringComparison.CurrentCultureIgnoreCase);
            if (emailUpdated)
                if (_userService.GetByEmailOrNull(request.Email) != null)
                    throw new ValidationException($"{nameof(User)} with the specified email address '{request.Email}' already exists");

            user.Email = request.Email;
            if (emailUpdated) user.EmailConfirmed = false;
            user.FirstName = request.FirstName;
            user.Surname = request.Surname;
            user.DisplayName = request.DisplayName;
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

                var userIdentityProvider = new IdentityProvider.Models.User
                {
                    Id = user.ExternalId.Value,
                    FirstName = user.FirstName,
                    LastName = user.Surname,
                    Username = user.Email,
                    Email = user.Email,
                    EmailVerified = user.EmailConfirmed,
                    PhoneNumber = user.PhoneNumber,
                    Gender = user.GenderId.HasValue ? _genderService.GetById(user.GenderId.Value).Name : null,
                    CountryOfOrigin = user.CountryId.HasValue ? _countryService.GetById(user.CountryId.Value).Name : null,
                    CountryOfResidence = user.CountryOfResidenceId.HasValue ? _countryService.GetById(user.CountryOfResidenceId.Value).Name : null,
                    DateOfBirth = user.DateOfBirth.HasValue ? user.DateOfBirth.Value.ToString("yyyy/MM/dd") : null
                };

                await _identityProviderClient.UpdateUser(userIdentityProvider, request.ResetPassword);

                scope.Complete();
            }

            var result = user.ToProfile();
            result.AdminsOf = _organizationService.ListAdminsOf();
            return result;
        }
        #endregion
    }
}