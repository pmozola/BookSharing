using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookSharing.Domain.BookAggregate;
using BookSharing.Infrastructure.BookApi.OpenLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            if (!IsResponseValid(responseString))
            {
                return null;
            }

            var book = JObject.Parse(responseString)?.Properties()?.First()?.Value?.ToObject<OpenLibraryBookResponse>();

            if (book == null)
            {
                return null;
            }

            List<string> authors = book.details.authors != null ? book.details.authors.Select(x => x.name.ToString()).ToList() : new List<string>();

            DateTime.TryParse(book.details.publish_date, out DateTime publishedDate);

            return new BookShortInformation(
                Isbn: isbn,
                Autor: authors,
                Title: book.details.title,
                Description: book.details.description,
                Year: publishedDate.Year,
                ImageUrl: book.thumbnail_url);
        }

        public async Task<BookShortInformation> GetBookByTitle(string title)
        {
            var responseString = await _openLibraryApi.GetBookByTitleFromOpenLibraryApi(title);

            if (!IsResponseValid(responseString))
            {
                return null;
            }

            var book = JObject.Parse(responseString)?.Properties()?.First()?.Value?.ToObject<OpenLibraryBookResponseByTitle>();

            if (book == null)
            {
                return null;
            }

            return new BookShortInformation(
               Isbn: book.docs.isbn.First(),
               Autor: book.docs.author_name,
               Title: book.docs.title,
               Description: book.docs.text.First(),
               Year: book.docs.first_publish_year,
               ImageUrl: book.docs.key);
        }

        public static bool IsResponseValid(string response)
        {
            return !string.IsNullOrEmpty(response) && !string.Equals(response, "{}", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
