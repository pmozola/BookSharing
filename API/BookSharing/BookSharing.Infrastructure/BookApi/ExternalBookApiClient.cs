using System;
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
            try
            {
                var book = await _bookApi.GetBookByISBN(isbn);
                try
                {
                    Int32.TryParse(book.volumeInfo.publishedDate, out int yearInt);

                    return new BookShortInformation(
                        isbn,
                        Autor: book.volumeInfo.authors,
                        Title: book.volumeInfo.title,
                        Year: yearInt,
                        ImageUrl: book.volumeInfo.imageLinks.ToString());
                }
                catch (Exception ex)
                {
                    _logger.LogError("Date converting error", ex);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ISBN search exception", ex);
                return null;
            }
        }
    }
}
