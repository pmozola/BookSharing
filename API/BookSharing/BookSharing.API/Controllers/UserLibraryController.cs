using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserBookResource>))]
        public async Task<IActionResult> Get()
        {
            //TODO provide implementation
            return Ok(GetUserBook().ToList());
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            //TODO provide implementation
            return Ok();
        }

        private static IEnumerable<UserBookResource> GetUserBook()
        {
            return new UserBookResource[]
            {
                new UserBookResource(1, "Wzorce Projektowe", "https://blog.artmetic.pl/wp-content/uploads/2015/11/Wzorce-Projektowe-794x1024.jpg", false),
                new UserBookResource(2, "Domain-Driven Design", "https://www.bbc.co.uk/staticarchive/a0ef83ea6587a3161de39009344958289a6f7353.jpg",true),
                new UserBookResource(8, "Nesbo", "https://cdn-lubimyczytac.pl/upload/books/240000/240113/335647-352x500.jpg",true),
                new UserBookResource(11, "Piknik na straju drogi","https://cdn-lubimyczytac.pl/upload/books/4943000/4943173/882012-352x500.jpg",false),
                new UserBookResource(66, "Robert Lewandowski", "https://cdn-lubimyczytac.pl/upload/books/307000/307877/485807-352x500.jpg",false),
                new UserBookResource(777, "Ola ma kota","https://cdn-lubimyczytac.pl/upload/books/4959000/4959474/883572-352x500.jpg",true),
            };
        }

        public record AddBookToUserLibrary(long Isbn, int Rank, string Description);
        public record UserBookResource(int Id, string Title, string ImageUrl, bool IsAnyConversation);
    }
}
