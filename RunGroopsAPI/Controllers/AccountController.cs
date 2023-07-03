using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunGroops.Application.Models;
using RunGroops.Application.Services;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace RunGroopsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequest userLoginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Login(userLoginRequest);
            if(result.isSuccess)
                return Ok(new { Message = result.Message });

            return BadRequest(new { Message = result.Message });
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterRequest userRegisterModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Register(userRegisterModel);

            if (result.isSuccess)
                return Ok(new { result.Message });

            return BadRequest(new {result.Message});
        }
        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Register()
        {
            if (!await _authService.Logout())
                return BadRequest(ModelState);

            return Ok(new { Message = "Logout successfull!" });
        }
        [HttpGet("Google")]
        public async Task GoogleLogin()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
            {
                RedirectUri = Url.RouteUrl("GoogleResponse")
            });

        }
        [HttpGet("GoogleResponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (!result.Succeeded)
                return BadRequest();

            if (result.Principal.Identities.Count() > 0)
            {
                var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

                return Ok(claims);
            }

                return BadRequest();
        }
        [HttpGet("FetchUser")]
        public async Task<IActionResult> FetchUser()
        {
            var result = HttpContext.User.Claims.Select(claim => new
            {
                claim.Type,
                claim.Value,
            });

            return Ok(new { result });
        }
    }
}

