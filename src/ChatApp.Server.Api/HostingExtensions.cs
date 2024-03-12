namespace ChatApp.Server.Api;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {

        return app;
    }
}