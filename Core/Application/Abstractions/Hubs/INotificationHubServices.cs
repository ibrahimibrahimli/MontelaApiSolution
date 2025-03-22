namespace Application.Abstractions.Hubs
{
    public interface INotificationHubServices
    {
        Task SendNotification(string message); 
    }
}
