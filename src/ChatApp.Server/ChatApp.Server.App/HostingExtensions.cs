﻿using ChatApp.Server.Api.Startup;
using ChatApp.Server.Api.Swagger;

namespace ChatApp.Server.Api;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews()
            .AddApplicationPart(Presentation.AssemblyReference.Assembly);

        builder.Services.AddRazorPages();

        builder.Services.AddBff();

        builder.Services.AddAuth();

        builder.Services.AddSwaggerGen(options =>
        {
            options.OperationFilter<AntiforgeryOperationFilter>();
            options.OperationFilter<CookieOperationFilter>();
        });

        builder.Services.AddEfCore(builder.Configuration);

        builder.Services.AddRepositories();

        builder.Services.AddServices();

        builder.Services.AddAutoMapper(options =>
            options.AddMaps(Application.AssemblyReference.Assembly));

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
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseBff();
        app.UseAuthorization();

        app.MapBffManagementEndpoints();

        app.MapControllers()
            .RequireAuthorization();

        app.MapRazorPages()
            .RequireAuthorization();

        app.MapFallbackToFile("index.html");

        return app;
    }
}