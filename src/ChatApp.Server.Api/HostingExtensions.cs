using ChatApp.Server.Application.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Users;
using ChatApp.Server.Infrastructure;
using ChatApp.Server.Infrastructure.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Api;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddControllers();

        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(defaultConnection));

        builder.Services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());

        builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.Scan(scan => scan
            .FromAssembliesOf(typeof(Repository<>), typeof(IRepository<>))
            .AddClasses(classes => classes
                .AssignableToAny(typeof(Repository<>)))
            .AsMatchingInterface()
            .WithScopedLifetime());

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}