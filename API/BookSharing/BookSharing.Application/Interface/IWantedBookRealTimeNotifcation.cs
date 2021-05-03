using System.Threading.Tasks;

namespace BookSharing.Application.Interface
{
    public interface IWantedBookRealTimeNotifcation
    {
        Task SendMessage(int userId, string isbn, string bookTitle);
    }
}
