using simple_recip_application.Dtos;

namespace simple_recip_application.Services.EmailService;

public interface IEmailService
{
    public Task<MethodResult> SendEmailAsync(string email, string message);
}
