namespace InvoiceKit.Pdf.Views;

using Layouts;
using Styles;

/// <summary>
/// Adds a horizontal line to the document.
/// </summary>
public sealed class HorizontalRuleViewBuilder(BlockStyle style) : IViewBuilder
{
    public ILayout ToLayout()
    {
        return new HorizontalRuleLayout(style);
    }
}
