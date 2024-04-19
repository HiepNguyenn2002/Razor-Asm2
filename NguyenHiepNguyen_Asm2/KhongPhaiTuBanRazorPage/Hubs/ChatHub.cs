using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;

namespace KhongPhaiTuBanRazorPage.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task SendMessage(string connectionId, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", connectionId, message);
        }
    }
}
