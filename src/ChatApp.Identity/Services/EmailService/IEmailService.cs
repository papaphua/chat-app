namespace ChatApp.Identity.Services.EmailService;

public interface IEmailService
{
    Task SendVerificationTokenAsync(string receiver, string token);
}