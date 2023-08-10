using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Infrastructure.Database.Context;
using Yoma.Core.Infrastructure.Database.Core.Repositories;

namespace Yoma.Core.Infrastructure.Database.Entity.Repositories.Lookups
{
    public class OrganizationProviderTypeRepository : BaseRepository<Entities.Lookups.OrganizationProviderType>, IRepository<Domain.Entity.Models.Lookups.OrganizationProviderType>
    {
        #region Class Variables
        #endregion

        #region Constructor
        public OrganizationProviderTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
        #endregion

        #region Public Members
        public IQueryable<Domain.Entity.Models.Lookups.OrganizationProviderType> Query()
        {
            return _context.OrganizationProviderType.Select(entity => new Domain.Entity.Models.Lookups.OrganizationProviderType 
            {
                Id = entity.Id,
                Name = entity.Name
            });
        }

        public Task<Domain.Entity.Models.Lookups.OrganizationProviderType> Create(Domain.Entity.Models.Lookups.OrganizationProviderType item)
        {
            throw new NotImplementedException();
        }

        public Task Update(Domain.Entity.Models.Lookups.OrganizationProviderType item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Domain.Entity.Models.Lookups.OrganizationProviderType item)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
