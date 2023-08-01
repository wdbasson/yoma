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
    [Authorize(Roles = $"{Constants.Role_Admin},{Constants.Role_OrganizationAdmin}")]
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
            _logger.LogInformation($"Handling request {nameof(Upsert)}"); //TODO: new or id

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

        [SwaggerOperation(Summary = "Assign a provider type to the specified organization")]
        [HttpPut("{id}/providerType/{providerTypeId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AssignProviderType([FromRoute] Guid id, [FromRoute] Guid providerTypeId)
        {
            _logger.LogInformation($"Handling request {nameof(AssignProviderType)} ({nameof(id)}: {id} | {nameof(providerTypeId)}: {providerTypeId})");

            await _organizationService.AssignProviderType(id, providerTypeId);

            _logger.LogInformation($"Request {nameof(AssignProviderType)} handled");

            return StatusCode((int)HttpStatusCode.OK);
        }

        [SwaggerOperation(Summary = "Remove a provider type from the specified organization")]
        [HttpDelete("{id}/providerType/{providerTypeId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProviderType([FromRoute] Guid id, [FromRoute] Guid providerTypeId)
        {
            _logger.LogInformation($"Handling request {nameof(DeleteProviderType)} ({nameof(id)}: {id} | {nameof(providerTypeId)}: {providerTypeId})");

            await _organizationService.DeleteProviderType(id, providerTypeId);

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
        #endregion Administrative Actions
        #endregion
    }
}
