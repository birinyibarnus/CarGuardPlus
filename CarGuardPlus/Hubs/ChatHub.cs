using Microsoft.AspNetCore.SignalR;

namespace CarGuardPlus.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("Recieve Message ",user, message);
        }
    }
}
