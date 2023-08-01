using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Transactions;
using Yoma.Core.Domain.Core;
using Yoma.Core.Domain.Core.Interfaces;
using Yoma.Core.Domain.Core.Models;
using Yoma.Core.Domain.Entity.Interfaces;
using Yoma.Core.Domain.Entity.Models;
using Yoma.Core.Domain.Lookups.Interfaces;

namespace Yoma.Core.Domain.Entity.Services
{
    public class OrganizationService : IOrganizationService
    {
        #region Class Variables
        private readonly IUserService _userService;
        private readonly IProviderTypeService _providerTypeService;
        private readonly IS3ObjectService _s3ObjectService;
        private readonly IRepository<Organization> _organizationRepository;
        private readonly IRepository<OrganizationUser> _organizationUserRepository;
        private readonly IRepository<OrganizationProviderType> _organizationProviderTypeRepository;
        #endregion

        #region Constructor
        public OrganizationService(IUserService userService,
            IProviderTypeService providerTypeService,
            IS3ObjectService s3ObjectService,
            IRepository<Organization> organizationRepository,
            IRepository<OrganizationUser> organizationUserRepository,
            IRepository<OrganizationProviderType> organizationProviderTypeRepository)
        {
            _userService = userService;
            _providerTypeService = providerTypeService;
            _s3ObjectService = s3ObjectService;
            _organizationRepository = organizationRepository;
            _organizationUserRepository = organizationUserRepository;
            _organizationProviderTypeRepository = organizationProviderTypeRepository;   
        }
        #endregion

        #region Public Members
        public Organization GetById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            var result = _organizationRepository.Query().SingleOrDefault(o => o.Id == id);

            if (result == null)
                throw new ArgumentOutOfRangeException(nameof(id), $"{nameof(Organization)} with id '{id}' does not exist");

            result.LogoURL = GetS3ObjectURL(result.LogoId);
            result.CompanyRegistrationDocumentURL = GetS3ObjectURL(result.CompanyRegistrationDocumentId);

            return result;
        }

        public Organization? GetByNameOrNull(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            name = name.Trim();

            var result = _organizationRepository.Query().SingleOrDefault(o => o.Name == name);
            if (result == null) return result;

            result.LogoURL = GetS3ObjectURL(result.LogoId);
            result.CompanyRegistrationDocumentURL = GetS3ObjectURL(result.CompanyRegistrationDocumentId);

            return result;
        }

        public async Task<Organization> Upsert(OrganizationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            //TODO: validate model

            // check if user exists
            var isNew = !request.Id.HasValue;
            var result = !request.Id.HasValue ? new Organization { Id = Guid.NewGuid() } : GetById(request.Id.Value);

            var existingByEmail = GetByNameOrNull(request.Name);
            if (existingByEmail != null && (isNew || result.Id != existingByEmail.Id))
                throw new ValidationException($"{nameof(Organization)} with the specified name '{request.Name}' already exists");

            result.Name = request.Name;
            result.WebsiteURL = request.WebsiteURL;
            result.PrimaryContactName = request.PrimaryContactName;
            result.PrimaryContactEmail = request.PrimaryContactEmail;
            result.PrimaryContactPhone = request.PrimaryContactPhone;
            result.VATIN = request.VATIN;
            result.TaxNumber = request.TaxNumber;
            result.RegistrationNumber = request.RegistrationNumber;
            result.City = request.City;
            result.CountryId = request.CountryId;
            result.StreetAddress = request.StreetAddress;
            result.Province = request.Province;
            result.PostalCode = request.PostalCode;
            result.Tagline = request.Tagline;
            result.Biography = request.Biography;
            result.Approved = false;
            result.Active = true;

            if (isNew)
                result = await _organizationRepository.Create(result);
            else
            {
                await _organizationRepository.Update(result);
                result.DateModified = DateTimeOffset.Now;
            }

            return result;
        }

        public List<OrganizationProviderType> ListProviderTypesById(Guid id)
        {
            var org = GetById(id);

            var result = _organizationProviderTypeRepository.Query().Where(o => o.OrganizationId == id).ToList();

            return result;
        }

        public async Task AssignProviderTypes(Guid id, List<Guid> providerTypeIds)
        {
            var org = GetById(id);

            if (providerTypeIds == null || !providerTypeIds.Any())
                throw new ArgumentNullException(nameof(providerTypeIds));

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (var typeId in providerTypeIds)
                {
                    var type = _providerTypeService.GetById(typeId);

                    var item = _organizationProviderTypeRepository.Query().SingleOrDefault(o => o.OrganizationId == org.Id && o.ProviderTypeId == type.Id);
                    if (item != null) return;

                    item = new OrganizationProviderType
                    {
                        OrganizationId = org.Id,
                        ProviderTypeId = type.Id
                    };

                    await _organizationProviderTypeRepository.Create(item);
                }

                scope.Complete();
            }
        }

        public async Task DeleteProviderTypes(Guid id, List<Guid> providerTypeIds)
        {
            var org = GetById(id);

            if (providerTypeIds == null || !providerTypeIds.Any())
                throw new ArgumentNullException(nameof(providerTypeIds));

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (var typeId in providerTypeIds)
                {
                    var type = _providerTypeService.GetById(typeId);

                    var item = _organizationProviderTypeRepository.Query().SingleOrDefault(o => o.OrganizationId == org.Id && o.ProviderTypeId == type.Id);
                    if (item == null) return;

                    await _organizationProviderTypeRepository.Delete(item);
                }
            }
        }

        public async Task<Organization> UpsertLogo(Guid id, IFormFile file)
        {
            var result = GetById(id);
            var currentLogoId = result.LogoId;

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                S3Object? s3Object = null;
                try
                {
                    s3Object = await _s3ObjectService.Create(file, FileTypeEnum.Photos);
                    result.LogoId = s3Object.Id;
                    await _organizationRepository.Update(result);

                    if (currentLogoId.HasValue)
                        await _s3ObjectService.Delete(currentLogoId.Value);

                    scope.Complete();
                }
                catch
                {
                    if (s3Object != null)
                        await _s3ObjectService.Delete(s3Object.Id);

                    throw;
                }
            }

            result.LogoURL = GetS3ObjectURL(result.LogoId);

            return result;
        }

        public async Task<Organization> UpsertRegistrationDocument(Guid id, IFormFile file)
        {
            var result = GetById(id);
            var currentDocumentId = result.CompanyRegistrationDocumentId;

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                S3Object? s3Object = null;
                try
                {
                    s3Object = await _s3ObjectService.Create(file, FileTypeEnum.Documents);
                    result.CompanyRegistrationDocumentId = s3Object.Id;
                    await _organizationRepository.Update(result);

                    if (currentDocumentId.HasValue)
                        await _s3ObjectService.Delete(currentDocumentId.Value);

                    scope.Complete();
                }
                catch
                {
                    if (s3Object != null)
                        await _s3ObjectService.Delete(s3Object.Id);

                    throw;
                }
            }

            result.CompanyRegistrationDocumentURL = GetS3ObjectURL(result.CompanyRegistrationDocumentId);

            return result;
        }

        public async Task AssignAdmin(Guid id, Guid userId)
        {
            var org = GetById(id);
            var user = _userService.GetById(userId);

            var item = _organizationUserRepository.Query().SingleOrDefault(o => o.OrganizationId == id && o.UserId == userId);
            if (item != null) return;

            item = new OrganizationUser
            {
                OrganizationId = org.Id,
                UserId = user.Id
            };

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                await _organizationUserRepository.Create(item);

                await _userService.EnsureKeycloakRoles(user.ExternalId, new List<string> { Constants.Role_OrganizationAdmin });

                scope.Complete();
            }
        }

        public async Task RemoveAdmin(Guid id, Guid userId)
        {
            var org = GetById(id);

            var user = _userService.GetById(userId);
            var item = _organizationUserRepository.Query().SingleOrDefault(o => o.OrganizationId == id && o.UserId == userId);
            if (item == null) return;

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                await _organizationUserRepository.Delete(item);

                await _userService.RemoveKeycloakRoles(user.ExternalId, new List<string> { Constants.Role_OrganizationAdmin });

                scope.Complete();
            }
        }
        #endregion

        #region Private Members
        private string? GetS3ObjectURL(Guid? id)
        {
            if (!id.HasValue) return null;
            return _s3ObjectService.GetURL(id.Value);
        }
        #endregion
    }
}
