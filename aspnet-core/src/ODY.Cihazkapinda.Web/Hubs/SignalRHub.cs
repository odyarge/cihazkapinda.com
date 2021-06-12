using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODY.Cihazkapinda.Web.Hubs
{
    public class SignalRHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("BroadcastMessage", user, message);
        }
    }
}
