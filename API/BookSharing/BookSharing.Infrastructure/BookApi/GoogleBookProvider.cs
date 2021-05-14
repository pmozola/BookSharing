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
            var bookResourceList = await _bookApi.GetBookByISBN(isbn);

            var book = bookResourceList?.items?.FirstOrDefault(x => x.volumeInfo.industryIdentifiers.Any(x => x.identifier == isbn.ToString()));
            
            if (book == null)
            {
                return null;
            }
            
            return new BookShortInformation(
                Isbn: isbn,
                Autor: book.volumeInfo.authors,
                Title: book.volumeInfo.title,
                Description: book.volumeInfo.description,
                Year: int.TryParse(book.volumeInfo.publishedDate, out var publishedDate) ? publishedDate : 1900,
                ImageUrl: book.volumeInfo.imageLinks?.thumbnail);
        }

        public Task<BookShortInformation> GetBook(string title)
        {
            throw new System.NotImplementedException();
        }
    }
}
