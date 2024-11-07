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
                using (SmtpClient smtpClient = new SmtpClient(_emailSettings.MailServer))
                {
                    MailMessage mailMessage = BuildMailMessage(emailData);
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while sending the email: {ex.Message}");
                throw;
            }
        }

        private MailMessage BuildMailMessage(EmailData emailData)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail),
                Subject = emailData.Subject,
                Body = emailData.Body
            };

            AddEmailAddresses(mailMessage.To, emailData.ToRecipients);
            AddEmailAddresses(mailMessage.CC, emailData.CCRecipients);

            return mailMessage;
        }

        private void AddEmailAddresses(MailAddressCollection addressCollection, string[] emailAddresses)
        {
            if (emailAddresses != null && emailAddresses.Length > 0)
            {
                foreach (var email in emailAddresses)
                {
                    addressCollection.Add(email);
                }
            }
        }
    }
}
