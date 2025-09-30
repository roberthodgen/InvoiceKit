namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;

internal class VStackLayout(List<ILayout> children, ILayout? header, ILayout? footer) : ILayout
{
    private readonly ILayout? _header = header;

    private readonly ILayout? _footer = footer;

    private readonly Queue<ILayout> _children = new (children);

    private bool _drawn;

    public LayoutResult Layout(LayoutContext context)
    {
        if (_children.Count == 0 || _drawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var drawables = new List<IDrawable>();

        while (_children.Count > 0)
        {
            var layout = _children.Peek();

            if (_header is not null)
            {
                drawables.AddRange(_header.Layout(context).Drawables);
            }
            var available = context.Available;
            // Todo: Measure isn't properly set up for most layouts
            var headerSize = _header?.Measure(context.Available.Size) ?? SKSize.Empty;
            var contentAvailable = SKRect.Create(available.Location, available.Size - headerSize);
            contentAvailable.Offset(0, headerSize.Height);

            var childContext = context.GetChildContext(contentAvailable);
            var layoutResult = layout.Layout(childContext);
            drawables.Add(new DebugDrawable(childContext.Allocated));
            drawables.AddRange(layoutResult.Drawables);
            context.CommitChildContext(childContext);

            if (layoutResult.Status == LayoutStatus.IsFullyDrawn)
            {
                _children.Dequeue();
            }
            else
            {
                return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
            }
        }

        _drawn = true;
        return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
    }

    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, _children.Sum(child => child.Measure(available).Height));
    }
}
