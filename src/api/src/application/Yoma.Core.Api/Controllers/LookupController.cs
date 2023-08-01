using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Yoma.Core.Domain.Lookups.Interfaces;
using Yoma.Core.Domain.Lookups.Models;

namespace Yoma.Core.Api.Controllers
{
    [Route("api/v3/lookup")]
    [ApiController]
    [SwaggerTag("(Anonymous)")]
    public class LookupController : Controller
    {
        #region Class Variables
        private readonly ILogger<UserController> _logger;
        private readonly IGenderService _genderService;
        private readonly ICountryService _countryService;
        private readonly IProviderTypeService _providerTypeService; 
        #endregion

        #region Constructor
        public LookupController(
            ILogger<UserController> logger,
            IGenderService genderService,
            ICountryService countryService,
            IProviderTypeService providerTypeService)
        {
            _logger = logger;
            _genderService = genderService;
            _countryService = countryService;
            _providerTypeService = providerTypeService;
        }
        #endregion

        #region Public Members
        #region Anonymous Actions
        [SwaggerOperation(Summary = "Return a list of genders")]
        [HttpGet("gender")]
        [ProducesResponseType(typeof(List<Gender>), (int)HttpStatusCode.OK)]
        public IActionResult ListGenders()
        {
            _logger.LogInformation($"Handling request {nameof(ListGenders)}");

            var result = _genderService.List();

            _logger.LogInformation($"Request {nameof(ListGenders)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [SwaggerOperation(Summary = "Return a list of countries")]
        [HttpGet("country")]
        [ProducesResponseType(typeof(List<Country>), (int)HttpStatusCode.OK)]
        public IActionResult ListCountries()
        {
            _logger.LogInformation($"Handling request {nameof(ListCountries)}");

            var result = _countryService.List();

            _logger.LogInformation($"Request {nameof(ListCountries)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [SwaggerOperation(Summary = "Return a list of provider types")]
        [HttpGet("providerType")]
        [ProducesResponseType(typeof(List<ProviderType>), (int)HttpStatusCode.OK)]
        public IActionResult ListProviderTypes()
        {
            _logger.LogInformation($"Handling request {nameof(ListProviderTypes)}");

            var result = _providerTypeService.List();

            _logger.LogInformation($"Request {nameof(ListProviderTypes)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        #endregion Anonymous Actions
        #endregion 
    }
}
