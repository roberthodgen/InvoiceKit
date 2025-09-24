namespace InvoiceKit.Pdf.Containers.Stacks;

using SkiaSharp;

public class HStackLayout : ILayout
{
    private List<ILayout> Children { get; }

    internal HStackLayout(List<ILayout> children)
    {
        Children = children;
    }

    /// <summary>
    /// Horizontal stack layout that will split into columns based on the number of children.
    /// </summary>
    public LayoutResult Layout(LayoutContext context)
    {
        if (Children.Count == 0)
        {
            return new LayoutResult([], LayoutState.IsEmpty);
        }

        // Todo: Need to fix HStack column sizes, currently working as a VStack
        List<LayoutResult> results = [];
        foreach (var child in Children)
        {
            results.Add(child.Layout(context));
        }
        var heights = results.Select(result => result.Drawables.Select(drawable => drawable.SizeAndLocation.Height));
        var maxHeight = heights.Max(height => height.Max());
        context.ForceAllocateSize(new SKSize(context.Available.Width, maxHeight));

        return results.Last();
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width / Children.Count, available.Height);
    }
}
