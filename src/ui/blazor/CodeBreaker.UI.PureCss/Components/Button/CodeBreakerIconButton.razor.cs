using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using CodeBreaker.UI.Utils;

namespace CodeBreaker.UI;

public partial class CodeBreakerIconButton
{
    public override string GetIcon() => IconHelper.GetIcon(Icon);
}
