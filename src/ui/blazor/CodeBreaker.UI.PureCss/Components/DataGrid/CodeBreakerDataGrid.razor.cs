using CodeBreaker.UI.Models.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace CodeBreaker.UI;

public partial class CodeBreakerDataGrid<T> : ComponentBase
{
    private IQueryable<T>? _source;
    private PaginationState pagination = new() { ItemsPerPage = 7 };

    [Parameter]
    public List<string> Headers { get; set; } = new List<string>();

    [Parameter]
    public List<T> Items { get; set; } = new();

    [Parameter]
    public List<CodeBreakerColumnDefinition<T>> Columns { get; set; } = new List<CodeBreakerColumnDefinition<T>>();

    [Parameter]
    public EventCallback<T> RowItemClicked { get; set; } = new();

    protected override void OnInitialized()
    {
        _source = Items.AsQueryable();
        pagination.TotalItemCountChanged += (sender, eventArgs) => StateHasChanged();
        base.OnInitialized();
    }
    private async Task GoToPageAsync(int pageIndex)
    {
        await pagination.SetCurrentPageIndexAsync(pageIndex);
    }

    private string? PageButtonClass(int pageIndex)
        => pagination.CurrentPageIndex == pageIndex ? "current" : null;

    private string? AriaCurrentValue(int pageIndex)
        => pagination.CurrentPageIndex == pageIndex ? "page" : null;

    private async Task Hire(T context) 
    {
        await RowItemClicked.InvokeAsync(context);
    }
}
