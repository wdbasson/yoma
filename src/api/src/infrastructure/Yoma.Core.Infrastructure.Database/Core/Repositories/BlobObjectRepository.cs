﻿using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Infrastructure.Database.Context;
using Yoma.Core.Infrastructure.Database.Core.Entities;

namespace Yoma.Core.Infrastructure.Database.Core.Repositories
{
    public class BlobObjectRepository : BaseRepository<BlobObject>, IRepository<Domain.Core.Models.BlobObject>
    {
        #region Class Variables
        #endregion

        #region Constructor
        public BlobObjectRepository(ApplicationDbContext context) : base(context)
        {
        }
        #endregion

        #region Public Members
        public IQueryable<Domain.Core.Models.BlobObject> Query()
        {
            return _context.BlobObject.Select(entity => new Domain.Core.Models.BlobObject
            {
                Id = entity.Id,
                Key = entity.Key,
                DateCreated = entity.DateCreated
            });
        }

        public async Task<Domain.Core.Models.BlobObject> Create(Domain.Core.Models.BlobObject item)
        {
            item.DateCreated = DateTimeOffset.Now;

            var entity = new BlobObject
            {
                Id = item.Id,
                Key = item.Key,
                DateCreated = item.DateCreated
            };

            _context.BlobObject.Add(entity);
            item.Id = entity.Id;
            await _context.SaveChangesAsync();

            return item;
        }

        public Task Update(Domain.Core.Models.BlobObject item)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(Domain.Core.Models.BlobObject item)
        {
            var entity = _context.BlobObject.Where(o => o.Id == item.Id).SingleOrDefault() ?? throw new ArgumentOutOfRangeException(nameof(item), $"S3Object with id '{item.Id}' does not exist");
            _context.BlobObject.Remove(entity);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}