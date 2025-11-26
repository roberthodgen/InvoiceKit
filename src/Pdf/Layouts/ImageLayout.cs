namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;
using Styles;
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

        if (context.TryAllocate(Style.GetStyleSize()) == false)
        {
            return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
        }

        if (imageContext.TryAllocate(this, out var rect))
        {
            if (Svg is not null)
            {
                // Add border drawable.
                // Background needs to come before text.
                drawables.Insert(0, new BackgroundDrawable(Style.GetBackgroundRect(rect), Style.BackgroundToPaint()));
                drawables.Add(new BorderDrawable(Style.GetBorderRect(rect), Style.Border));
                drawables.Add(new DebugDrawable(rect, DebugDrawable.ContentColor));
                drawables.Add(new SvgImageDrawable(Svg, rect));
            }
            else if (Bitmap is not null)
            {
                drawables.Add(new DebugDrawable(rect, DebugDrawable.ContentColor));
                drawables.Add(new BitmapImageDrawable(Bitmap, rect));
            }

            // Add margin and padding debug drawables.
            drawables.Add(new DebugDrawable(Style.GetMarginDebugRect(imageContext.Allocated), DebugDrawable.MarginColor));
            drawables.Add(new DebugDrawable(Style.GetBackgroundRect(imageContext.Allocated), DebugDrawable.PaddingColor));

            context.CommitChildContext(imageContext);
            return new LayoutResult(drawables, LayoutStatus.IsFullyDrawn);
        }

        return new LayoutResult(drawables, LayoutStatus.NeedsNewPage);
    }
}
