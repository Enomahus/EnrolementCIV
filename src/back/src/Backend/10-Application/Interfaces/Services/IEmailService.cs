namespace Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendCreatePasswordEmailAsync(string activateLink, string toEmail);

        Task SendForgotPasswordEmailAsync(string resetLink, string toEmail);
        Task ActivateAccountEmail(string? link, string toEmail);
        Task SendResetPasswordEmail(string? link, string toEmail);

        Task SendEndOfConformityEmailAsync(
            string certificateRequestLink,
            string createNewCertificateRequestLink,
            string? toEmail,
            IEnumerable<string> bcc
        );
        Task SendEndOfConformitySoonEmailAsync(
            string certificateRequestLink,
            string? toEmail,
            IEnumerable<string> bcc
        );
        Task SendEndOfVersionEmailAsync(
            string certificateRequestLink,
            string createNewCertificateRequestLink,
            string? toEmail,
            IEnumerable<string> bcc
        );
    }
}
