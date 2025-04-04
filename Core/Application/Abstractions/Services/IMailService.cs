namespace Application.Abstractions.Services
{
    public interface IMailService
    {
        Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendMessageAsync(List<string> to, string subject, string body, bool isBodyHtml = true);
    }
}
