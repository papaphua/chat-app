using ChatApp.Identity.Options;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ChatApp.Identity.Services.SmsService;

public sealed class SmsService : ISmsService
{
    private readonly TwilioOptions _options;

    public SmsService(IOptions<TwilioOptions> options)
    {
        _options = options.Value;
        TwilioClient.Init(_options.AccountSid, _options.AuthToken);
    }

    public Task SendVerificationTokenAsync(string receiver, string token)
    {
        return MessageResource.CreateAsync(
            new PhoneNumber(receiver),
            from: new PhoneNumber(_options.FromNumber),
            body: $"Your verification code is: {token}");
    }
}