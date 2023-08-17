using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Opportunity.Models.Lookups;
using Yoma.Core.Infrastructure.Database.Context;
using Yoma.Core.Infrastructure.Database.Core.Repositories;
using Yoma.Core.Domain.Core.Extensions;
using System.Linq.Expressions;

namespace Yoma.Core.Infrastructure.Database.Opportunity.Repositories.Lookups
{
    public class OpportunityTypeRepository : BaseRepository<Entities.Lookups.OpportunityType>, IRepository<OpportunityType>
    {
        #region Constructor
        public OpportunityTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
        #endregion

        #region Public Members
        public IQueryable<OpportunityType> Query()
        {
            return _context.OpportunityType.Select(entity => new OpportunityType
            {
                Id = entity.Id,
                Name = entity.Name
            }); ;
        }

        public Expression<Func<OpportunityType, bool>> Contains(Expression<Func<OpportunityType, bool>> predicate, string value)
        {
            return predicate.Or(o => o.Name.Contains(value));
        }

        public IQueryable<OpportunityType> Contains(IQueryable<OpportunityType> query, string value)
        {
            return query.Where(o => o.Name.Contains(value));
        }

        public Task<OpportunityType> Create(OpportunityType item)
        {
            throw new NotImplementedException();
        }

        public Task Update(OpportunityType item)
        {
            throw new NotImplementedException();
        }
        public Task Delete(OpportunityType item)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
