using System.Threading.Tasks;

namespace BookSharing.Infrastructure.Interface
{
    public interface IBookService
    {
        public Task<BookView> GetBook(long isbn);
    }

    public record BookView(long Isbn, string Autor, string Title, string Year, string ImageUrl);
}
