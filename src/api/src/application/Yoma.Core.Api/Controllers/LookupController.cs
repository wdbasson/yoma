using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Yoma.Core.Domain.Lookups.Interfaces;
using Yoma.Core.Domain.Lookups.Models;

namespace Yoma.Core.Api.Controllers
{
    [Route("api/v3/lookup")]
    [ApiController]
    [AllowAnonymous]
    [SwaggerTag("(Anonymous)")]
    public class LookupController : Controller
    {
        #region Class Variables
        private readonly ILogger<UserController> _logger;
        private readonly ICountryService _countryService;
        private readonly IGenderService _genderService;
        private readonly ILanguageService _languageService;
        private readonly ISkillService _skillService;
        private readonly ITimeIntervalService _timeIntervalService;
        #endregion

        #region Constructor
        public LookupController(
            ILogger<UserController> logger,
            ICountryService countryService,
            IGenderService genderService,
            ILanguageService languageService,
            ISkillService skillService,
            ITimeIntervalService timeIntervalService
            )
        {
            _logger = logger;
            _countryService = countryService;
            _genderService = genderService;
            _languageService = languageService;
            _skillService = skillService;
            _timeIntervalService = timeIntervalService;
        }
        #endregion

        #region Public Members
        #region Anonymous Actions
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

        [SwaggerOperation(Summary = "Return a list of languages")]
        [HttpGet("language")]
        [ProducesResponseType(typeof(List<Gender>), (int)HttpStatusCode.OK)]
        public IActionResult ListLanguages()
        {
            _logger.LogInformation($"Handling request {nameof(ListLanguages)}");

            var result = _languageService.List();

            _logger.LogInformation($"Request {nameof(ListLanguages)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [SwaggerOperation(Summary = "Return a list of skills")]
        [HttpGet("skill")]
        [ProducesResponseType(typeof(List<Gender>), (int)HttpStatusCode.OK)]
        public IActionResult ListSkills()
        {
            _logger.LogInformation($"Handling request {nameof(ListSkills)}");

            var result = _skillService.List();

            _logger.LogInformation($"Request {nameof(ListSkills)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [SwaggerOperation(Summary = "Return a list of time intervals")]
        [HttpGet("timeInterval")]
        [ProducesResponseType(typeof(List<Gender>), (int)HttpStatusCode.OK)]
        public IActionResult ListTimeIntervals()
        {
            _logger.LogInformation($"Handling request {nameof(ListTimeIntervals)}");

            var result = _timeIntervalService.List();

            _logger.LogInformation($"Request {nameof(ListTimeIntervals)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }
        #endregion Anonymous Actions
        #endregion 
    }
}
