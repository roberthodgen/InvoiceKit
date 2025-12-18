namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using Geometry;
using SkiaSharp;
using Svg.Skia;

internal class ImageLayout : ILayout
{
    private BlockStyle Style { get; }

    private SKSvg? Svg { get; }

    private SKBitmap? Bitmap { get; }

    internal ImageLayout(string path, ImageType type, BlockStyle style)
    {
        Style = style;
        if (type == ImageType.Svg)
        {
            Svg = new SKSvg();
            Svg.Load(path);
        }
        else if (type == ImageType.Bmp)
        {
            using var data = SKData.Create(path);
            using var codec = SKCodec.Create(data);
            Bitmap = SKBitmap.Decode(codec);
        }
        else
        {
            throw new ArgumentException("Unsupported image type", nameof(type));
        }
    }

    private ContentSize Measure()
    {
        if (Svg?.Drawable is not null)
        {
            return new ContentSize(Svg.Drawable.Bounds.Width, Svg.Drawable.Bounds.Height);
        }

        if (Bitmap is not null)
        {
            return new ContentSize(Bitmap.Width, Bitmap.Height);
        }

        throw new Exception("Image not loaded.");
    }

    public LayoutResult Layout(ILayoutContext context)
    {
        var contentSize = Measure();
        var paddingSize = Style.Padding.ToSize(contentSize);
        var borderSize = Style.Border.ToSize(paddingSize);
        var marginSize = Style.Margin.ToSize(borderSize);
        if (context.TryAllocate(marginSize, out var outerRect))
        {
            if (Svg is not null)
            {
                return LayoutResult.FullyDrawn([new SvgImageDrawable(Svg, Style.GetContentRect(outerRect).ToRect()),]);
            }

            if (Bitmap is not null)
            {
                return LayoutResult.FullyDrawn(
                [
                    new BitmapImageDrawable(Bitmap, Style.GetContentRect(outerRect).ToRect()),
                ]);
            }

            throw new ApplicationException("Image not loaded.");
        }

        return LayoutResult.NeedsNewPage([]);
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        return parentContext.GetVerticalChildContext();
    }

    public ILayoutContext GetContext(ILayoutContext parentContext, OuterRect intersectingRect)
    {
        return parentContext.GetVerticalChildContext(intersectingRect);
    }
}
