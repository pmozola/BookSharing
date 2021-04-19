using System.Threading.Tasks;
using Refit;

namespace BookSharing.Infrastructure.BookApi
{
    //https://www.googleapis.com/books/v1/volumes?q=9788324626625+terms
    public interface IGoogleBookApiClient
    {
        [Get("/books/v1/volumes?q=/{isbn}/")]
        Task<VolumeInfo> GetBookByISBN(long isbn);
    }
}