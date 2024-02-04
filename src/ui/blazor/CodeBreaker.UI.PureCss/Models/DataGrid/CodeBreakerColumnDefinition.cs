using System.Linq.Expressions;

namespace CodeBreaker.UI.Models.DataGrid;
public class CodeBreakerColumnDefinition<T>
{
    public string ColumnDataKey { get; set; }
    public string Title { get; set; }

    public Func<T, object>? FieldSelector { get; set; }

    public Expression<Func<T, object>>? FieldSelectorExpression { get; set; }
    public string? Value { get; set; }
    public bool ShowMobile { get; set; }

    public CodeBreakerColumnDefinition(
        string fieldName,
        Expression<Func<T, object>> fieldSelectorExpression,
        bool showMobile = true,
        string? value = null)
    {
        ColumnDataKey = fieldName;
        Title = fieldName;
        FieldSelectorExpression = fieldSelectorExpression;
        FieldSelector = fieldSelectorExpression.Compile();
        ShowMobile = showMobile;
        Value = value;
    }
}
