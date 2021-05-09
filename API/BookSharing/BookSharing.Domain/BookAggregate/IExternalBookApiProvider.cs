using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSharing.Domain.BookAggregate
{
    public interface IExternalBookApiProvider
    {
        public Task<BookShortInformation> GetBook(long isbn);
        public Task<BookShortInformation> GetBook(string title);
    }

    public record BookShortInformation(long Isbn, IList<string> Autor,  string Title, string Description, int Year, string ImageUrl);
}
