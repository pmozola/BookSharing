using System.Threading.Tasks;

namespace BookSharing.Infrastructure.Interface
{
    public interface IBookInformationFromExternalSource
    {
        public Task<BookShortInformation> GetBook(long isbn);
    }

    public record BookShortInformation(long Isbn, string Autor, string Title, int Year, string ImageUrl);
}
