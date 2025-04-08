namespace Application.Abstractions.Services
{
    public interface IMailService
    {
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendMailAsync(List<string> to, string subject, string body, bool isBodyHtml = true);
        Task SendResetPasswordMailAsync(string to, string userId, string resetToken);
    }
}
