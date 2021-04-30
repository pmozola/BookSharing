using System.Threading.Tasks;
using Refit;

namespace BookSharing.Infrastructure.BookApi.Google
{
    public interface IGoogleBookApiClient
    {
        [Get("/books/v1/volumes?q=isbn:{isbn}")]
        Task<GoogleBookApiResponse> GetBookByISBN(long isbn);
    }
}