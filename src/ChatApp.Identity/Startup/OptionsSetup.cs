using ChatApp.Identity.Options.Setups;

namespace ChatApp.Identity.Startup;

public static class OptionsSetup
{
    public static IServiceCollection SetupOptions(this IServiceCollection services)
    {
        services.ConfigureOptions<SendGridOptionsSetup>();
        services.ConfigureOptions<TwilioOptionsSetup>();
        
        return services;
    }
}