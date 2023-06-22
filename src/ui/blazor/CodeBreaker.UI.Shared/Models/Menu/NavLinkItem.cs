using CodeBreaker.UI.Shared.Models.Icon;
using Microsoft.AspNetCore.Components.Routing;

namespace CodeBreaker.UI.Shared.Models.Menu;

public record NavLinkItem(string Label, string Href, NavLinkMatch Match, CodeBreakerIcon Icon);
