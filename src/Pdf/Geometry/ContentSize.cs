namespace InvoiceKit.Pdf.Geometry;

using SkiaSharp;

/// <summary>
/// Represents the size of a content area.
/// </summary>
/// <remarks>
/// The content area is inset within the padding, border, and margin.
/// </remarks>
public readonly record struct ContentSize
{
    public static ContentSize Empty => new (SKSize.Empty);

    private SKSize Value { get; }

    public float Width => Value.Width;

    public float Height => Value.Height;

    public ContentSize(float width, float height)
        : this(new SKSize(width, height))
    {
    }

    public ContentSize(SKSize value)
    {
        Value = value;
    }

    public SKSize ToSize() => Value;

    public override string ToString()
    {
        return Value.ToString();
    }
}
