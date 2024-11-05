namespace EmailService.Models
{
    public class EmailSettings
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string MailUsername { get; set; }
        public string MailPassword { get; set; }
        public string SenderEmail { get; set; }
    }

    public class EmailData
    {
        public string[] ToRecipients { get; set; }
        public string[] CCRecipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
