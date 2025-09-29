namespace InvoiceKit.Pdf.Elements.Images;

using SkiaSharp;
using Svg.Skia;

internal class SvgImageDrawable(SKSvg svg, SKRect rect) : IDrawable
{
    public void Draw(IDrawableContext context)
    {
        context.Canvas.DrawPicture(svg.Picture, rect.Location);
    }

    public void Dispose()
    {
        svg.Dispose();
    }
}
