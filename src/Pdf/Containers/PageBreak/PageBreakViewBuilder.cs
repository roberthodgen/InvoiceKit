namespace InvoiceKit.Pdf.Containers.PageBreak;

public sealed class PageBreakViewBuilder : IViewBuilder
{
    public ILayout ToLayout()
    {
        return new PageBreakLayout();
    }
}
