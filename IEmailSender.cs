using EmailService.Models;

namespace EmailService
{
    public interface IEmailSender
    {
        Task SendEmail(EmailData emailData);
    }
}
