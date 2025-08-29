namespace InvoiceKit.Pdf.Containers.Stacks;

using SkiaSharp;

public class VStackLayout : ILayout
{
    private List<ILayout> Children { get; }

    internal VStackLayout(List<ILayout> children)
    {
        Children = children;
    }

    public void LayoutPages(MultiPageContext context)
    {
        Children.ForEach(child => child.LayoutPages(context));
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, Children.Sum(child => child.Measure(available).Height));
    }
}
