using Microsoft.AspNetCore.Http;
using Yoma.Core.Domain.Entity.Models;

namespace Yoma.Core.Domain.Entity.Interfaces
{
    public interface IOrganizationService
    {
        Organization GetById(Guid id);

        Task<Organization> Upsert(Organization request);

        List<OrganizationProviderType> ListProviderTypesById(Guid id);

        Task AssignProviderType(Guid id, Guid providerTypeId);

        Task DeleteProviderType(Guid id, Guid providerTypeId);

        Task<Organization> UpsertLogo(Guid id, IFormFile file);

        Task<Organization> UpsertRegistrationDocument(Guid id, IFormFile file);
    }
}
