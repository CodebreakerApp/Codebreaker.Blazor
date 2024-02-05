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

builder.Services.AddHttpClient("ServerAPI", configure =>
    configure.BaseAddress = new Uri(builder.Configuration["ApiBase"] ?? throw new InvalidOperationException("Missing ApiBase configuration")));

builder.Services.AddScoped<IAuthService, DummyAuthService>();
builder.Services.AddScoped<IGameClient, GameClient>();
builder.Services.AddScoped<IGameReportClient, GameClient>();
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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(GamePage).Assembly);

app.Run();