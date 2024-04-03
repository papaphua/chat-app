using ChatApp.Server.Application.Core;
using ChatApp.Server.Application.Core.Abstractions;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ChatApp.Server.Infrastructure.SmsService;

public sealed class SmsService : ISmsService
{
    private readonly SmsOptions _options;

    public SmsService(IOptions<SmsOptions> options)
    {
        _options = options.Value;
        TwilioClient.Init(_options.AccountSid, _options.AuthToken);
    }

    public Task SendMessageAsync(MessageTemplate template)
    {
        return MessageResource.CreateAsync(
            new PhoneNumber(template.Receiver),
            from: new PhoneNumber(_options.FromNumber),
            body: template.Content);
    }
}