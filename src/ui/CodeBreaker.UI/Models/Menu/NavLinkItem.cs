using CodeBreaker.UI.Models.Icon;
using Microsoft.AspNetCore.Components.Routing;

namespace CodeBreaker.UI.Models.Menu;

public record NavLinkItem(string Label, string Href, NavLinkMatch Match, CodeBreakerIcon Icon);
