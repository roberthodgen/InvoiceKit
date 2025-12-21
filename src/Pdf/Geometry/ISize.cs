namespace InvoiceKit.Pdf.Geometry;

using SkiaSharp;

/// <summary>
/// Represents a block size.
/// </summary>
public interface ISize
{
    /// <summary>
    /// With of a block.
    /// </summary>
    float Width { get; }

    /// <summary>
    /// Height of a block.
    /// </summary>
    float Height { get; }

    /// <summary>
    /// Gets the block size as an <see cref="SKSize"/>.
    /// </summary>
    SKSize ToSize();

    /// <summary>
    /// Converts this size to a content size using a given style.
    /// </summary>
    /// <param name="style">The current block's style</param>
    ContentSize ToContentSize(BlockStyle style);

    /// <summary>
    /// Converts this size to a padding size using a given style.
    /// </summary>
    /// <param name="style">The current block's style</param>
    PaddingSize ToPaddingSize(BlockStyle style);

    /// <summary>
    /// Converts this size to a border size using a given style.
    /// </summary>
    /// <param name="style">The current block's style</param>
    BorderSize ToBorderSize(BlockStyle style);

    /// <summary>
    /// Converts this size to an outer (margin) size using a given style.
    /// </summary>
    /// <param name="style">The current block's style</param>
    OuterSize ToOuterSize(BlockStyle style);
}
