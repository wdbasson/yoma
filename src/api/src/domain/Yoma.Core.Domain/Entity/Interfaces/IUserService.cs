using Microsoft.AspNetCore.Http;
using Yoma.Core.Domain.Entity.Models;

namespace Yoma.Core.Domain.Entity.Interfaces
{
    public interface IUserService
    {
        public User GetByEmail(string? email);

        public User? GetByEmailOrNull(string email);

        public User GetById(Guid Id);

        Task<User> Upsert(User request);

        Task<User> UpdateProfile(string? email, UserProfileRequest request);

        Task<User> UpsertPhoto(string? email, IFormFile file);
    }
}
