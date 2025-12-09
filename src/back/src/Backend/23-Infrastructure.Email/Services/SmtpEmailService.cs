using System.Net.Mail;
using Application.Interfaces.Services;
using Infrastructure.Email.Configurations;
using Microsoft.Extensions.Options;

namespace Infrastructure.Email.Services
{
    public class SmtpEmailService(SmtpClient smtpClient, IOptions<EmailConfiguration> config)
        : IEmailService
    {
        private async Task SendEmailAsync(
            string subject,
            string htmlBody,
            string? toEmail,
            IEnumerable<string>? bcc = null
        )
        {
            if (toEmail is null && (bcc is null || bcc.All(bccMail => bccMail is null)))
                return;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(config.Value.FromEmail ?? "", config.Value.FromName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true,
            };

            if (toEmail != null)
            {
                mailMessage.To.Add(toEmail);
            }
            foreach (var bccMail in bcc ?? [])
            {
                mailMessage.Bcc.Add(bccMail);
            }
            await smtpClient.SendMailAsync(mailMessage);
        }

        public Task SendCreatePasswordEmailAsync(string activateLink, string toEmail)
        {
            return this.SendEmailAsync(
                "Account created - create password",
                $"<a href=\"{activateLink}\">{activateLink}</a>",
                toEmail
            );
        }

        public Task SendEndOfConformityEmailAsync(
            string certificateRequestLink,
            string createNewCertificateRequestLink,
            string? toEmail,
            IEnumerable<string> bcc
        )
        {
            return this.SendEmailAsync(
                "End of conformity",
                $"<a href=\"{certificateRequestLink}\">Certificate request : {certificateRequestLink}</a> <a href=\"{createNewCertificateRequestLink}\">Create new request : {createNewCertificateRequestLink}</a>",
                toEmail,
                bcc
            );
        }

        public Task SendEndOfConformitySoonEmailAsync(
            string certificateRequestLink,
            string? toEmail,
            IEnumerable<string> bcc
        )
        {
            return this.SendEmailAsync(
                "End of conformity soon",
                $"<a href=\"{certificateRequestLink}\">Certificate request : {certificateRequestLink}</a>",
                toEmail,
                bcc
            );
        }

        public Task SendEndOfVersionEmailAsync(
            string certificateRequestLink,
            string createNewCertificateRequestLink,
            string? toEmail,
            IEnumerable<string> bcc
        )
        {
            return this.SendEmailAsync(
                "End of version",
                $"<a href=\"{certificateRequestLink}\">Certificate request : {certificateRequestLink}</a> <a href=\"{createNewCertificateRequestLink}\">Create new request : {createNewCertificateRequestLink}</a>",
                toEmail,
                bcc
            );
        }

        public Task SendForgotPasswordEmailAsync(string resetLink, string toEmail)
        {
            return this.SendEmailAsync(
                "Forgot password",
                $"<a href=\"{resetLink}\">{resetLink}</a>",
                toEmail
            );
        }

        public async Task ActivateAccountEmail(string? link, string toEmail)
        {
            var body =
                @$"
            <p>Pour activer votre compte CEI, veuillez cliquer sur ce lien : </p>
            <a href=""{link}"" target=""_blank"">{link}</a>
        ";

            var subject = "Activation de votre compte CEI";

            await SendEmailAsync(body, subject, toEmail);
        }

        public async Task SendResetPasswordEmail(string? link, string toEmail)
        {
            var body =
                @$"
            <p>Pour modifier votre mot de passe, veuillez cliquer sur ce lien : </p>
            <a href=""{link}"" target=""_blank"">{link}</a>
        ";

            var subject = "Modification du mot de passe de votre compte CEI";

            await SendEmailAsync(body, subject, toEmail);
        }
    }
}
