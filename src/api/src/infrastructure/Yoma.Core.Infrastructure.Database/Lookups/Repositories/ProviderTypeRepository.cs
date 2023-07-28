using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Infrastructure.Database.Context;
using Yoma.Core.Infrastructure.Database.Core.Repositories;
using Yoma.Core.Infrastructure.Database.Lookups.Entities;

namespace Yoma.Core.Infrastructure.Database.Lookups.Repositories
{
    public class ProviderTypeRepository : BaseRepository<ProviderType>, IRepository<Domain.Lookups.Models.ProviderType>
    {
        #region Class Variables
        #endregion

        #region Constructor
        public ProviderTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
        #endregion

        #region Public Members
        public IQueryable<Domain.Lookups.Models.ProviderType> Query()
        {
            return _context.ProviderType.Select(entity => new Domain.Lookups.Models.ProviderType
            {
                Id = entity.Id,
                Name = entity.Name
            });
        }

        public Task<Domain.Lookups.Models.ProviderType> Create(Domain.Lookups.Models.ProviderType item)
        {
            throw new NotImplementedException();
        }

        public Task Update(Domain.Lookups.Models.ProviderType item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Domain.Lookups.Models.ProviderType item)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
