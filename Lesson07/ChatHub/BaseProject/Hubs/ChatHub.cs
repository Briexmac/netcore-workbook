using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace BaseProject.Hubs
{
    public class ChatHub : Hub
    {
        public Task SendMessage(string username, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", username, message);
        }
    }
}
