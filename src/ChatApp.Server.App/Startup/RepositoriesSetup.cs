using ChatApp.Server.Domain;
using ChatApp.Server.Infrastructure.Core.Abstractions;

namespace ChatApp.Server.Api.Startup;

public static class RepositoriesSetup
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan
            .FromAssemblies(
                AssemblyReference.Assembly,
                Infrastructure.AssemblyReference.Assembly)
            .AddClasses(classes => classes
                .AssignableToAny(typeof(Repository<>)))
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }
}