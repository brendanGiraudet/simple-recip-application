using FluentEmail.Core;
using simple_recip_application.Dtos;

namespace simple_recip_application.Services.EmailService;

public class EmailService
    (
        IFluentEmail _fluentEmail,
        ILogger<EmailService> _logger
    )
    : IEmailService
{
    public async Task<MethodResult> SendEmailAsync(string email, string subject, string message)
    {
        try
        {
            await _fluentEmail.To(email)
                              .Body(message, true)
                              .Subject(subject)
                              .SendAsync();

            return new MethodResult(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Erreur lors de l'envoie de l'email à {email} avec les messages {message}");

            return new MethodResult(false);
        }
    }
}
