using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ChatApp.Client.App;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();

builder.Services.AddHttpClient("server", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("server"));

await builder.Build().RunAsync();