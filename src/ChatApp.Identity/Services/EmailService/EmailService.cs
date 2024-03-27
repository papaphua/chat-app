using DotNetEnv;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ChatApp.Identity.Services.EmailService;

public sealed class EmailService : IEmailService
{
    private readonly SendGridClient _client = new(Env.GetString("SENDGRID_APIKEY"));
    private readonly string _fromEmail = Env.GetString("SENDGRID_SENDER_EMAIL");

    public async Task SendVerificationTokenAsync(string receiver, string token)
    {
        var message = new SendGridMessage
        {
            From = new EmailAddress(_fromEmail),
            Subject = "Verification",
            PlainTextContent = $"Your verification code is: {token}"
        };

        message.AddTo(new EmailAddress(receiver));

        await _client.SendEmailAsync(message);
    }
}