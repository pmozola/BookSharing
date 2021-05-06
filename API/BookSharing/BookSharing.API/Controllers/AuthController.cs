using BookSharing.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookSharing.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;

        public AuthController(UserManager<AppUser> userManager) => this.userManager = userManager;


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var claims = new List<Claim>
            {
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yourSecretHashyourSecretHashyourSecretHash"));
               
                var token = new JwtSecurityToken(
                    issuer: "YourApplication",
                    audience: "YourApplication",
                    expires: DateTime.Now.AddDays(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

                return new OkObjectResult(new TokenDetails(
                    Token: new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration: token.ValidTo
                ));
            }
            return new BadRequestObjectResult($"User with name {model.UserName} couldn't be found");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationModel model)
        {

            if (model.Password != model.ConfirmPassword)
            {
                return new BadRequestObjectResult("Passwords do no match");
            }

            if (await userManager.FindByNameAsync(model.UserName) is not null)
            {
                return new BadRequestObjectResult("Username is taken");
            }
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var userCreated = await userManager.CreateAsync(user, model.Password);
            if (!userCreated.Succeeded)
            {
                return new BadRequestObjectResult(string.Join(", ", userCreated.Errors.Select(error => $"{error.Code} {error.Description}")));
            }
            return new OkObjectResult(user);
        }

        public record LoginModel
       (
           [Required(ErrorMessage = "User name is required!")] string UserName,
           [Required(ErrorMessage = "Password is required!")] string Password
       );
        public record RegistrationModel
        (
            [Required(ErrorMessage = "User name is required!")] string UserName,
            [Required(ErrorMessage = "Password is required!")] string Password,
            [Required(ErrorMessage = "Confirm password is required!")] string ConfirmPassword,
            [Required(ErrorMessage = "Email is required!")] string Email
        );
        public record TokenDetails
       (
           string Token,
           DateTime Expiration
       );
    }
}
