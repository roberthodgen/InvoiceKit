namespace InvoiceKit.Pdf.Elements.Images;

using Containers;
using SkiaSharp;
using Svg.Skia;

internal class SvgImageDrawable : IDrawable
{
    private readonly SKSvg _svg;

    public SKRect SizeAndLocation { get; }

    public bool Debug { get; }

    public SvgImageDrawable(string path, SKRect rect, bool debug = false)
    {
        _svg = new SKSvg();
        _svg.Load(path);
        SizeAndLocation = rect;
        Debug = debug;
    }

    public SKSize Measure(SKSize available)
    {
        if (_svg.Drawable is null)
        {
            throw new InvalidOperationException("SVG does not have drawable size.");
        }

        return new SKSize(_svg.Drawable.Bounds.Size.Width, _svg.Drawable.Bounds.Size.Height);
    }

    public void Draw(SKCanvas canvas, PageLayout page)
    {
        canvas.DrawPicture(_svg.Picture, page.Available.Location);
    }

    public void Dispose()
    {
        _svg.Dispose();
    }
}
