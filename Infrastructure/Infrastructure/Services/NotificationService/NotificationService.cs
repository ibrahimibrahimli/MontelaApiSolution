using Application.Abstractions.Services;
using MediatR;

namespace Infrastructure.Services.NotificationService
{
    public class NotificationService : INotification, INotificationService
    {
        public Task SendNotificationAsync(string not)
        {
            throw new NotImplementedException();
        }
    }
}
