using Microsoft.AspNetCore.Http;

namespace Philbor.Domain.Shared
{
    public class MailRequest
    {
        public List<string> ToEmails { get; set; }
        public string? FromEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
        public string? SenderName { get; set; }
    }
}
