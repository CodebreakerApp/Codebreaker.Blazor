namespace CodeBreaker.Blazor.Services;

public class DialogService
{
    private readonly MudBlazor.IDialogService _mudBlazorDialogService;

    public DialogService(MudBlazor.IDialogService mudBlazorDialogService)
    {
        _mudBlazorDialogService = mudBlazorDialogService;
    }
    public Task ShowMessageAsync(string message) =>
        _mudBlazorDialogService.ShowMessageBox("CodeBreaker", message);
}
