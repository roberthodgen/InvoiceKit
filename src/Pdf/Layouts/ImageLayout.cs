namespace InvoiceKit.Pdf.Layouts;

using Drawables;
using SkiaSharp;
using Svg.Skia;

internal class ImageLayout : ILayout
{
    private bool _drawn;

    private SKSvg? Svg { get; }

    private SKBitmap? Bitmap { get; }

    internal ImageLayout(string path, ImageType type)
    {
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

    public SKSize Measure(SKRect available)
    {
        if (Svg?.Drawable != null)
        {
            return new SKSize(Svg.Drawable.Bounds.Width, Svg.Drawable.Bounds.Height);
        }

        if (Bitmap != null)
        {
            return new SKSize(Bitmap.Width, Bitmap.Height);
        }

        throw new Exception("Image not loaded.");
    }

    public LayoutResult Layout(LayoutContext context, LayoutType layoutType)
    {
        if (_drawn)
        {
            return new LayoutResult([], LayoutStatus.IsFullyDrawn);
        }

        var listDrawables = new List<IDrawable>();
        var size = Measure(context.Available);

        var rect = new SKRect(
            context.Available.Left,
            context.Available.Top,
            context.Available.Left + size.Width,
            context.Available.Top + size.Height);

        if (context.TryAllocateRect(rect))
        {
            if (Svg is not null)
            {
                listDrawables.Add(new SvgImageDrawable(Svg, rect));
            }
            else if (Bitmap is not null)
            {
                listDrawables.Add(new BitmapImageDrawable(Bitmap, rect));
            }

            if (layoutType == LayoutType.DrawOnceElement)
            {
                _drawn = true;
            }
            return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
        }

        // Will only be hit if the page is full.
        return new LayoutResult(listDrawables, LayoutStatus.NeedsNewPage);
    }
}
