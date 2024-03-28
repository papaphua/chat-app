using DotNetEnv;
using Microsoft.Extensions.Options;

namespace ChatApp.Identity.Options.Setups;

public sealed class SendGridOptionsSetup : IConfigureOptions<SendGridOptions>
{
    public void Configure(SendGridOptions options)
    {
        options.ApiKey = Env.GetString("SENDGRID_APIKEY");
        options.SenderEmail = Env.GetString("SENDGRID_SENDER_EMAIL");
    }
}