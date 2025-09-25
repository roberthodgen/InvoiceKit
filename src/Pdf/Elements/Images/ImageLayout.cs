namespace InvoiceKit.Pdf.Elements.Images;

using SkiaSharp;
using Svg.Skia;

public class ImageLayout : ILayout
{
    private SKSvg? Svg { get; }

    private SKBitmap? Bitmap { get; }

    internal ImageLayout(string path, string imageType)
    {
        switch (imageType)
        {
            case "svg":
                Svg = new SKSvg();
                Svg.Load(path);
                break;
            case "bmp":
            {
                using var data = SKData.Create(path);
                using var codec = SKCodec.Create(data);
                Bitmap = SKBitmap.Decode(codec);
                break;
            }
            default:
                throw new ArgumentException("Image type not supported.");
        }
    }

    public SKSize Measure(SKSize available)
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

    public LayoutResult Layout(LayoutContext context)
    {
        var listDrawables = new List<IDrawable>();
        var size = Measure(context.Available.Size);
        while (true)
        {
            var rect = new SKRect(
                context.Available.Left,
                context.Available.Top,
                context.Available.Left + size.Width,
                context.Available.Top + size.Height);

            if (Svg?.Drawable != null)
            {
                if (context.TryAllocateRect(rect))
                {
                    listDrawables.Add(new SvgImageDrawable(Svg, rect));
                    break;
                }
            }

            if (Bitmap != null)
            {
                if (context.TryAllocateRect(rect))
                {
                    listDrawables.Add(new BitmapImageDrawable(Bitmap, rect));
                    break;
                }
            }

            // Will only be hit if the page is full.
            return new LayoutResult(listDrawables, LayoutStatus.NeedsNewPage);
        }
        return new LayoutResult(listDrawables, LayoutStatus.IsFullyDrawn);
    }
}
