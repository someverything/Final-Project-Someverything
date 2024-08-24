using Business.Abstract;
using Business.Messages;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

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

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var result = await _authService.LoginAsync(model);
            if (result.Success) return Ok();
            return BadRequest(result);
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshTokenLoginAsync([FromBody] RefreshTokenDTO refreshTokenDTO)
        {
            var result = await _authService.RefreshTokenLoginAsync(refreshTokenDTO.RefreshToken);
            if (result.Success) return Ok();
            return BadRequest(result);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _authService.Logout(userId);
            if (result.Success) return Ok();
            return BadRequest(result);
        }


    }
}
