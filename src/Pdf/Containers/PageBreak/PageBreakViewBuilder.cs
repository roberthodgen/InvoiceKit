namespace InvoiceKit.Pdf.Containers.PageBreak;

using Layout;

public sealed class PageBreakViewBuilder : IViewBuilder
{
    public IReadOnlyCollection<IViewBuilder> Children => [];

    public ILayout ToLayout()
    {
        return new PageBreakLayout();
    }
}
