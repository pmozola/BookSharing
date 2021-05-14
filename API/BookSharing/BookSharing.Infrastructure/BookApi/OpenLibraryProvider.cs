using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BookSharing.Domain.BookAggregate;
using BookSharing.Infrastructure.BookApi.OpenLibrary;
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

            var book = JObject.Parse(responseString).Properties().First().Value.ToObject<OpenLibraryBookResponse>();


            if (book == null)
            {
                return null;
            }

            List<string> authors = book.details.authors != null ? book.details.authors.Select(x => x.name.ToString()).ToList() : new List<string>();

            _ = DateTime.TryParse(book.details.publish_date, out DateTime publishedDate);

            return new BookShortInformation(
                Isbn: isbn,
                Autor: authors,
                Title: book.details.title,
                Description: book.details.description,
                Year: publishedDate.Year,
                ImageUrl: book.thumbnail_url);

        }

        public Task<BookShortInformation> GetBook(string title)
        {
            throw new NotImplementedException();
        }

        public static bool IsResponseValid(string response)
        {
            return !string.IsNullOrEmpty(response) && !string.Equals(response, "{}", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
