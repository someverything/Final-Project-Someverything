using Business.Abstract;
using Business.Messages;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IStringLocalizer<AuthMessages> _localizer;

        public AuthController(IAuthService authService, IStringLocalizer<AuthMessages> localizer)
        {
            _authService = authService;
            _localizer = localizer;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var result = await _authService.RegisterAsync(model);
            if (result.Success) return Ok();
            return BadRequest(result);
        }

    }
}
