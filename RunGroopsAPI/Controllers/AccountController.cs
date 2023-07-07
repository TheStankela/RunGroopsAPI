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
using RunGroops.Domain.EFModels;
using System.Web;
using RunGroops.Domain.Enum;

namespace RunGroopsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IJWTService _jwtService;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config, RoleManager<IdentityRole> roleManager, IJWTService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequest userLoginRequest)
        {
            var userFromDb = await _userManager.FindByEmailAsync(userLoginRequest.Email);

            if (userFromDb is null)
            {
                return BadRequest("User does not exist");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(userFromDb, userLoginRequest.Password, false);

            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            var roles = await _userManager.GetRolesAsync(userFromDb);

            IList<Claim> claims = await _userManager.GetClaimsAsync(userFromDb);

            return Ok(new
            {
                result = result,
                username = userFromDb.UserName,
                email = userFromDb.Email,
                token = _jwtService.GenerateToken(userFromDb, roles, claims)
            });
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterRequest userRegisterModel)
        {
            var userToCreate = new AppUser
            {
                Email = userRegisterModel.Email,
                UserName = userRegisterModel.Email
            };

            //Create User
            var result = await _userManager.CreateAsync(userToCreate, userRegisterModel.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userToCreate, UserRoles.User);

                //var userFromDb = await _userManager.FindByEmailAsync(userToCreate.Email);

                //var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);

                //var uriBuilder = new UriBuilder(_config["ReturnPaths:ConfirmEmail"]);
                //var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                //query["token"] = token;
                //query["userid"] = userFromDb.Id;
                //uriBuilder.Query = query.ToString();
                //var urlString = uriBuilder.ToString();

                //var senderEmail = _config["ReturnPaths:SenderEmail"];

                //await _emailSender.SendEmailAsync(senderEmail, userFromDb.Email, "Confirm your email address", urlString);

                //var claim = new Claim("JobTitle", model.JobTitle);

                //await _userManager.AddClaimAsync(userFromDb, claim);
                return Ok(result);

            }
            return BadRequest(result);
        }
        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Register()
        {
            await _signInManager.SignOutAsync();

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

