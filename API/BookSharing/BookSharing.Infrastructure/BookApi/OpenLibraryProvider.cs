using BookSharing.Domain.BookAggregate;
using BookSharing.Infrastructure.BookApi.OpenLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSharing.Infrastructure.BookApi
{
    public class OpenLibraryProvider : IExternalBookApiProvider
    {
        private readonly IOpenLibraryApiClient _openLibraryApi;

        public OpenLibraryProvider(IOpenLibraryApiClient openLibraryApi)
        {
            _openLibraryApi = openLibraryApi;
        }

        public async Task<BookShortInformation> GetBook(long isbn)
        {
            var bookResourceList = await _openLibraryApi.GetBookByIsbnFromOpenLibraryApi(isbn);

            List<string> isbnStringList = new List<string> { isbn.ToString() };

            var books = bookResourceList.details;
            List<string> authorsToList = new List<string> { books.authors.name };
            
            if (books == null || books.isbn_13 != isbnStringList)
            {
                return null;
            }

            return new BookShortInformation(
                Isbn: isbn,
                Autor: authorsToList,
                Title: books.title,
                Description: books.description,
                Year: int.TryParse(books.publish_date, out int publishedDate) ? publishedDate : 1900,
                ImageUrl: bookResourceList.info_url);
        }
    }
}
