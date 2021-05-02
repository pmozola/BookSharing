using System.Threading.Tasks;
using BookSharing.Application.Interface;
using Microsoft.AspNetCore.SignalR;

namespace BookSharing.API.SingnalRHubs
{
    public class WantedBooksHub : Hub
    {
        public async Task SendMessage(int userId, string isbn, string title)
        {
            await Clients.User(userId.ToString()).SendAsync("WantedBookAdded", isbn, title);
        }
    }

    public class WantedBookRealTimeNotifcation : IWantedBookRealTimeNotifcation
    {
        private readonly IHubContext<WantedBooksHub> _hubContext;

        public WantedBookRealTimeNotifcation(IHubContext<WantedBooksHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public  Task SendMessage(int userId, string isbn, string bookTitle)
        {
            return _hubContext.Clients.User(userId.ToString()).SendAsync("WantedBookAdded", isbn, bookTitle);
        }
    }
}
