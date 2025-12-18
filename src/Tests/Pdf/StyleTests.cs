namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using InvoiceKit.Pdf.Geometry;
using SkiaSharp;

public sealed class StyleTests
{
    [Fact]
    public void CopyForChild_ResetsValues()
    {
        // Arrange
        var style = new BlockStyle
        {
            ForegroundColor = SKColors.Yellow,
            BackgroundColor = SKColors.Blue,
            Margin = new Margin(10f),
            Padding = new Padding(10f),
            Border = BoxBorder.Create(new BorderStyle { Width = 2f }),
        };

        // Sanity checks
        style.Margin.Left.ShouldBe(10f);
        style.Padding.Left.ShouldBe(10f);
        style.Border.Left.Width.ShouldBe(2f);

        // Act
        var childStyle = style.CopyForChild();

        // Assert
        childStyle.ForegroundColor.ShouldBe(style.ForegroundColor);
        childStyle.BackgroundColor.ShouldBe(style.BackgroundColor);
        childStyle.Margin.ShouldBe(new Margin());
        childStyle.Padding.ShouldBe(new Padding());
        childStyle.Border.ShouldBe(new BoxBorder());
    }

    [Fact]
    public void GetBorderRect_ReturnsRect()
    {
        // Arrange
        var style = new BlockStyle
        {
            Margin = new Margin(10f),
            Padding = new Padding(10f),
            Border = BoxBorder.Create(new BorderStyle { Width = 1f })
        };

        // Sanity checks
        style.Margin.Left.ShouldBe(10f);
        style.Padding.Left.ShouldBe(10f);
        style.Border.Left.Width.ShouldBe(1f);

        // Act
        var outer = new OuterRect(10, 10, 100, 100);
        var contentRect = style.GetContentRect(outer);
        var borderRect = style.GetBorderRect(contentRect);

        // Assert
        borderRect.Left.ShouldBe(20f);
        borderRect.Top.ShouldBe(20f);
        borderRect.Right.ShouldBe(90f);
        borderRect.Bottom.ShouldBe(90f);
    }

    [Fact]
    public void GetBackgroundRect_ReturnsRect()
    {
        // Arrange
        var style = new BlockStyle
        {
            Margin = new Margin(10f),
            Padding = new Padding(10f),
            Border = BoxBorder.Create(new BorderStyle { Width = 1f })
        };

        // Sanity checks
        style.Margin.Left.ShouldBe(10f);
        style.Padding.Left.ShouldBe(10f);
        style.Border.Left.Width.ShouldBe(1f);

        // Act
        var outerRect = new OuterRect(10, 10, 100, 100);
        var contentRect = style.GetContentRect(outerRect);
        var backgroundRect = style.GetBackgroundRect(contentRect);

        // Assert
        backgroundRect.Left.ShouldBe(21f);
        backgroundRect.Top.ShouldBe(21f);
        backgroundRect.Right.ShouldBe(89f);
        backgroundRect.Bottom.ShouldBe(89f);
    }

    [Fact]
    public void GetContentRect_ReturnsRect()
    {
        // Arrange
        var style = new BlockStyle
        {
            Margin = new Margin(10f),
            Padding = new Padding(10f),
            Border = BoxBorder.Create(new BorderStyle { Width = 1f })
        };

        // Sanity checks
        style.Margin.Left.ShouldBe(10f);
        style.Padding.Left.ShouldBe(10f);
        style.Border.Left.Width.ShouldBe(1f);

        // Act
        var startingRect = new OuterRect(10, 10, 100, 100);
        var contentRect = style.GetContentRect(startingRect);

        // Assert
        contentRect.Left.ShouldBe(31f);
        contentRect.Top.ShouldBe(31f);
        contentRect.Right.ShouldBe(79f);
        contentRect.Bottom.ShouldBe(79f);
    }

    [Fact]
    public void MarginTest_GetContentRect()
    {
        // Arrange
        var rect = new OuterRect(10, 10, 100, 100);
        var style = new BlockStyle { Margin = new Margin(10f, 10f, 10f, 10f), };

        // Act
        var styledRect = style.Margin.GetBorderRect(rect);

        // Assert
        styledRect.Top.ShouldBe(20);
        styledRect.Bottom.ShouldBe(90);
        styledRect.Left.ShouldBe(20);
        styledRect.Right.ShouldBe(90);
    }

    [Fact]
    public void PaddingTest_GetDrawableRect()
    {
        // Arrange
        var rect = new ContentRect(10, 10, 100, 100);
        var style = new BlockStyle { Padding = new Padding(10f, 10f, 10f, 10f) };

        // Act
        var styledRect = style.Padding.GetPaddingRect(rect);

        // Assert
        styledRect.Top.ShouldBe(0);
        styledRect.Bottom.ShouldBe(110);
        styledRect.Left.ShouldBe(0);
        styledRect.Right.ShouldBe(110);
    }

    [Fact]
    public void PaddingTest_GetContentRect()
    {
        // Arrange
        var rect = new PaddingRect(10, 10, 100, 100);
        var style = new BlockStyle { Padding = new Padding(10f, 10f, 10f, 10f) };

        // Act
        var styledRect = style.Padding.GetContentRect(rect);

        // Assert
        styledRect.Top.ShouldBe(20);
        styledRect.Bottom.ShouldBe(90);
        styledRect.Left.ShouldBe(20);
        styledRect.Right.ShouldBe(90);
    }

    [Fact]
    public void BorderTest_GetDrawableRect()
    {
        // Arrange
        var rect = new PaddingRect(10, 10, 100, 100);
        var style = new BlockStyle { Border = BoxBorder.Create(new BorderStyle { Width = 2f }) };

        // Act
        var styledRect = style.Border.GetBorderRect(rect);

        // Assert
        styledRect.Top.ShouldBe(8);
        styledRect.Bottom.ShouldBe(102);
        styledRect.Left.ShouldBe(8);
        styledRect.Right.ShouldBe(102);
    }

    [Fact]
    public void BorderTest_GetContentRect()
    {
        // Arrange
        var rect = new BorderRect(10, 10, 100, 100);
        var style = new BlockStyle { Border = BoxBorder.Create(new BorderStyle { Width = 2f }) };

        // Act
        var styledRect = style.Border.GetPaddingRect(rect);

        // Assert
        styledRect.Top.ShouldBe(12);
        styledRect.Bottom.ShouldBe(98);
        styledRect.Left.ShouldBe(12);
        styledRect.Right.ShouldBe(98);
    }
}
