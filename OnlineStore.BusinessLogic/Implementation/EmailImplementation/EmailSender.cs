using System.Net.Mail;
using System.Net;
namespace OnlineStore.WebApp
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "OnlineStore908@outlook.com";
            var password = "OnlineStorePassword";

            var smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(mail, password),
                EnableSsl = true,
            };

            return smtpClient.SendMailAsync(
                 new MailMessage(from: mail, to: email, subject, message)
                );
        }

        public Task SendPdf(MailMessage mailMessage)
        {

            var mail = "OnlineStore908@outlook.com";
            var password = "OnlineStorePassword";

            var smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(mail, password),
                EnableSsl = true,
            };

            return smtpClient.SendMailAsync(mailMessage);

        }

        public Task SendDocument(string email, string subject, string message, string path)
        {
            var mail = "OnlineStore908@outlook.com";
            var password = "OnlineStorePassword";

            var smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(mail, password),
                EnableSsl = true,
            };

            return smtpClient.SendMailAsync(
                                new MailMessage(from: mail, to: email, subject, message)
                                {
                                    Attachments = { new Attachment(path) }
                                }
                                               );
        }
    }
}
