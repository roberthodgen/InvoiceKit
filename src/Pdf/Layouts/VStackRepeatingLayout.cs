namespace InvoiceKit.Pdf.Layouts;

using SkiaSharp;

internal class VStackRepeatingLayout(List<ILayout> children) : ILayout
{
    public LayoutResult Layout(LayoutContext context)
    {
        return LayoutResult.Deferred(children.Select(child => new ChildLayout(child, context)).ToList());
    }
}
