using System.Collections.Generic;
using System.Threading.Tasks;

using BookSharing.Application.CommandHandlers.UserLibrary;
using BookSharing.Application.QueryHandlers.UserLibrary;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserBookShortInformation>))]
        public async Task<IActionResult> Get()
        {
            return Ok(await _sender.Send(new GetAllUserBooksQuery()));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _sender.Send(new MarkUserBookAsGivenCommand(id)));
        }
    }
}
