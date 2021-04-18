using System;
using System.Threading.Tasks;
using BookSharing.Infrastructure.Interface;

namespace BookSharing.Infrastructure.BookApi
{
    public class BookApiRetriver : IBookService
    {
        private readonly IBookApi _bookApi;

        public BookApiRetriver(IBookApi bookApi)
        {
            this._bookApi = bookApi;
        }
        public async Task<BookView> GetBook(long isbn)
        {
            try
            {
                var book = await this._bookApi.GetBookByISBN(isbn);
              
                return new BookView(isbn, "", "","", "");
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}
