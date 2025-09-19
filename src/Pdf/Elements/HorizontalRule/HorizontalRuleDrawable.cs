namespace InvoiceKit.Pdf.Elements.HorizontalRule;

using Containers;
using SkiaSharp;

public sealed class HorizontalRuleDrawable : IDrawable
{
    public SKRect SizeAndLocation { get; }

    public bool Debug { get; }

    public HorizontalRuleDrawable(SKRect rect, bool debug = false)
    {
        SizeAndLocation = rect;
        Debug = debug;
    }

    public void Draw(SKCanvas canvas, Page page)
    {
        canvas.DrawLine(
            SizeAndLocation.Location,
            SKPoint.Add(SizeAndLocation.Location, new SKSize(SizeAndLocation.Width, 0)),
            new SKPaint
            {
                Color = SKColors.Black,
                StrokeWidth = 1f,
            });
    }

    public void Dispose()
    {
    }
}
