using System.Threading.Tasks;
using Refit;

namespace BookSharing.Infrastructure.BookApi
{
    //https://openlibrary.org/isbn/9780140328721.json
    //https://www.googleapis.com/books/v1/volumes?q=9788324626625+terms
    public interface IBookApi
    {
        //[Get("/isbn/{isbn}.json")]
        //Task<BookOpenApiResource> GetBookByISBN(long isbn);

        [Get("/books/v1/volumes?q=/{isbn}/")]
        Task<BookOpenApiResource> GetBookByISBN(long isbn);
    }
}