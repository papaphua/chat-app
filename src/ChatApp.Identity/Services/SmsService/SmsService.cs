using DotNetEnv;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ChatApp.Identity.Services.SmsService;

public sealed class SmsService : ISmsService
{
    private readonly string _accountSid = Env.GetString("TWILIO_ACCOUNT_SID");
    private readonly string _authToken = Env.GetString("TWILIO_AUTH_TOKEN");
    private readonly string _fromNumber = Env.GetString("TWILIO_FROM_NUMBER");

    public Task SendVerificationTokenAsync(string receiver, string token)
    {
        TwilioClient.Init(_accountSid, _authToken);

        return MessageResource.CreateAsync(
            new PhoneNumber(receiver),
            from: new PhoneNumber(_fromNumber),
            body: $"Your verification code is: {token}");
    }
}