namespace InvoiceKit.Pdf.Drawables;

using SkiaSharp;
using Styles;
using Svg.Skia;

internal class SvgImageDrawable(SKSvg svg, SKRect rect, BlockStyle style) : IDrawable
{
    public void Draw(IDrawableContext context)
    {
        context.Canvas.DrawRect(rect, style.BackgroundToPaint());
        context.Canvas.DrawPicture(svg.Picture, rect.Location);
    }

    public void Dispose()
    {
        svg.Dispose();
    }
}
