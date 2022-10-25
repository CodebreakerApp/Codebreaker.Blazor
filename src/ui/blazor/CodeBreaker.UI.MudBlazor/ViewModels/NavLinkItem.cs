using Microsoft.AspNetCore.Components.Routing;

namespace CodeBreaker.UI.ViewModels;

public record NavLinkItem(string Label, string Href, NavLinkMatch Match, string Icon);
