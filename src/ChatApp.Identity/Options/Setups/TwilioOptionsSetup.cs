using DotNetEnv;
using Microsoft.Extensions.Options;

namespace ChatApp.Identity.Options.Setups;

public sealed class TwilioOptionsSetup : IConfigureOptions<TwilioOptions>
{
    public void Configure(TwilioOptions options)
    {
        options.AccountSid = Env.GetString("TWILIO_ACCOUNT_SID");
        options.AuthToken = Env.GetString("TWILIO_AUTH_TOKEN");
        options.FromNumber = Env.GetString("TWILIO_FROM_NUMBER");
    }
}