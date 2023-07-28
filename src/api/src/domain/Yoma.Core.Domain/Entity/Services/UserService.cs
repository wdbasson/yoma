using System.ComponentModel.DataAnnotations;
using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Entity.Extensions;
using Yoma.Core.Domain.Entity.Interfaces;
using Yoma.Core.Domain.Entity.Models;

namespace Yoma.Core.Domain.Entity.Services
{
    public class UserService : IUserService
    {
        #region Class Variables
        private readonly IRepository<User> _userRepository;
        #endregion

        #region Constructor
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region Public Members
        public User GetByEmail(string? email)
        {
            if(string.IsNullOrWhiteSpace(email))
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

            //TODO: validate model

            var emailUpdated = !string.Equals(user.Email, request.Email, StringComparison.CurrentCultureIgnoreCase);
            if (emailUpdated)
                if (GetByEmailOrNull(request.Email) != null)
                    throw new ValidationException($"An user with the specified email address '{request.Email}' already exists");

            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.Surname = request.Surname;
            user.SetDisplayName();
            user.PhoneNumber = request.PhoneNumber;
            user.CountryId = request.CountryId;
            user.CountryOfResidenceId = request.CountryOfResidenceId;
            user.GenderId = request.GenderId;
            user.DateOfBirth = request.DateOfBirth;
            



            await _userRepository.Update(user);
            user.DateModified = DateTimeOffset.Now;

            return user;
        }

        public async Task<User> Upsert(User request)
        {
            if (request == null)
                throw new ArgumentNullException();

            //TODO: validate model

            // check if user exists
            var isNew = !request.Id.HasValue;
            var user = !request.Id.HasValue ? new User { Id = Guid.NewGuid() } : GetById(request.Id.Value);

            user.Email = request.Email;
            user.EmailConfirmed = request.EmailConfirmed;
            user.FirstName = request.FirstName;
            user.Surname = request.Surname;
            user.DisplayName = request.DisplayName;
            user.PhoneNumber = request.PhoneNumber;
            user.CountryId = request.CountryId;
            user.CountryOfResidenceId = request.CountryOfResidenceId;
            user.PhotoId = request.PhotoId;
            user.GenderId = request.GenderId;
            user.DateOfBirth = request.DateOfBirth;
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
    }
}
