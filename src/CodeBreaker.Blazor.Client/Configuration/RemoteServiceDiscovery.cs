using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;

namespace CodeBreaker.Blazor.Client.Configuration;

/// <summary>
/// A configuration provider that retrieves the URL of the gateway service from the remote Blazor application.
/// </summary>
internal class RemoteServiceDiscovery(Uri baseAddress) : ConfigurationProvider
{
    /// <summary>
    /// Loads the configuration by retrieving the URLs from the remote service discovery endpoint.
    /// </summary>
    public override async void Load()
    {
        using var httpClient = new HttpClient()
        {
            BaseAddress = baseAddress
        };
        var urls = await httpClient.GetFromJsonAsync<IDictionary<string, string>>("service-discovery");

        if (urls is null)
            return;

        foreach (var (key, value) in urls)
            Set($"services:{key}", value);
    }
}

internal class RemoteServiceDiscoveryConfigurationSource(Uri baseAddress) : IConfigurationSource
{
    /// <summary>
    /// Builds the configuration provider using the specified configuration builder.
    /// </summary>
    public IConfigurationProvider Build(IConfigurationBuilder builder) =>
        new RemoteServiceDiscovery(baseAddress);
}

internal static class RemoteServiceDiscoveryConfigurationExtensions
{
    /// <summary>
    /// Adds the remote service discovery configuration provider to the configuration builder with the specified base address.
    /// </summary>
    public static IConfigurationBuilder AddRemoteServiceDiscovery(this IConfigurationBuilder builder, Uri baseAddress)
    {
        builder.Add(new RemoteServiceDiscoveryConfigurationSource(baseAddress));
        return builder;
    }

    /// <summary>
    /// Adds the remote service discovery configuration provider to the configuration builder with the base address from the specified host environment.
    /// </summary>
    public static IConfigurationBuilder AddRemoteServiceDiscovery(this IConfigurationBuilder builder, IWebAssemblyHostEnvironment hostEnvironment)
    {
        builder.Add(new RemoteServiceDiscoveryConfigurationSource(new(hostEnvironment.BaseAddress)));
        return builder;
    }

    /// <summary>
    /// Adds the remote service discovery configuration provider to the configuration builder with the base address from the specified host builder.
    /// </summary>
    public static IConfigurationBuilder AddRemoteServiceDiscovery(this IConfigurationBuilder builder, WebAssemblyHostBuilder hostBuilder)
    {
        builder.Add(new RemoteServiceDiscoveryConfigurationSource(new(hostBuilder.HostEnvironment.BaseAddress)));
        return builder;
    }
}

internal static class ServiceDiscoveryHttpClientBuilderExtensions
{
    /// <summary>
    /// Configures the HttpClient to use the service discovery endpoint to replace the base address with the service endpoint based on the configuration.
    /// </summary>
    public static IHttpClientBuilder ConfigureRemoteServiceDiscovery(this IHttpClientBuilder httpClientBuilder)
    {
        httpClientBuilder.ConfigureHttpClient((services, HttpClient) =>
        {
            var configuredBaseAddress = HttpClient.BaseAddress;

            // If the base address is not configured or is not a DNS host name, do nothing
            if (configuredBaseAddress is null || configuredBaseAddress.HostNameType != UriHostNameType.Dns)
                return;

            var oldHostName = configuredBaseAddress.Host;  // The old host name represents the service name (e.g. "backend" or "gateway")
            var configuration = services.GetRequiredService<IConfiguration>();

            var serviceEndpoint = configuration[$"services:{oldHostName}"];

            // If no service endpoint is found, do nothing and use the configured base address
            if (serviceEndpoint is null)
                return;

            var serviceEndpointUri = new Uri(serviceEndpoint);

            // Replace the old host name with the new host name
            HttpClient.BaseAddress = new UriBuilder(configuredBaseAddress)
            {
                Scheme = serviceEndpointUri.Scheme,
                Host = serviceEndpointUri.Host,
                Port = serviceEndpointUri.Port
            }.Uri;
        });

        return httpClientBuilder;
    }
}