using ChatApp.Server.Infrastructure.EmailService;
using ChatApp.Server.Infrastructure.SmsService;
using DotNetEnv;

namespace ChatApp.Server.App.Startup;

public static class OptionsSetup
{
    public static IServiceCollection SetupOptions(this IServiceCollection services)
    {
        services.Configure<EmailOptions>(options =>
        {
            options.ApiKey = Env.GetString("SENDGRID_APIKEY");
            options.SenderEmail = Env.GetString("SENDGRID_SENDER_EMAIL");
        });

        services.Configure<SmsOptions>(options =>
        {
            options.AccountSid = Env.GetString("TWILIO_ACCOUNT_SID");
            options.AuthToken = Env.GetString("TWILIO_AUTH_TOKEN");
            options.FromNumber = Env.GetString("TWILIO_FROM_NUMBER");
        });
        
        return services;
    }
}