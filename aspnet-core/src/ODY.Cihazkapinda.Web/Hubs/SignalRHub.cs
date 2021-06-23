using Microsoft.AspNetCore.SignalR;
using ODY.Cihazkapinda.Customers;
using ODY.Cihazkapinda.MessageData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.MultiTenancy;

namespace ODY.Cihazkapinda.Web.Hubs
{
    public class SignalRHub : Hub
    {
        static public Dictionary<string, string> connections = new Dictionary<string, string>();
        private readonly IMessagesAppService _messagesAppService;
        private readonly ICustomerAppService _customerAppService;
        private readonly ICurrentTenant _currentTenant;
        public SignalRHub(IMessagesAppService messagesAppService,
            ICurrentTenant currentTenant,
            ICustomerAppService customerAppService)
        {
            _messagesAppService = messagesAppService;
            _currentTenant = currentTenant;
            _customerAppService = customerAppService;
        }
        public override async Task OnConnectedAsync()
        {
            //get conntection Id Context.ConnectionId;
            var username = Context.GetHttpContext().Request.Headers["username"];
            var check = connections.ContainsKey(username);
            if (check == false)
            {
                connections.Add(username, Context.ConnectionId);
            }
            else
            {
                connections.Remove(username);
                connections.Add(username, Context.ConnectionId);
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var username = Context.GetHttpContext().Request.Headers["username"];
            var check = connections.ContainsKey(username);
            if (check == true)
            {
                connections.Remove(username);
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string sender, string receive, string message)
        {
            MessagesCreateUpdateDto newMessage = new MessagesCreateUpdateDto
            {
                Sender = sender,
                Receive = receive,
                TenantId = _currentTenant.Id,
                Message = message,
                Status = 0
            };

            if (connections.TryGetValue(receive, out string _receive))
            {
                if (receive == _currentTenant.Name)
                {
                    var result = await _messagesAppService.CreateAsync(newMessage);

                    var list = await _messagesAppService.GetMessageInCustomers();
                    await Clients.Client(_receive).SendAsync("ReceiveMessage", result, list);

                    if (connections.TryGetValue(sender, out string _sender))
                    {
                        await Clients.Client(_sender).SendAsync("OwnMessage", result);
                    }
                }
                else
                {
                    newMessage.Status = 1;
                    var result = await _messagesAppService.CreateAsync(newMessage);

                    await Clients.Client(_receive).SendAsync("ReceiveMessage", result);

                    if (connections.TryGetValue(sender, out string _sender))
                    {
                        await Clients.Client(_sender).SendAsync("OwnMessage", result);
                    }
                }
            }

        }

        public async Task DeleteConnection(string username)
        {
            if (connections.TryGetValue(username, out string _delete))
            {
                await Clients.Client(_delete).SendAsync("DeleteConnectionMessage");
            }
            connections.Remove(username);
        }
        public async Task MessageRead(string username)
        {
            await _messagesAppService.GetLastMessageByUserRead(username);
        }
    }
}
