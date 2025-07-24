using Philbor.Domain.Shared;

namespace Philbor.Application.Abstractions
{
    public interface IMailService
    {
        Task<Result> SendEmailAsync(MailRequest mailRequest);
    }
}
