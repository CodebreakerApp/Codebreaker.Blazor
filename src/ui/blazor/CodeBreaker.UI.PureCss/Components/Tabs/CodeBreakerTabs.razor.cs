namespace CodeBreaker.UI
{
    public partial class CodeBreakerTabs
    {
        private async Task ChangeActiveTab(string tab)
        {
            ActiveTab = tab;
            await ActiveTabChanged.InvokeAsync(ActiveTab);
        }
    }
}
