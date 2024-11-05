using System.Net.Mail;
using EmailService.Models;
using Microsoft.Extensions.Options;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailSender> _logger;
        public EmailSender(ILogger<EmailSender> logger, IOptions<EmailSettings> emailSettings)
        {
            _logger = logger;
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmail(EmailData emailData)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient(_emailSettings.MailServer);

                MailMessage mailMessage = BuildMailMessage(emailData);
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private MailMessage BuildMailMessage(EmailData emailData)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(_emailSettings.SenderEmail);

            if (emailData.ToRecipients != null && emailData.ToRecipients.Length > 0)
            {
                foreach (string toEmail in emailData.ToRecipients)
                {
                    mailMessage.To.Add(toEmail);
                }
            }

            if (emailData.CCRecipients != null && emailData.CCRecipients.Length > 0)
            {
                foreach (string ccEmail in emailData.CCRecipients)
                {
                    mailMessage.CC.Add(ccEmail);
                }
            }

            mailMessage.Subject = emailData.Subject;

            mailMessage.Body = emailData.Body;

            return mailMessage;
        }
    }
}
