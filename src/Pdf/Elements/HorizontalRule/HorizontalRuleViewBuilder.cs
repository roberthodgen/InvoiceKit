namespace InvoiceKit.Pdf.Elements.HorizontalRule;

using Layout;

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
