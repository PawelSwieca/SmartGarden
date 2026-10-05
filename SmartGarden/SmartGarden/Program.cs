using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SmartGarden.Components;
using SmartGarden.Cookie;
using SmartGarden.Data;
using SmartGarden.Repositories;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSyncfusionBlazor();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGardenRepository, GardenRepository>();
builder.Services.AddScoped<IPlantedCropRepository, PlantedCropRepository>();
builder.Services.AddScoped<IPlantProfileRepository, PlantProfileRepository>();

var connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        connection,
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents(); 

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.AccessDeniedPath = "/access-denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
    });


// Włącza dostęp do IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Rejestruje nasz nowy "przekazywacz ciastek"
builder.Services.AddTransient<ServerCookieHandler>();

// Konfiguruje HttpClienta dla serwera i "zapina" na niego nasz Handler
builder.Services.AddHttpClient("SmartGardenAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7081/");
})
.AddHttpMessageHandler<ServerCookieHandler>();

// Zapewniamy domyślnego klienta
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("SmartGardenAPI"));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SmartGarden.Client._Imports).Assembly);

app.MapControllers();


app.Run();
