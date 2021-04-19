using System;
using System.Threading.Tasks;
using BookSharing.Infrastructure.Interface;
using Microsoft.Extensions.Logging;

namespace BookSharing.Infrastructure.BookApi
{
    public class BookApiRetriver : IBookInformationFromExternalSource
    {
        private readonly IGoogleBookApiClient _bookApi;
        private readonly ILogger<BookApiRetriver> _logger;
        public BookApiRetriver(IGoogleBookApiClient bookApi, ILogger<BookApiRetriver> logger)
        {
            this._bookApi = bookApi;
            this._logger = logger;
        }
        public async Task<BookShortInformation> GetBook(long isbn)
        {
            try
            {
                var book = await this._bookApi.GetBookByISBN(isbn);

                return new BookShortInformation(isbn,  Autor:"", Title: "", Year: 250893, ImageUrl: "");
            }
            catch (Exception ex)
            {
                _logger.LogError("ISBN search exception", ex);
                return null;
            }
        }
    }
}
