using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BookSharing.Application.QueryHandlers.Books;
using MediatR;

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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<BookResource>))]
        public async Task<IActionResult> Get()
        {
            var result = await _sender.Send(new GetAllBooksQuery());

            return Ok(result);
        }
    }
}
