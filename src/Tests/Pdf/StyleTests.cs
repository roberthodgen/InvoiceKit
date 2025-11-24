namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf.Styles;
using SkiaSharp;

public sealed class StyleTests
{
    [Fact]
    public void MarginTest_GetDrawableRect()
    {
        // Arrange
        var rect = new SKRect(10, 10, 100, 100);
        var style = new BlockStyle { Margin = new Margin(10f, 10f, 10f, 10f) };

        // Act
        var styledRect = style.Margin.GetDrawableRect(rect);

        // Assert
        styledRect.Top.ShouldBe(0);
        styledRect.Bottom.ShouldBe(110);
        styledRect.Left.ShouldBe(0);
        styledRect.Right.ShouldBe(110);
    }

    [Fact]
    public void MarginTest_GetContentRect()
    {
        // Arrange
        var rect = new SKRect(10, 10, 100, 100);
        var style = new BlockStyle { Margin = new Margin(10f, 10f, 10f, 10f) };

        // Act
        var styledRect = style.Margin.GetContentRect(rect);

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
        var rect = new SKRect(10, 10, 100, 100);
        var style = new BlockStyle { Padding = new Padding(10f, 10f, 10f, 10f) };

        // Act
        var styledRect = style.Padding.GetDrawableRect(rect);

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
        var rect = new SKRect(10, 10, 100, 100);
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
        var rect = new SKRect(10, 10, 100, 100);
        var style = new BlockStyle { Border = BoxBorder.Create(new BorderStyle { Width = 2f }) };

        // Act
        var styledRect = style.Border.GetDrawableRect(rect);

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
        var rect = new SKRect(10, 10, 100, 100);
        var style = new BlockStyle { Border = BoxBorder.Create(new BorderStyle { Width = 2f }) };

        // Act
        var styledRect = style.Border.GetContentRect(rect);

        // Assert
        styledRect.Top.ShouldBe(12);
        styledRect.Bottom.ShouldBe(98);
        styledRect.Left.ShouldBe(12);
        styledRect.Right.ShouldBe(98);
    }
}
