using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Infrastructure.Database.Context;
using Yoma.Core.Infrastructure.Database.Core.Repositories;
using Yoma.Core.Infrastructure.Database.Entity.Entities;

namespace Yoma.Core.Infrastructure.Database.Entity.Repositories
{
    public class UserRepository : BaseRepository<User>, IRepositoryWithNavigation<Domain.Entity.Models.User>
    {
        #region Constructor
        public UserRepository(ApplicationDbContext context) : base(context) { }
        #endregion

        #region Public Members
        public IQueryable<Domain.Entity.Models.User> Query()
        {
            return Query(false);
        }

        public IQueryable<Domain.Entity.Models.User> Query(bool includeChildItems)
        {
            return _context.User.Select(entity => new Domain.Entity.Models.User()
            {
                Id = entity.Id,
                Email = entity.Email,
                EmailConfirmed = entity.EmailConfirmed,
                FirstName = entity.FirstName,
                Surname = entity.Surname,
                DisplayName = entity.DisplayName,
                PhoneNumber = entity.PhoneNumber,
                CountryId = entity.CountryId,
                CountryOfResidenceId = entity.CountryOfResidenceId,
                PhotoId = entity.PhotoId,
                GenderId = entity.GenderId,
                DateOfBirth = entity.DateOfBirth,
                DateLastLogin = entity.DateLastLogin,
                ExternalId = entity.ExternalId,
                ZltoWalletId = entity.ZltoWalletId,
                ZltoWalletCountryId = entity.ZltoWalletCountryId,
                ZltoWalletCountryCodeAlpha2 = entity.Country.CodeAlpha2,
                TenantId = entity.TenantId,
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified
            });
        }

        public async Task<Domain.Entity.Models.User> Create(Domain.Entity.Models.User item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            item.DateCreated = DateTimeOffset.Now;
            item.DateModified = DateTimeOffset.Now;

            var entity = new User
            {
                Id = item.Id,
                Email = item.Email,
                EmailConfirmed = item.EmailConfirmed,
                FirstName = item.FirstName,
                Surname = item.Surname,
                DisplayName = item.DisplayName,
                PhoneNumber = item.PhoneNumber,
                CountryId = item.CountryId,
                CountryOfResidenceId = item.CountryOfResidenceId,
                PhotoId = item.PhotoId,
                GenderId = item.GenderId,
                DateOfBirth = item.DateOfBirth,
                DateLastLogin = item.DateLastLogin,
                ExternalId = item.ExternalId,
                ZltoWalletId = item.ZltoWalletId,
                ZltoWalletCountryId = item.ZltoWalletCountryId,
                TenantId = item.TenantId,
                DateCreated = item.DateCreated,
                DateModified = item.DateModified
            };

            _context.User.Add(entity);

            await _context.SaveChangesAsync();

            item.Id = entity.Id;

            return item;
        }

        public async Task Update(Domain.Entity.Models.User item)
        {
            var entity = _context.User.Where(o => o.Id == item.Id).SingleOrDefault() ?? throw new ArgumentOutOfRangeException(nameof(item), $"User with id '{item.Id}' does not exist");
            entity.Email = item.Email;
            entity.EmailConfirmed = item.EmailConfirmed;
            entity.FirstName = item.FirstName;
            entity.Surname = item.Surname;
            entity.DisplayName = item.DisplayName;
            entity.PhoneNumber = item.PhoneNumber;
            entity.CountryId = item.CountryId;
            entity.CountryOfResidenceId = item.CountryOfResidenceId;
            entity.PhotoId = item.PhotoId;
            entity.GenderId = item.GenderId;
            entity.DateOfBirth = item.DateOfBirth;
            entity.DateLastLogin = item.DateLastLogin;
            entity.ExternalId = item.ExternalId;
            entity.ZltoWalletId = item.ZltoWalletId;
            entity.ZltoWalletCountryId = item.ZltoWalletCountryId;
            entity.TenantId = item.TenantId;
            entity.DateModified = DateTimeOffset.Now;

            await _context.SaveChangesAsync();
        }

        public Task Delete(Domain.Entity.Models.User item)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
