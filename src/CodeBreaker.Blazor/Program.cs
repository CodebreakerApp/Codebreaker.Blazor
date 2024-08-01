using CodeBreaker.Blazor.Client.Pages;
using Codebreaker.GameAPIs.Client;
using CodeBreaker.Blazor.Components;
using CodeBreaker.Blazor.Client.Services;
using CodeBreaker.Blazor.Client.Contracts.Services;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(); // Aspire

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddFluentUIComponents();

builder.Services.AddLocalization();

builder.Services.AddHttpClient<IGamerNameSuggestionClient, GamerNameSuggestionClient>(configure =>
    configure.BaseAddress =
        new Uri("https://userapis") // Utilize Aspire service discovery
    );

builder.Services.AddHttpClient<IGamesClient, GamesClient>(configure =>
    configure.BaseAddress = new Uri("https://gameapis") // Utilize Aspire service discovery
);

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

app.MapDefaultEndpoints(); // Aspire
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(GamePage).Assembly);

/// <summary>
/// Reads the environment variables for services and returns a mapping of service names to URLs.
/// </summary>
app.MapGet("/service-discovery", () =>
{
    var serviceMapping = new Dictionary<string, string>();
    var environmentVariables = Environment.GetEnvironmentVariables();

    foreach (var key in environmentVariables.Keys)
    {
        var keyText = key.ToString()!;

        // Check if the key is a service URL (e.g. services__gameapis__http)
        if (keyText.StartsWith("services__"))
        {
            // Extract the service name from the key (e.g. gameapis
            var serviceName = keyText.Split("__")[1];

            // Add the service name and URL to the mapping if it doesn't already exist
            if (!serviceMapping.ContainsKey(serviceName))
                serviceMapping.Add(serviceName, environmentVariables[key]!.ToString()!);
        }
    }

    return Results.Json(serviceMapping);
});

app.Run();