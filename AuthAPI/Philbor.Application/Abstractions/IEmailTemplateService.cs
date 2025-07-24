namespace Philbor.Application.Abstractions
{
    public interface IEmailTemplateService
    {
        string Get2FATemplate(string userName, string twoFAToken);
        string GetInvitationTemplate(string userName, string invitationLink);
        string GetSendPasswordResetLinkTemplate(string userName, string resetLink);
    }
}
