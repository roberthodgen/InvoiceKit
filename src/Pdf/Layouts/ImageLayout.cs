namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;
using Styles;
using Svg.Skia;

internal class ImageLayout : ILayout
{
    public BlockStyle Style { get; }

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

    public SKSize Measure(SKSize available)
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

    public LayoutResult Layout(LayoutContext context)
    {
        var drawables = new List<IDrawable>();
        var imageContext = context.GetChildContext(Style.GetContentRect(context.Available));

        if (imageContext.TryAllocate(this, out var rect))
        {
            if (Svg is not null)
            {
                drawables.Add(new SvgImageDrawable(Svg, rect));
            }
            else if (Bitmap is not null)
            {
                drawables.Add(new BitmapImageDrawable(Bitmap, rect));
            }

            // Add background and border drawables
            drawables.Add(new BackgroundDrawable(Style.GetBackgroundRect(imageContext.Allocated), Style.BackgroundToPaint()));
            drawables.Add(new BorderDrawable(Style.GetBorderRect(imageContext.Allocated), Style.Border));

            // Add margin and padding debug drawables
            drawables.Add(new DebugDrawable(Style.GetMarginDebugRect(imageContext.Allocated), DebugDrawable.MarginDebug));
            drawables.Add(new DebugDrawable(Style.GetPaddingDebugRect(imageContext.Allocated), DebugDrawable.PaddingDebug));

            context.CommitChildContext(imageContext);
            return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
        }

        return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
    }
}
