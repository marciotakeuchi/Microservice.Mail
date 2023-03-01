using System.ComponentModel.DataAnnotations;

namespace MicroserviceMail.ViewModel
{
    public class MailViewModel
    {
        [Required]
        public string Owner { get; set; }
        [Required]
        [EmailAddress]
        public string From { get; set; }
        [Required]
        public string To { get; set; }
        public string? Cc { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        public string? Status { get; set; }
    }
}
