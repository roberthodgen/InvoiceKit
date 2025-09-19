namespace InvoiceKit.Pdf.Containers.Stacks;

using SkiaSharp;

public class HStackLayout : ILayout
{
    private List<ILayout> Children { get; }

    internal HStackLayout(List<ILayout> children)
    {
        Children = children;
    }

    public void LayoutPages(MultiPageContext context, bool debug)
    {
        throw new NotImplementedException();
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, Children.Sum(child => child.Measure(available).Height));
    }
}
