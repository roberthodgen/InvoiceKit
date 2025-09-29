namespace InvoiceKit.Pdf.Elements.Images;

using SkiaSharp;
using Svg.Skia;

internal class SvgImageDrawable : IDrawable
{
    private readonly SKSvg _svg;

    public SKRect SizeAndLocation { get; }

    public bool Debug { get; }

    public SvgImageDrawable(SKSvg svg, SKRect rect, bool debug = false)
    {
        _svg = svg;
        SizeAndLocation = rect;
        Debug = debug;
    }

    public void Draw(IDrawableContext context)
    {
        context.Canvas.DrawPicture(_svg.Picture, SizeAndLocation.Location);
    }

    public void Dispose()
    {
        _svg.Dispose();
    }
}
