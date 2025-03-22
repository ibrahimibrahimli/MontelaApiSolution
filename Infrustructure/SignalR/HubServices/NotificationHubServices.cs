using Application.Abstractions.Hubs;
using Microsoft.AspNetCore.SignalR;
using SignalR.Hubs;

namespace SignalR.HubServices
{
    public class NotificationHubServices : INotificationHubServices
    {
        readonly IHubContext<ProductHub> _hubContext;

        public NotificationHubServices(IHubContext<ProductHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task SendNotification(string message)
        {
            throw new NotImplementedException();
        }
    }
}
