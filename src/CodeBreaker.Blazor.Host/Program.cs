using CodeBreaker.Blazor.Host.Components;
using CodeBreaker.Blazor.Pages;
using CodeBreaker.Services.Authentication;
using CodeBreaker.Services;
using CodeBreaker.UI;
using CodeBreaker.UI.Services.Dialog;
using CodeBreaker.Blazor.Host.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddLocalization();
builder.Services.AddCodeBreakerUI();

builder.Services.AddHttpClient("GameApi", configure =>
    configure.BaseAddress = new Uri(builder.Configuration.GetRequired("ApiBase")));

builder.Services.AddScoped<IAuthService, DummyAuthService>();
builder.Services.AddHttpClient<IGameClient, GameClient>("GameApi");
builder.Services.AddHttpClient<IGameReportClient, GameClient>("GameApi");
builder.Services.AddScoped<IDialogService, DialogService>();

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