namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using InvoiceKit.Pdf.Geometry;
using InvoiceKit.Pdf.Views;

public sealed class ColumnTests
{
    /// <summary>
    /// Points per inch.
    /// </summary>
    private static readonly OuterRect TestRect = new (0, 0, 500, 500);

    private readonly RootLayoutContext _root = new (TestRect.ToRect());

    [Fact]
    public void ColumnWidthPercent_GetColumnWidth_ReturnsCorrectWidth()
    {
        var context = _root.GetHorizontalChildContext();
        var columnPercent = ColumnWidthPercent.FromPercent(50);
        var columnWidth = columnPercent.GetColumnWidth(context);
        columnWidth.ShouldBeEquivalentTo(new OuterSize(250, 500));
    }

    [Fact]
    public void ColumnWidthPoints_GetColumnWidth_ReturnsCorrectWidth()
    {
        var context = _root.GetHorizontalChildContext();
        var columnPoints = ColumnWidthPoints.FromPoints(200);
        var columnWidth = columnPoints.GetColumnWidth(context);
        columnWidth.ShouldBeEquivalentTo(new OuterSize(200, 500));
    }

    [Fact]
    public void ColumnWidthBuilder_AddColumnPercent_ThrowsWhenOver100Percent()
    {
        var builder = new ColumnBuilder();
        var exception = Should.Throw<ApplicationException>(() => builder.AddColumnPercent(101));
        exception.Message.ShouldContain("Percent");
    }

    [Fact]
    public void ColumnWidthPoints_GetColumnWidth_ThrowsWhenGreaterThanContext()
    {
        var context = _root.GetHorizontalChildContext();
        var columnPoints = ColumnWidthPoints.FromPoints(501);
        var exception = Should.Throw<ApplicationException>(() => columnPoints.GetColumnWidth(context));
        exception.Message.ShouldContain("Points");
    }
}
