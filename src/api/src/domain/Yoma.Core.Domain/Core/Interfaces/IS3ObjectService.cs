using Microsoft.AspNetCore.Http;
using Yoma.Core.Domain.Core.Models;

namespace Yoma.Core.Domain.Core.Interfaces
{
    public interface IS3ObjectService
    {
        S3Object GetById(Guid id);

        Task<S3Object> Create(IFormFile file, FileTypeEnum type);

        string GetURL(Guid id);

        Task Delete(Guid id);
    }
}
