using ChatApp.Server.Persistence;

namespace ChatApp.Server.App.Startup;

public static class ServicesSetup
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(
                Application.AssemblyReference.Assembly,
                AssemblyReference.Assembly)
            .AddClasses(classes => classes
                .Where(type => type.IsClass && type.Name.EndsWith("Service")))
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }
}