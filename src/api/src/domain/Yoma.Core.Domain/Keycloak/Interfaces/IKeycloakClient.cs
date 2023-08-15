using Microsoft.AspNetCore.Http;
using Yoma.Core.Domain.Keycloak.Models;

namespace Yoma.Core.Domain.Keycloak.Interfaces
{
    public interface IKeycloakClient
    {
        bool AuthenticateWebhook(HttpContext httpContext);

        Task<User?> GetUser(string? username);

        Task UpdateUser(User user, bool resetPassword);

        Task EnsureRoles(Guid id, List<string> roles);

        Task RemoveRoles(Guid id, List<string> roles);
    }
}
