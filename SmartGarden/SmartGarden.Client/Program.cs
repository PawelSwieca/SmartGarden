using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using SmartGarden.Client.Authorization;
using Syncfusion.Blazor;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

var syncfusionKey = builder.Configuration["Syncfusion:LicenseKey"];

Console.WriteLine($"[DIAGNOZA] Pobrany klucz: '{syncfusionKey}'");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

using (var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
{
    try
    {
        var response = await http.GetFromJsonAsync<SyncfusionLicenseResponse>("api/config/syncfusion-license");
        if (!string.IsNullOrWhiteSpace(response?.LicenseKey))
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(response.LicenseKey);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"License key could not be received: {ex.Message}");
    }
}

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

public record SyncfusionLicenseResponse(string LicenseKey);