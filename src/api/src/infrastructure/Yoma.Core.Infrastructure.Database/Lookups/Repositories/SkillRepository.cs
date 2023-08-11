using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Infrastructure.Database.Context;
using Yoma.Core.Infrastructure.Database.Core.Repositories;
using Yoma.Core.Infrastructure.Database.Lookups.Entities;

namespace Yoma.Core.Infrastructure.Database.Lookups.Repositories
{
    public class SkillRepository : BaseRepository<Skill>, IRepository<Domain.Lookups.Models.Skill>
    {
        #region Constructor
        public SkillRepository(ApplicationDbContext context) : base(context)
        {
        }
        #endregion

        #region Public Members
        public IQueryable<Domain.Lookups.Models.Skill> Query()
        {
            return _context.Skill.Select(entity => new Domain.Lookups.Models.Skill
            {
                Id = entity.Id,
                Name = entity.Name
            });
        }

        public Task<Domain.Lookups.Models.Skill> Create(Domain.Lookups.Models.Skill item)
        {
            throw new NotImplementedException();
        }

        public Task Update(Domain.Lookups.Models.Skill item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Domain.Lookups.Models.Skill item)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
