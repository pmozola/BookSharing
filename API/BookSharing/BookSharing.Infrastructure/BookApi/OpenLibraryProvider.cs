using BookSharing.Domain.BookAggregate;
using BookSharing.Infrastructure.BookApi.OpenLibrary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            
            var book = JsonConvert.DeserializeObject<Root>(responseString);
            var book2 = JObject.Parse(responseString)?.Properties()?.First()?.Value?.ToObject<Root>();
            //List<string> isbnStringList = new List<string> { isbn.ToString() };

            //var books = responseString.details;
            //List<string> authorsToList = new List<string> { books.authors.name };

            //if (books == null || books.isbn_13 != isbnStringList)
            //{
            //    return null;
            //}
            return null;
            //return new BookShortInformation(
            //    Isbn: isbn,
            //    Autor: authorsToList,
            //    Title: books.title,
            //    Description: books.description,
            //    Year: int.TryParse(books.publish_date, out int publishedDate) ? publishedDate : 1900,
            //    ImageUrl: responseString.info_url);
        }
    }
}
