namespace InvoiceKit.Pdf.Layouts;

internal class PageBreakLayout : ILayout
{
    private bool IsDrawn;

    public LayoutResult Layout(ILayoutContext context)
    {
        if (IsDrawn)
        {
            return LayoutResult.FullyDrawn([]);
        }

        IsDrawn = true;
        return LayoutResult.NeedsNewPage([]);
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        return parentContext.GetVerticalChildContext(parentContext.Available);
    }
}
