using System.Threading.Tasks;

using BookSharing.Application.CommandHandlers.UserLibrary;
using BookSharing.Application.QueryHandlers.UserLibrary;
using BookSharing.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookSharing.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserLibraryController : ControllerBase
    {
        private readonly ISender _sender;

        public UserLibraryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(AddBookToUserLibraryCommand command)
        {
            await _sender.Send(command);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserBookShortInformation[]))]
        public async Task<IActionResult> Get()
        {
            return Ok(await _sender.Send(new GetAllUserBooksQuery()));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sender.Send(new MarkUserBookAsGivenCommand(id));

            return result.Match<IActionResult>(
                success: Ok,
                error: exception => exception switch
                {
                    NotFoundException => NotFound(),
                    _ => StatusCode(StatusCodes.Status500InternalServerError)
                });
        }
    }
}
