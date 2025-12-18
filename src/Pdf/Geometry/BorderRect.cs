namespace InvoiceKit.Pdf.Geometry;

using SkiaSharp;

public readonly record struct BorderRect
{
    private SKRect Value { get; }

    public float Left => Value.Left;

    public float Top => Value.Top;

    public float Right => Value.Right;

    public float Bottom => Value.Bottom;

    internal BorderRect(SKRect rect)
    {
        Value = rect;
    }

    public BorderRect(float left, float top, float right, float bottom)
        : this(new SKRect(left, top, right, bottom))
    {
    }

    public BorderSize ToSize() => new (Value.Size);

    public SKRect ToRect() => Value;

    public override string ToString()
    {
        return Value.ToString();
    }
}
