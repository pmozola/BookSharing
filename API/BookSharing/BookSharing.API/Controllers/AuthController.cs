using System;
using System.Threading.Tasks;

using BookSharing.Auth.Application.CommandHandlers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookSharing.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthController(ISender sender) => _sender = sender;

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {

            var result = await _sender.Send(command);
            
            return result.Match<IActionResult>(
                success: Ok,
                error: exception => exception switch
                {
                    ArgumentException ex => BadRequest(ex.Message),
                    _ => StatusCode(StatusCodes.Status500InternalServerError)
                });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _sender.Send(command);
            
            return result.Match<IActionResult>(
                success: Ok,
                error: exception => exception switch
                {
                    ArgumentException ex => BadRequest(ex.Message),
                    _ => StatusCode(StatusCodes.Status500InternalServerError)
                });
        }
    }
}
