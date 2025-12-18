namespace InvoiceKit.Pdf.Geometry;

using SkiaSharp;

public readonly record struct PaddingRect
{
    private SKRect Value { get; }

    public float Left => Value.Left;

    public float Top => Value.Top;

    public float Right => Value.Right;

    public float Bottom => Value.Bottom;

    public PaddingRect(SKRect rect)
    {
        Value = rect;
    }

    public PaddingRect(float left, float top, float right, float bottom)
        : this(new SKRect(left, top, right, bottom))
    {
    }

    public SKRect ToRect() => Value;

    public PaddingSize ToSize() => new (Value.Size);

    public override string ToString()
    {
        return Value.ToString();
    }
}
