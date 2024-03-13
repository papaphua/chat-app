using ChatApp.Server.Application.Core.Abstractions;
using ChatApp.Server.Domain.Core.Abstractions;
using ChatApp.Server.Domain.Core.Identity;
using ChatApp.Server.Domain.Users;
using ChatApp.Server.Infrastructure;
using ChatApp.Server.Infrastructure.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ChatApp.Server.Api;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddControllers();

        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(defaultConnection));

        builder.Services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());

        builder.Services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserValidator<CustomUserValidator>();

        builder.Services.Scan(scan => scan
            .FromAssembliesOf(typeof(Repository<>), typeof(IRepository<>))
            .AddClasses(classes => classes
                .AssignableToAny(typeof(Repository<>)))
            .AsMatchingInterface()
            .WithScopedLifetime());

        builder.Services.Scan(scan => scan
            .FromAssemblyOf<IUnitOfWork>()
            .AddClasses(classes => classes
                .Where(type => type.IsClass && type.Name.EndsWith("Service")))
            .AsMatchingInterface()
            .WithScopedLifetime());

        builder.Services.AddAutoMapper(options =>
            options.AddMaps(typeof(IUnitOfWork).Assembly));

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        return app;
    }

    public static void LogRegisteredServices(this IServiceCollection services)
    {
        foreach (var service in services)
            Log.Information("Registered service: {ServiceType}", service.ServiceType.Name);
    }
}