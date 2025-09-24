namespace InvoiceKit.Pdf.Containers.PageBreak;

public sealed class PageBreakViewBuilder : IViewBuilder
{
    public IReadOnlyCollection<IViewBuilder> Children => [];

    public ILayout ToLayout()
    {
        return new PageBreakLayout();
    }
}
