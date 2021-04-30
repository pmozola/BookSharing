using System;
using System.Linq;
using System.Threading.Tasks;
using BookSharing.Infrastructure.Interface;
using Microsoft.Extensions.Logging;

namespace BookSharing.Infrastructure.BookApi
{
    public class ExternalBookApiClient : IExternalBookApiClient
    {
        private readonly IGoogleBookApiClient _bookApi;
        private readonly ILogger<ExternalBookApiClient> _logger;
        public ExternalBookApiClient(IGoogleBookApiClient bookApi, ILogger<ExternalBookApiClient> logger)
        {
            _bookApi = bookApi;
            _logger = logger;
        }
        public async Task<BookShortInformation> GetBook(long isbn)
        {
            var bookResourceList = await _bookApi.GetBookByISBN(isbn);

            var books = bookResourceList.items.Where(x => x.volumeInfo.industryIdentifiers.Any(x => x.identifier == isbn.ToString()));
            if (books == null || !books.Any())
            {
                return null;
            }

            Int32.TryParse(books.First().volumeInfo.publishedDate, out int publishedDate);

            return new BookShortInformation(
                Isbn: isbn,
                Autor: books.First().volumeInfo.authors,
                Title: books.First().volumeInfo.title,
                Year: publishedDate,
                ImageUrl: books.First().volumeInfo.imageLinks.thumbnail);
        }
    }
}
