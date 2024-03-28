using ChatApp.Identity.Options;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ChatApp.Identity.Services.EmailService;

public sealed class EmailService(IOptions<SendGridOptions> options) : IEmailService
{
    private readonly SendGridOptions _options = options.Value;
    private readonly SendGridClient _client = new(options.Value.ApiKey);

    public async Task SendVerificationTokenAsync(string receiver, string token)
    {
        var message = new SendGridMessage
        {
            From = new EmailAddress(_options.SenderEmail),
            Subject = "Verification",
            PlainTextContent = $"Your verification code is: {token}"
        };

        message.AddTo(new EmailAddress(receiver));

        await _client.SendEmailAsync(message);
    }
}