using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookSharing.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserLibraryController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(AddBookToUserLibrary command)
        {
            //TODO provide implementation
            return Ok();
        }

        public record AddBookToUserLibrary(long Isbn, int Rank, string Description);
    }
}
