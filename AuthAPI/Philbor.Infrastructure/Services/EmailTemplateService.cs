using Philbor.Application.Abstractions;

namespace Philbor.Infrastructure.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        public string teamInfo = "Philbor";
        public string Get2FATemplate(string userName, string twoFAToken)
        {
            string template = $@"  <div style=""font-family: Arial, sans-serif; background-color: #f4f4f7; padding: 20px; margin: 0;"">
                              <div style=""max-width: 600px; margin: auto; background-color: #ffffff; border-radius: 8px; padding: 30px; box-shadow: 0 0 10px rgba(0,0,0,0.05);"">
                                <h2 style=""color: #333333;"">🔐 Two-Factor Authentication</h2>
                                <p style=""font-size: 16px; color: #555555;"">
                                  Hello {userName},
                                </p>
                                <p style=""font-size: 16px; color: #555555;"">
                                  Your two-factor authentication code is:
                                </p>

                                <div style=""margin: 30px 0; text-align: center;"">
                                  <span style=""display: inline-block; background-color: #e9f5ff; color: #1a73e8; font-size: 24px; font-weight: bold; padding: 15px 30px; border-radius: 6px;"">
                                    {twoFAToken}
                                  </span>
                                </div>

                                <p style=""font-size: 14px; color: #777777;"">
                                  This code is valid for a limited time. Please do not share it with anyone.
                                </p>

                                <p style=""font-size: 14px; color: #777777;"">
                                  If you did not request this code, please contact support immediately.
                                </p>

                                <p style=""font-size: 14px; color: #999999; margin-top: 40px;"">Thank you,<br/>{teamInfo}</p>
                              </div>
                              </div>";

            return template;
        }

        public string GetInvitationTemplate(string userName, string invitationLink)
        {
            string template = @$"  <div style=""font-family: Arial, sans-serif; background-color: #f4f4f7; padding: 20px; margin: 0;"">
  <div style=""max-width: 600px; margin: auto; background-color: #ffffff; border-radius: 8px; padding: 30px; box-shadow: 0 0 10px rgba(0,0,0,0.05);"">
    <h2 style=""color: #333333;"">📩 You're Invited to Join Us!</h2>

    <p style=""font-size: 16px; color: #555555;"">
      Hello {userName},<br/><br/>
      You have been invited to join our platform. To get started, please accept the invitation and set up your account by clicking the button below:
    </p>

    <div style=""margin: 30px 0; text-align: center;"">
      <a href=""{invitationLink}"" target=""_blank""
         style=""display: inline-block; background-color: #1a73e8; color: #ffffff; text-decoration: none; font-size: 16px; font-weight: bold; padding: 15px 30px; border-radius: 6px;"">
        Accept Invitation
      </a>
    </div>

    <p style=""font-size: 14px; color: #777777;"">
      If you did not expect this invitation, you can safely ignore this email.
    </p>

    <p style=""font-size: 14px; color: #999999; margin-top: 40px;"">Looking forward to having you with us,<br/>{teamInfo}</p>
  </div>
  </div>";

            return template;
        }

        public string GetSendPasswordResetLinkTemplate(string userName, string resetLink)
        {
            string template = $@" <div style=""font-family: Arial, sans-serif; background-color: #f4f4f7; padding: 20px; margin: 0;"">
                  <div style=""max-width: 600px; margin: auto; background-color: #ffffff; border-radius: 8px; padding: 30px; box-shadow: 0 0 10px rgba(0,0,0,0.05);"">
                    <h2 style=""color: #333333;"">🔐 Reset Your Password</h2>

                    <p style=""font-size: 16px; color: #555555;"">
                      Hello {userName},
                    </p>

                    <p style=""font-size: 16px; color: #555555;"">
                      We received a request to reset your password. Click the button below to set a new password for your account:
                    </p>

                    <div style=""margin: 30px 0; text-align: center;"">
                      <a href=""{resetLink}"" target=""_blank""
                         style=""display: inline-block; background-color: #1a73e8; color: #ffffff; text-decoration: none; font-size: 16px; font-weight: bold; padding: 15px 30px; border-radius: 6px;"">
                        Reset Password
                      </a>
                    </div>

                    <p style=""font-size: 14px; color: #777777;"">
                      If you did not request a password reset, please ignore this email. No changes will be made to your account unless you follow the link above.
                    </p>

                    <p style=""font-size: 14px; color: #999999; margin-top: 40px;"">Stay secure,<br/>{teamInfo}</p>
                  </div>
                  </div>";

            return template;
        }
    }
}
