namespace InvoiceKit.Pdf.Geometry;

using SkiaSharp;

public readonly record struct OuterRect
{
    private SKRect Value { get; }

    public float Left => Value.Left;

    public float Top => Value.Top;

    public float Right => Value.Right;

    public float Bottom => Value.Bottom;

    public float Width => Value.Width;

    public float Height => Value.Height;

    public OuterRect(SKRect rect)
    {
        Value = rect;
    }

    public OuterRect(SKSize size)
        : this(SKRect.Create(size))
    {
    }

    public OuterRect(float left, float top, float right, float bottom)
        : this(new SKRect(left, top, right, bottom))
    {
    }

    public OuterSize ToSize() => new (Value.Size);

    public SKRect ToRect() => Value;

    public static OuterRect Intersect(OuterRect a, OuterRect b)
    {
        return new (SKRect.Intersect(a.Value, b.Value));
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
