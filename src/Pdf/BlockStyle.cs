namespace InvoiceKit.Pdf;

using Drawables;
using Geometry;
using SkiaSharp;

public readonly record struct BlockStyle()
{
    /// <summary>
    /// Sets the foreground color for text and horizontal rules.
    /// </summary>
    public SKColor ForegroundColor { get; init; } = SKColors.Black;

    /// <summary>
    /// Sets the background fill color for blocks.
    /// </summary>
    public SKColor BackgroundColor { get; init; } = SKColor.Empty;

    /// <summary>
    /// Should be specified as <c>Font Name/Style</c>, e.g.: <c>Open Sans/SemiBold</c>.
    /// </summary>
    public string? FontPath { get; init; } = "Open Sans/Regular";

    /// <summary>
    /// Text line height.
    /// </summary>
    public float LineHeight { get; init; } = 1.1f;

    /// <summary>
    /// Text font size.
    /// </summary>
    public float FontSize { get; init; } = 12f;

    /// <summary>
    /// Stores border styles for all sides.
    /// </summary>
    public BoxBorder Border { get; init; } = new ();

    /// <summary>
    /// Stores margin styles for all sides.
    /// </summary>
    public Margin Margin { get; init; } = new ();

    /// <summary>
    /// Stores padding styles for all sides.
    /// </summary>
    public Padding Padding { get; init; } = new ();

    /// <summary>
    /// Converts the foreground color to a <see cref="SKPaint"/> for drawing on the canvas.
    /// </summary>
    public SKPaint ForegroundToPaint()
    {
        return new SKPaint
        {
            Color = ForegroundColor,
            IsAntialias = true,
        };
    }

    /// <summary>
    /// Converts the background color to a <see cref="SKPaint"/> for drawing on the canvas.
    /// </summary>
    public SKPaint BackgroundToPaint()
    {
        return new SKPaint
        {
            Color = BackgroundColor,
            IsAntialias = true,
        };
    }

    /// <summary>
    /// Converts font size and font into a <see cref="SKFont"/> for drawing on the canvas.
    /// </summary>
    public SKFont ToFont()
    {
        var font = new SKFont
        {
            Size = FontSize,
        };

        var truePath = $"Fonts/{FontPath}.ttf";
        if (!string.IsNullOrWhiteSpace(FontPath) && File.Exists(truePath))
        {
            font.Typeface = SKTypeface.FromFile(truePath);
        }

        return font;
    }

    /// <summary>
    /// Creates a copy of the current style for a child element.
    /// </summary>
    /// <returns>A copy of the current styling without margin, padding, and border.</returns>
    public BlockStyle CopyForChild()
    {
        return this with
        {
            Border = new BoxBorder(),
            Margin = new Margin(),
            Padding = new Padding(),
        };
    }

    /// <summary>
    /// Creates a list of drawables for the border, background, and debugs.
    /// </summary>
    /// <param name="content">Content rect received from a TryAllocate method on <see cref="ILayoutContext"/>.</param>
    /// <returns></returns>
    public List<IDrawable> GetStyleDrawables(OuterRect content)
    {
        var drawables = new List<IDrawable>
        {
            // TODO new BackgroundDrawable(GetBackgroundRect(content), BackgroundToPaint()),
            new BorderDrawable(content, this),
            // TODO new DebugDrawable(content, DebugDrawable.ContentColor),
            // TODO new DebugDrawable(GetMarginDebugRect(content), DebugDrawable.MarginColor),
            // TODO new DebugDrawable(GetBackgroundRect(content), DebugDrawable.PaddingColor),
        };
        return drawables;
    }

    /// <summary>
    /// Creates a drawable area after removing styling sizes.
    /// </summary>
    /// <param name="available">Available drawing size.</param>
    public ContentRect GetContentRect(OuterRect available)
    {
        return Padding.GetContentRect(Border.GetContentRect(Margin.ToBorderRect(available)));
    }

    /// <summary>
    /// Creates a drawable area for the background by expanding from the contentRect.
    /// </summary>
    /// <param name="contentRect">The SKRect of the element that was allocated.</param>
    public PaddingRect GetBackgroundRect(ContentRect contentRect)
    {
        return Padding.GetDrawableRect(contentRect);
    }

    /// <summary>
    /// Creates a drawable rect for the border by expanding from the contentRect.
    /// </summary>
    /// <param name="contentRect">The SKRect of the element that was allocated.</param>
    public BorderRect GetBorderRect(ContentRect contentRect)
    {
        return Border.GetDrawableRect(Padding.GetDrawableRect(contentRect));
    }
}
