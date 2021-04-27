using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookSharing.Infrastructure.Interface
{
    public interface IExternalBookApiClient
    {
        public Task<BookShortInformation> GetBook(long isbn);
    }
    public record BookShortInformation(long Isbn, IList<string> Autor, string Title, int Year, string ImageUrl);
}
