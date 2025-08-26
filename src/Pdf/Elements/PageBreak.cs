namespace InvoiceKit.Pdf.Elements;

using SkiaSharp;

public sealed class PageBreak : IDrawable
{
    public SKSize Measure(SKSize available)
    {
        return new SKSize(available.Width, 0);
    }

    public void Draw(MultiPageContext context, SKRect rect)
    {
        // nothing to draw
        context.NextPage();
    }

    public void Dispose()
    {
    }
}
