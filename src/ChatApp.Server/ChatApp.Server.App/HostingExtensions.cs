using ChatApp.Server.Api.Startup;

namespace ChatApp.Server.Api;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews()
            .AddApplicationPart(Presentation.AssemblyReference.Assembly);

        builder.Services.AddRazorPages();
        
        builder.Services.AddSwaggerGen();

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
        app.UseAuthorization();

        app.MapControllers();

        app.MapRazorPages();

        app.MapFallbackToFile("index.html");
            
        return app;
    }
}