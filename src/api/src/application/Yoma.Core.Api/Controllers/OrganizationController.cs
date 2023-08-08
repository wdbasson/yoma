using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Yoma.Core.Domain.Core;
using Yoma.Core.Domain.Entity.Interfaces;
using Yoma.Core.Domain.Entity.Models;

namespace Yoma.Core.Api.Controllers
{
    [Route("api/v3/organization")]
    [ApiController]
    [Authorize(Policy = Common.Constants.Authorization_Policy, 
        Roles = $"{Constants.Role_Admin},{Constants.Role_OrganizationAdmin}")]
    [SwaggerTag("(Admin or Organization Admin roles required)")]
    public class OrganizationController : Controller
    {
        #region Class Variables
        private readonly ILogger<OrganizationController> _logger;
        private readonly IOrganizationService _organizationService;
        #endregion

        #region Constructor
        public OrganizationController(
            ILogger<OrganizationController> logger,
            IOrganizationService organizationService)
        {
            _logger = logger;
            _organizationService = organizationService;
        }
        #endregion

        #region Public Members
        #region Administrative Actions
        [SwaggerOperation(Summary = "Get the specified organization by id")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Organization), (int)HttpStatusCode.OK)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            _logger.LogInformation($"Handling request {nameof(GetById)} ({nameof(id)}: {id})");

            var result = _organizationService.GetById(id);

            _logger.LogInformation($"Request {nameof(GetById)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [SwaggerOperation(Summary = "Insert or update an organization")]
        [HttpPost()]
        [ProducesResponseType(typeof(Organization), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Upsert([FromBody] OrganizationRequest request)
        {
            _logger.LogInformation($"Handling request {nameof(Upsert)} ({nameof(request)}: {(!request.Id.HasValue ? "insert" : $"update: {request.Id.Value}")}"); 

            var result = await _organizationService.Upsert(request);

            _logger.LogInformation($"Request {nameof(Upsert)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [SwaggerOperation(Summary = "List the provider types for specified organization")]
        [HttpGet("{id}/providerType")]
        [ProducesResponseType(typeof(List<OrganizationProviderType>), (int)HttpStatusCode.OK)]
        public IActionResult ListProviderTypesById([FromRoute] Guid id)
        {
            _logger.LogInformation($"Handling request {nameof(ListProviderTypesById)} ({nameof(id)}: {id})");

            var result = _organizationService.ListProviderTypesById(id);

            _logger.LogInformation($"Request {nameof(ListProviderTypesById)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [SwaggerOperation(Summary = "Assign provider type(s) to the specified organization")]
        [HttpPut("{id}/assign/providerTypes")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AssignProviderType([FromRoute] Guid id, [FromBody] List<Guid> providerTypeIds)
        {
            _logger.LogInformation($"Handling request {nameof(AssignProviderType)} ({nameof(id)}: {id} | {nameof(providerTypeIds)}: {providerTypeIds})");

            await _organizationService.AssignProviderTypes(id, providerTypeIds);

            _logger.LogInformation($"Request {nameof(AssignProviderType)} handled");

            return StatusCode((int)HttpStatusCode.OK);
        }

        [SwaggerOperation(Summary = "Remove provider type(s) from the specified organization")]
        [HttpDelete("{id}/remove/providerTypes")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProviderType([FromRoute] Guid id, [FromBody] List<Guid> providerTypeIds)
        {
            _logger.LogInformation($"Handling request {nameof(DeleteProviderType)} ({nameof(id)}: {id} | {nameof(providerTypeIds)}: {providerTypeIds})");

            await _organizationService.DeleteProviderTypes(id, providerTypeIds);

            _logger.LogInformation($"Request {nameof(DeleteProviderType)} handled");

            return StatusCode((int)HttpStatusCode.OK);
        }

        [SwaggerOperation(Summary = "Insert or update the organization's logo")]
        [HttpPost("{id}/logo")]
        [ProducesResponseType(typeof(Organization), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertLogo([FromRoute] Guid id, [Required] IFormFile file)
        {
            _logger.LogInformation($"Handling request {nameof(UpsertLogo)} ({file.Name})");

            var result = await _organizationService.UpsertLogo(id, file);

            _logger.LogInformation($"Request {nameof(UpsertLogo)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [SwaggerOperation(Summary = "Insert or update the organization's registration document")]
        [HttpPost("{id}/registrationDocument")]
        [ProducesResponseType(typeof(Organization), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertRegistrationDocument([FromRoute] Guid id, [Required] IFormFile file)
        {
            _logger.LogInformation($"Handling request {nameof(UpsertRegistrationDocument)} ({file.Name})");

            var result = await _organizationService.UpsertRegistrationDocument(id, file);

            _logger.LogInformation($"Request {nameof(UpsertRegistrationDocument)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [SwaggerOperation(Summary = "Assign the specified user as organization administrator")]
        [HttpPut("{id}/assign/{userId}/admin")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult AssignAdmin([FromRoute] Guid id, [FromRoute] Guid userId)
        {
            _logger.LogInformation($"Handling request {nameof(AssignAdmin)} ({nameof(id)}: {id} | {nameof(userId)}: {userId})");

            var result = _organizationService.AssignAdmin(id, userId);

            _logger.LogInformation($"Request {nameof(AssignAdmin)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [SwaggerOperation(Summary = "Remove the specified user as organization administrator")]
        [HttpDelete("{id}/remove/{userId}/admin")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult RemoveAdmin([FromRoute] Guid id, [FromRoute] Guid userId)
        {
            _logger.LogInformation($"Handling request {nameof(RemoveAdmin)} ({nameof(id)}: {id} | {nameof(userId)}: {userId})");

            var result = _organizationService.RemoveAdmin(id, userId);

            _logger.LogInformation($"Request {nameof(RemoveAdmin)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }
        #endregion Administrative Actions
        #endregion
    }
}
