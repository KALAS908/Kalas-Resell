using System.Net.Mail;

namespace OnlineStore.WebApp
{
    public interface IEmailSender
    {
        Task SendDocument(string email, string subject, string message, string path);
        Task SendEmailAsync (string email, string subject, string message);
        Task SendPdf(MailMessage mailMessage);
    }
}
