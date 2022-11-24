using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeBreaker.UI.Shared.Models.Icon;
using Microsoft.AspNetCore.Components;

namespace CodeBreaker.UI.Shared.Components.Buttons;
public abstract class CodeBreakerIconButtonBase : CodeBreakerButtonBase
{
    [Parameter]
    public CodeBreakerIcon Icon { get; set; }
}
