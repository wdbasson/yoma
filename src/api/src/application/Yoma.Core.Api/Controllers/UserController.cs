using FluentValidation;
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
    [Route("api/v3/user")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        #region Class Variables
        private readonly ILogger<UserController> _logger;
        private IValidator<User> _userValidator;
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public UserController(
            ILogger<UserController> logger,
            IValidator<User> userValidator,
            IUserService userService)
        {
            _logger = logger;
            _userValidator = userValidator;
            _userService = userService;
        }
        #endregion

        #region Public Members
        #region Administrative Actions
        [SwaggerOperation(Summary = "Get the specified user by id (Admin role required)")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [Authorize(Roles = Constants.Role_Admin)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            _logger.LogInformation($"Handling request {nameof(GetById)} ({nameof(id)}: {id}");

            var result = _userService.GetById(id);

            _logger.LogInformation($"Request {nameof(GetById)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }
        #endregion Administrative Actions

        #region Authenticated User Based Actions
        [SwaggerOperation(Summary = "Get the authenticated user")]
        [HttpGet("")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public IActionResult Get()
        {
            _logger.LogInformation($"Handling request {nameof(Get)}");

            var result = _userService.GetByEmail(HttpContext.User.Identity?.Name);

            _logger.LogInformation($"Request {nameof(Get)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [SwaggerOperation(Summary = "Update the authenticated user's profile, within Yoma and Keycloak, optionally requesting a email verification and/or password reset")]
        [HttpPatch()]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileRequest profile)
        {
            _logger.LogInformation($"Handling request {nameof(UpdateProfile)}");

            var result = await _userService.UpdateProfile(HttpContext.User.Identity?.Name, profile);

            _logger.LogInformation($"Request {nameof(UpdateProfile)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }

        [SwaggerOperation(Summary = "Insert or update the authenticated user's profile photo")]
        [HttpPost("image")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpsertPhoto([Required] IFormFile file)
        {
            _logger.LogInformation($"Handling request {nameof(UpsertPhoto)} ({file.Name})");

            var result = await _userService.UpsertPhoto(HttpContext.User.Identity?.Name, file);

            _logger.LogInformation($"Request {nameof(UpsertPhoto)} handled");

            return StatusCode((int)HttpStatusCode.OK, result);
        }
        #endregion Authenticated User Based Actions
        #endregion
    }
}
