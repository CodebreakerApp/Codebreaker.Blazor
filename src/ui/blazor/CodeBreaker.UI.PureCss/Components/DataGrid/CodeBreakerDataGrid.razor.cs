using System.Linq.Expressions;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace CodeBreaker.UI;

public partial class CodeBreakerDataGrid<T>
{
    private IQueryable<T> source;
    private PaginationState pagination = new() { ItemsPerPage = 7 };

    protected override void OnInitialized()
    {
        source = Items.AsQueryable();
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
