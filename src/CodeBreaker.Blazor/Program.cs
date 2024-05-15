using CodeBreaker.Blazor.Client.Pages;
using Codebreaker.GameAPIs.Client;
using CodeBreaker.Blazor.Components;
using CodeBreaker.Blazor.Client.Extensions;
using CodeBreaker.Blazor.Client.Services;
using CodeBreaker.Blazor.Client.Contracts.Services;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddFluentUIComponents();

builder.Services.AddLocalization();

builder.Services.AddHttpClient<IGamerNameSuggestionClient, GamerNameSuggestionClient>(configure =>
    configure.BaseAddress = new Uri(builder.Configuration.GetRequired("UserApiBase")));

builder.Services.AddHttpClient<IGamesClient, GamesClient>(configure =>
    configure.BaseAddress = new Uri(builder.Configuration.GetRequired("GameApiBase")));

builder.Services.AddScoped<IMobileDetectorService, MobileDetectorService>();

builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseRequestLocalization(new RequestLocalizationOptions()
    .AddSupportedCultures(["de", "en"])
    .AddSupportedUICultures(["de", "en"]));

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(GamePage).Assembly);

app.Run();