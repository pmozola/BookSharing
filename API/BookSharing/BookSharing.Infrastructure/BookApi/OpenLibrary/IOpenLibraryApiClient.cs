using Refit;
using System.Threading.Tasks;

namespace BookSharing.Infrastructure.BookApi.OpenLibrary
{
    public interface IOpenLibraryApiClient
    {
        [Get("/api/books?bibkeys=ISBN:{isbn}&jscmd=details&format=json")]
        Task<string> GetBookByIsbnFromOpenLibraryApi(long isbn);
        [Get("/search.json?title={title}")]
        Task<SearchByTitleResult> GetBookByTitleFromOpenLibraryApi(string title);
    }
}
