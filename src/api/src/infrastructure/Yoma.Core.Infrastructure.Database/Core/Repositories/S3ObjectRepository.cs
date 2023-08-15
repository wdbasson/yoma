using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Infrastructure.Database.Context;
using Yoma.Core.Infrastructure.Database.Core.Entities;

namespace Yoma.Core.Infrastructure.Database.Core.Repositories
{
    public class S3ObjectRepository : BaseRepository<S3Object>, IRepository<Domain.Core.Models.S3Object>
    {
        #region Class Variables
        #endregion

        #region Constructor
        public S3ObjectRepository(ApplicationDbContext context) : base(context)
        {
        }
        #endregion

        #region Public Members
        public IQueryable<Domain.Core.Models.S3Object> Query()
        {
            return _context.S3Object.Select(entity => new Domain.Core.Models.S3Object
            {
                Id = entity.Id,
                ObjectKey = entity.ObjectKey,
                DateCreated = entity.DateCreated
            });
        }

        public async Task<Domain.Core.Models.S3Object> Create(Domain.Core.Models.S3Object item)
        {
            item.DateCreated = DateTimeOffset.Now;

            var entity = new S3Object
            {
                Id = item.Id,
                ObjectKey = item.ObjectKey,
                DateCreated = item.DateCreated
            };

            _context.S3Object.Add(entity);
            item.Id = entity.Id;
            await _context.SaveChangesAsync();

            return item;
        }

        public Task Update(Domain.Core.Models.S3Object item)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Domain.Core.Models.S3Object item)
        {
            var entity = _context.S3Object.Where(o => o.Id == item.Id).SingleOrDefault() ?? throw new ArgumentOutOfRangeException(nameof(item), $"S3Object with id '{item.Id}' does not exist");
            _context.S3Object.Remove(entity);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
