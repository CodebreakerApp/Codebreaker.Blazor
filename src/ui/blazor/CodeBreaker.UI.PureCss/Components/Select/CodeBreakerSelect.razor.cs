namespace CodeBreaker.UI;
public partial class CodeBreakerSelect<T>
{
    private string _selectedKey = string.Empty;
    private bool _isOpen;

    protected override void OnInitialized()
    {
        var selectedItem = Items.FirstOrDefault(i => i.Value.GetHashCode() == Value.GetHashCode()).Key;
        if (!String.IsNullOrWhiteSpace(selectedItem))
        {
            _selectedKey = selectedItem;
        }
        base.OnInitialized();
    }

    private async Task OnSelectionChanged(string key, T value)
    {
        Value = value;
        await ValueChanged.InvokeAsync(Value);
        _selectedKey = key;
        _isOpen = false;
    }
}
