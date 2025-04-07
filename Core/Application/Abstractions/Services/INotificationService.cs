namespace Application.Abstractions.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string not);
    }
}
