using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BookSharing.Application.QueryHandlers.Books;
using MediatR;
using BookSharing.Domain.Exceptions;

namespace BookSharing.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ISender _sender;

        public BookController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{isbn}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookInformationResource))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(long isbn)
        {
            var result = await _sender.Send(new GetBookQuery(isbn));

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
