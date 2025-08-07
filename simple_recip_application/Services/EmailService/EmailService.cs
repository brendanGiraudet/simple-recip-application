using simple_recip_application.Dtos;

namespace simple_recip_application.Services.EmailService;

public class EmailService : IEmailService
{
    public async Task<MethodResult> SendEmailAsync(string email, string message)
    {
        //TODO finir l'implementation
        return new MethodResult(true);
    }
}
