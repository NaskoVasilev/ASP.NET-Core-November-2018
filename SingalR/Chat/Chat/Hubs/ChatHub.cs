using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Chat.Hubs
{
    public class ChatHub : Hub
    {
       public async Task SendMessage(string message)
        {
            string user = this.Context.User.Identity.Name;
            await this.Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
