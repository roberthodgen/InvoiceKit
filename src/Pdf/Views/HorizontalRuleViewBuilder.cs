namespace InvoiceKit.Pdf.Views;

using Layouts;

/// <summary>
/// Adds a horizontal line to the document.
/// </summary>
public sealed class HorizontalRuleViewBuilder : IViewBuilder
{
    public ILayout ToLayout()
    {
        return new HorizontalRuleLayout();
    }
}
