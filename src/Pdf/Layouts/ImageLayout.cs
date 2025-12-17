namespace InvoiceKit.Pdf.Layouts;

using Drawables;
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

    private SKSize Measure()
    {
        if (Svg?.Drawable is not null)
        {
            return new SKSize(Svg.Drawable.Bounds.Width, Svg.Drawable.Bounds.Height);
        }

        if (Bitmap is not null)
        {
            return new SKSize(Bitmap.Width, Bitmap.Height);
        }

        throw new Exception("Image not loaded.");
    }

    public LayoutResult Layout(ILayoutContext context)
    {
        var drawables = new List<IDrawable>();
        var childContext = GetContext(context);

        if (context.TryAllocate(Style.GetStyleSize()) == false)
        {
            return LayoutResult.NeedsNewPage([]);
        }

        if (childContext.TryAllocate(Measure(), out var rect))
        {
            drawables.AddRange(Style.GetStyleDrawables(rect));

            if (Svg is not null)
            {
                drawables.Add(new SvgImageDrawable(Svg, rect));
            }
            else if (Bitmap is not null)
            {
                drawables.Add(new BitmapImageDrawable(Bitmap, rect));
            }

            childContext.CommitChildContext();
            return LayoutResult.FullyDrawn(drawables);
        }

        childContext.CommitChildContext();
        return LayoutResult.NeedsNewPage(drawables);
    }

    public ILayoutContext GetContext(ILayoutContext parentContext)
    {
        return parentContext.GetVerticalChildContext(Style.GetContentRect(parentContext.Available));
    }
}
