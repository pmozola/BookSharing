using System.Linq;
using System.Threading.Tasks;
using BookSharing.Domain.BookAggregate;
using BookSharing.Infrastructure.BookApi.Google;

namespace BookSharing.Infrastructure.BookApi
{
    public class GoogleBookProvider : IExternalBookApiProvider
    {
        private readonly IGoogleBookApiClient _bookApi;
        public GoogleBookProvider(IGoogleBookApiClient bookApi)
        {
            _bookApi = bookApi;
        }

        public async Task<BookShortInformation> GetBook(long isbn)
        {
            var bookResourceList = await _bookApi.GetBookByIsbnFromGoogleApi(isbn);

            var books = bookResourceList?.items?.Where(x => x.volumeInfo.industryIdentifiers.Any(x => x.identifier == isbn.ToString()));
            
            if (books == null || !books.Any())
            {
                return null;
            }

            return new BookShortInformation(
                Isbn: isbn,
                Autor: books.First().volumeInfo.authors,
                Title: books.First().volumeInfo.title,
                Description: books.First().volumeInfo.description,
                Year: int.TryParse(books.First().volumeInfo.publishedDate, out int publishedDate) ? publishedDate : 1900,
                ImageUrl: books.First().volumeInfo.imageLinks?.thumbnail);
        }
    }
}
