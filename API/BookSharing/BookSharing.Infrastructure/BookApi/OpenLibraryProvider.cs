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
            var response = await _openLibraryApi.GetBookByTitleFromOpenLibraryApi(title);

            //TODO Tutaj musi byc zwracana lista, zeby uzytkownik mogl wybrac sobie ksiazke
            if (response != null && response.numFound > 0)
            {
                return new BookShortInformation(
                   Isbn: long.Parse(response.docs.First().isbn.First()), //TODO  to trzeba poprawic, dlaczego tam jest wiele isbn ?(isbn 13 i isbn 9? -> trzeba brac 13) jak wybrac dobry ?
                   Autor: response.docs.First().author_name,
                   Title: response.docs.First().title,
                   Description: response.docs.First().text.First(), //TODO nie ma opisu ? trzeba wywalic ?
                   Year: response.docs.First().first_publish_year,
                   ImageUrl: PreviewImageGenerator(response.docs.First().isbn.FirstOrDefault(x => x.Length == 13)); ;
            }

            throw new ArgumentException("jakis tam blad");
        }

        public static bool IsResponseValid(string response)
        {
            return !string.IsNullOrEmpty(response) && !string.Equals(response, "{}", StringComparison.InvariantCultureIgnoreCase);
        }

        private static string PreviewImageGenerator(string isbn)
        {
            return $"http://covers.openlibrary.org/b/isbn/{isbn}-M.jpg";
        }
    }
}
