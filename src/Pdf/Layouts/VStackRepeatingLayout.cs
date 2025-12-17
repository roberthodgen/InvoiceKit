namespace InvoiceKit.Pdf.Layouts;

internal class VStackRepeatingLayout(List<ILayout> children) : ILayout
{
    public LayoutResult Layout(ILayoutContext context)
    {
        return LayoutResult.Deferred(children.Select(child => ChildLayout.CreateVertical(child, context)).ToList());
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        return parentContext.GetVerticalChildContext(parentContext.Available);
    }
}
