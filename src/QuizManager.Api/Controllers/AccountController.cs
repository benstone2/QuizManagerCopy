using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizManager.Core.User;
using QuizManager.Types.Account;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QuizManager.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {

        private readonly SignInManager<QuizManagerUser> _signInManager;
        private readonly UserManager<QuizManagerUser> _userManager;
        private readonly IConfiguration _config;

        public AccountController(SignInManager<QuizManagerUser> signInManager, UserManager<QuizManagerUser> userManager, IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto userDto)
        {
            var user = new QuizManagerUser
            {
                Email = userDto.EmailAddress,
                UserName = userDto.EmailAddress,
                OrganisationId = userDto.OrganisationId
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded == true)
            {
                await _userManager.AddToRoleAsync(user, userDto.Role);

                return Ok(new RegistrationResponse { Success = true });
            }

            return StatusCode((int)HttpStatusCode.BadRequest, "Error registering account");
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInUserDto userDto)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(userDto.EmailAddress, userDto.Password, false, false);

            if (signInResult.Succeeded)
            {
                var identityUser = await _userManager.FindByNameAsync(userDto.EmailAddress);
                var JSONWebTokenAsString = await GenerateJSONWebToken(identityUser);

                return Ok(new AuthenticationResponse
                {
                    IsAuthenticationSuccessful = true,
                    Token = JSONWebTokenAsString
                });
            }

            return Unauthorized(userDto);
        }

        [NonAction]
        [ApiExplorerSettings(IgnoreApi = true)]
        private async Task<string> GenerateJSONWebToken(QuizManagerUser identityUser)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
                new Claim(ClaimTypes.Country, identityUser.OrganisationId.ToString())
            };

            var roleNames = await _userManager.GetRolesAsync(identityUser);
            claims.AddRange(roleNames.Select(rolename => new Claim(ClaimsIdentity.DefaultRoleClaimType, rolename)));

            var jwtSecurityToken = new JwtSecurityToken
            (
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                null,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
