using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Opportunity.Models.Lookups;
using Yoma.Core.Infrastructure.Database.Context;
using Yoma.Core.Infrastructure.Database.Core.Repositories;
using Yoma.Core.Domain.Core.Extensions;
using System.Linq.Expressions;

namespace Yoma.Core.Infrastructure.Database.Opportunity.Repositories.Lookups
{
    public class OpportunityCategoryRepository : BaseRepository<Entities.Lookups.OpportunityCategory>, IRepositoryValueContains<OpportunityCategory>
    {
        #region Constructor
        public OpportunityCategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
        #endregion

        #region Public Members
        public IQueryable<OpportunityCategory> Query()
        {
            return _context.OpportunityCategory.Select(entity => new OpportunityCategory
            {
                Id = entity.Id,
                Name = entity.Name
            });
        }

        public Expression<Func<OpportunityCategory, bool>> Contains(Expression<Func<OpportunityCategory, bool>> predicate, string value)
        {
            return predicate.Or(o => o.Name.Contains(value));
        }

        public IQueryable<OpportunityCategory> Contains(IQueryable<OpportunityCategory> query, string value)
        {
            return query.Where(o => o.Name.Contains(value));
        }

        public Task<OpportunityCategory> Create(OpportunityCategory item)
        {
            throw new NotImplementedException();
        }

        public Task Update(OpportunityCategory item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(OpportunityCategory item)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
