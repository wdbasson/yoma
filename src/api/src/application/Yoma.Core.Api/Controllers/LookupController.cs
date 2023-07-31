using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Yoma.Core.Domain.Entity.Models;
using Yoma.Core.Domain.Lookups.Interfaces;
using Yoma.Core.Domain.Lookups.Models;

namespace Yoma.Core.Api.Controllers
{
    [Route("api/v3/lookup")]
    [ApiController]
    public class LookupController : Controller
    {
        #region Class Variables
        private readonly ILogger<UserController> _logger;
        private IValidator<User> _userValidator;
        private readonly IGenderService _genderService;
        private readonly ICountryService _countryService;
        #endregion

        #region Constructor
        public LookupController(
            ILogger<UserController> logger,
            IValidator<User> userValidator,
            IGenderService genderService,
            ICountryService countryService)
        {
            _logger = logger;
            _userValidator = userValidator;
            _genderService = genderService;
            _countryService = countryService;
        }
        #endregion

        #region Public Members
        #region Anonymous Actions
        [SwaggerOperation(Summary = "Return a list of gender's (Anonymous)")]
        [HttpGet("gender")]
        [ProducesResponseType(typeof(List<Gender>), (int)HttpStatusCode.OK)]
        public IActionResult ListGenders()
        {
            _logger.LogInformation($"Handling request {nameof(ListGenders)}");

            var result = _genderService.List();

            _logger.LogInformation($"Request {nameof(ListGenders)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [SwaggerOperation(Summary = "Return a list of countries (Anonymous)")]
        [HttpGet("country")]
        [ProducesResponseType(typeof(List<Country>), (int)HttpStatusCode.OK)]
        public IActionResult ListCountries()
        {
            _logger.LogInformation($"Handling request {nameof(ListCountries)}");

            var result = _countryService.List();

            _logger.LogInformation($"Request {nameof(ListCountries)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }
        #endregion Anonymous Actions
        #endregion 
    }
}
