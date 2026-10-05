using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using SmartGarden.Client.Authorization;
using Syncfusion.Blazor;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

var syncfusionKey = builder.Configuration["Syncfusion:LicenseKey"];
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncfusionKey);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddTransient<CookieHandler>();
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
}).AddHttpMessageHandler<CookieHandler>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));

builder.Services.AddSyncfusionBlazor();

await builder.Build().RunAsync();
