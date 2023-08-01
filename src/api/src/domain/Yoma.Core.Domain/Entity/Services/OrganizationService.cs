using Microsoft.AspNetCore.Http;
using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Entity.Interfaces;
using Yoma.Core.Domain.Entity.Models;

namespace Yoma.Core.Domain.Entity.Services
{
    public class OrganizationService : IOrganizationService
    {
        #region Class Variables
        private readonly IRepository<Organization> _organizationRepository;
        #endregion

        #region Constructor
        public OrganizationService(IRepository<Organization> organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }
        #endregion

        #region Public Members
        public Organization GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Organization> Upsert(Organization request)
        {
            throw new NotImplementedException();
        }

        public List<OrganizationProviderType> ListProviderTypesById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task AssignProviderType(Guid id, Guid providerTypeId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProviderType(Guid id, Guid providerTypeId)
        {
            throw new NotImplementedException();
        }

        public async Task<Organization> UpsertLogo(Guid id, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public async Task<Organization> UpsertRegistrationDocument(Guid id, IFormFile file)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
