using System.Linq.Expressions;

namespace CodeBreaker.UI;

public partial class CodeBreakerDataGrid<T>
{
    private IQueryable<T> source;
    protected override void OnInitialized()
    {
        source = Items.AsQueryable();
        base.OnInitialized();
    }

    private async Task Hire(T context) 
    {
        await RowItemClicked.InvokeAsync(context);
    }
}
