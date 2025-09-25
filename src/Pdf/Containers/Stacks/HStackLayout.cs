namespace InvoiceKit.Pdf.Containers.Stacks;

using SkiaSharp;

public class HStackLayout : ILayout
{
    private Stack<ILayout> Children { get; }

    internal HStackLayout(List<ILayout> children)
    {
        Children = new Stack<ILayout>(children.AsEnumerable().Reverse());
    }

    /// <summary>
    /// Horizontal stack layout that will split into columns based on the number of children.
    /// </summary>
    public LayoutResult Layout(LayoutContext context)
    {
        if (Children.Count == 0)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var needsNewPage = false;
        List<LayoutResult> layoutResults = [];

        // Loops for the number of children once. Children that need a new page are added back to the stack.
        for (var i = 0; i < Children.Count; i++)
        {
            var layout = Children.Pop();
            var layoutResult = layout.Layout(context);
            if (layoutResult.Status == LayoutStatus.NeedsNewPage)
            {
                Children.Push(layout); // HStack should not reverse the stack to push
                needsNewPage = true;
            }
            layoutResults.Add(layoutResult);
        }

        var listDrawables = new List<IDrawable>();
        layoutResults.ForEach(result => result.Drawables.ForEach(drawable => listDrawables.Add(drawable)));

        return needsNewPage ? new LayoutResult(listDrawables, LayoutStatus.NeedsNewPage)
            : new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width / Children.Count, available.Height);
    }
}
