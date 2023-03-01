namespace MicroserviceMail.Domain
{
    public class Mail
    {
        public int MailId { get; set; }
        public string Owner { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreateAt { get; set; }
        public bool Sent { get; set; }
        public string Status { get; set; }
    }
}
