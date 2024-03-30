using ChatApp.Server.Domain;
using ChatApp.Server.Persistence.Core.Abstractions;

namespace ChatApp.Server.App.Startup;

public static class RepositoriesSetup
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(
                AssemblyReference.Assembly,
                Persistence.AssemblyReference.Assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(Repository<>)))
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }
}