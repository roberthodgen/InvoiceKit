namespace InvoiceKit.Pdf.Views;

using Layouts;

/// <summary>
/// Represents a page break. All content after this will be rendered on a new page.
/// </summary>
public sealed class PageBreakViewBuilder : IViewBuilder
{
    public ILayout ToLayout()
    {
        return new PageBreakLayout();
    }
}
