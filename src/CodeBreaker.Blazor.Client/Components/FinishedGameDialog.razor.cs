using Codebreaker.GameAPIs.Client.Models;
using CodeBreaker.Blazor.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace CodeBreaker.Blazor.Client.Components;

public partial class FinishedGameDialog : IDialogContentComponent<GameInfo?>
{
    [Parameter] public GameInfo? Content { get; set; }

    [CascadingParameter] public FluentDialog Dialog { get; set; } = default!;

    public GameInfo? Game => Content;

    private IEnumerable<SelectionAndKeyPegs>? _gameMoves;

    private string KeyPegsFormat => Game?.NumberCodes > 4 ? "three-two" : "two-two";

    protected override void OnInitialized()
    {
        _gameMoves = Game?.Moves.Select(move => new SelectionAndKeyPegs([.. Field.Parse(move.GuessPegs)], move.KeyPegs, move.MoveNumber));
    }

    private async Task CloseDialog() =>
        await Dialog.CloseAsync();
}