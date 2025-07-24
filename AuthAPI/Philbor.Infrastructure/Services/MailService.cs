using Microsoft.Extensions.Options;
using Philbor.Application.Abstractions;
using Philbor.Domain.Shared;
using System.Net;
using System.Net.Mail;

namespace Philbor.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            this.mailSettings = mailSettings.Value;
        }
        public async Task<Result> SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(mailSettings.Host, mailSettings.Port))
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(mailSettings.Mail, mailSettings.Password);

                    // Create email message
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        string displayName = string.IsNullOrEmpty(mailRequest.SenderName)
                            ? "Philbor"
                            : mailRequest.SenderName;

                        mailMessage.From = new MailAddress(mailSettings.Mail, displayName);
                        if (mailRequest.ToEmails != null)
                        {
                            for (int i = 0; i < mailRequest.ToEmails.Count; i++)
                            {
                                if (!string.IsNullOrWhiteSpace(mailRequest.ToEmails[i])) mailMessage.To.Add(mailRequest.ToEmails[i]);
                            }
                        }

                        mailMessage.Subject = mailRequest.Subject;
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Body = mailRequest.Body;
                        if (mailRequest.Attachments != null && mailRequest.Attachments.Count > 0)
                        {
                            foreach (var formFile in mailRequest.Attachments)
                            {
                                if (formFile.Length > 0)
                                {
                                    var stream = formFile.OpenReadStream();
                                    var attachment = new Attachment(stream, formFile.FileName);
                                    mailMessage.Attachments.Add(attachment);
                                }
                            }
                        }

                        // Send email asynchronously
                        await client.SendMailAsync(mailMessage);
                    }
                }

                return Result.Success(message: "Mail sent successfully");
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error("Mail Error", ex.Message));
            }
        }
    }
}
