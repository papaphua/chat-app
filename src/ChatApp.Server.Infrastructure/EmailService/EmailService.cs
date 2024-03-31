using ChatApp.Server.Application.Core;
using ChatApp.Server.Application.Core.Abstractions;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ChatApp.Server.Infrastructure.EmailService;

public sealed class EmailService(IOptions<EmailOptions> options) : IEmailService
{
    private readonly SendGridClient _client = new(options.Value.ApiKey);
    private readonly EmailOptions _options = options.Value;

    public async Task SendMessageAsync(MessageTemplate template)
    {
        var message = new SendGridMessage
        {
            From = new EmailAddress(_options.SenderEmail),
            PlainTextContent = template.Content
        };
        message.AddTo(new EmailAddress(template.Receiver));

        await _client.SendEmailAsync(message);
    }
}