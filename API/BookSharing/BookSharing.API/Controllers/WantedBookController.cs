using System.Threading.Tasks;
using BookSharing.Application.CommandHandlers.Wanted;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookSharing.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WantedBookController : ControllerBase
    {
        private readonly ISender _sender;

        public WantedBookController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(AddBookToUserWantedCommand command)
        {
            await _sender.Send(command);

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserWantedBookResource[]))]
        public async Task<IActionResult> Get()
        {
            return Ok(await _sender.Send(new GetAllUserWantedBookQuery()));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _sender.Send(new RemoveBookFromUserWantedCommand(id));

            return NoContent();
        }
    }
}
