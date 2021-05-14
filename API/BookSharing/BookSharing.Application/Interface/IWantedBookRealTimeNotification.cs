using System.Threading.Tasks;

namespace BookSharing.Application.Interface
{
    public interface IWantedBookRealTimeNotification
    {
        Task SendMessage(int userId, string isbn, string bookTitle);
    }
}
