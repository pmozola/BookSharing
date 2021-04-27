using System.Threading.Tasks;
using Refit;

namespace BookSharing.Infrastructure.BookApi
{
    public interface IGoogleBookApiClient
    {
        [Get("/books/v1/volumes?q=/{isbn}/")]
        Task<BookOpenApiResource> GetBookByISBN(long isbn);
    }
}