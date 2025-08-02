namespace InvoiceKit.Pdf.Elements.Images;

using Layouts;
using SkiaSharp;
using Svg.Skia;

internal class SvgImage : IDrawable
{
    private readonly SKSvg _svg;

    public SvgImage(string path)
    {
        _svg = new SKSvg();
        _svg.Load(path);
    }

    public SKSize Measure(SKSize available)
    {
        if (_svg.Drawable is null)
        {
            throw new InvalidOperationException("SVG does not have drawable size.");
        }

        return new SKSize(_svg.Drawable.Bounds.Size.Width, _svg.Drawable.Bounds.Size.Height);
    }

    public void Draw(PageLayout page, SKRect rect, Func<PageLayout> getNextPage)
    {
        page.Canvas.DrawPicture(_svg.Picture, rect.Location);
    }

    public void Dispose()
    {
        _svg.Dispose();
    }
}
