using Microsoft.Fast.Components.FluentUI;
using CodeBreaker.UI.Shared.Models.Icon;
using CodeBreaker.UI.Utils;

namespace CodeBreaker.UI;

public partial class CodeBreakerIconButton
{
    public override string GetIcon() => IconHelper.GetIcon(Icon);
}
