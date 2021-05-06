using BookSharing.Domain.BookAggregate;
using BookSharing.Infrastructure.BookApi.OpenLibrary;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var responseString = await _openLibraryApi.GetBookByIsbnFromOpenLibraryApi(isbn);
            var book = JObject.Parse(responseString)?.Properties()?.First()?.Value?.ToObject<OpenLibraryBookApiResponse>();

            List<string> isbnToStringList = new List<string> { isbn.ToString() };

            List<string> authorToStringList = book.details.authors.Select(x => x.name.ToString()).ToList();

            DateTime.TryParse(book.details.publish_date, out DateTime publishedDate);

            if (book == null || book.details.isbn_13 == isbnToStringList)
            {
                return null;
            }
           
            return new BookShortInformation(
                Isbn: isbn,
                Autor: authorToStringList,
                Title: book.details.title,
                Description: book.details.description,
                Year: publishedDate.Year,
                ImageUrl: book.thumbnail_url);
        }
    }
}
