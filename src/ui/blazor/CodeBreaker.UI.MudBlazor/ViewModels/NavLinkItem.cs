using Microsoft.AspNetCore.Components.Routing;

namespace CodeBreaker.UI.MudBlazor.ViewModels;

public record NavLinkItem(string Label, string Href, NavLinkMatch Match, string Icon);
