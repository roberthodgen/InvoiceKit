namespace InvoiceKit.Tests.Pdf;

using InvoiceKit.Pdf;
using InvoiceKit.Pdf.Geometry;
using InvoiceKit.Pdf.Views;

public sealed class ColumnTests
{
    private static readonly OuterRect TestRect = new (0, 0, 500, 500);

    private readonly RootLayoutContext _root = new (TestRect.ToRect());

    [Fact]
    public void ColumnWidthPercent_GetColumnWidth_ReturnsCorrectWidth()
    {
        var context = _root.GetHorizontalChildContext();
        var columnPercent = ColumnWidthPercent.FromPercent(50);
        var columnWidth = columnPercent.GetColumnSize(context);
        columnWidth.ShouldBeEquivalentTo(new OuterSize(250, 500));
    }

    [Fact]
    public void ColumnWidthPoints_GetColumnWidth_ReturnsCorrectWidth()
    {
        var context = _root.GetHorizontalChildContext();
        var columnPoints = ColumnWidthPoints.FromPoints(200);
        var columnWidth = columnPoints.GetColumnSize(context);
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
        var exception = Should.Throw<ApplicationException>(() => columnPoints.GetColumnSize(context));
        exception.Message.ShouldContain("Points");
    }

    [Fact]
    public void ColumnWidthEqual_CreateEqualColumns_ReturnsCorrectList()
    {
        var numberOfColumns = 3;
        var columnWidths = ColumnWidthEqual.CreateEqualColumns(numberOfColumns);
        columnWidths.Count.ShouldBe(numberOfColumns);
    }

    [Fact]
    public void ColumnWidthEqual_GetColumnSize_ReturnsCorrectSize()
    {
        var context = _root.GetHorizontalChildContext();
        var columnWidths = ColumnWidthEqual.CreateEqualColumns(2);
        var columnWidth1 = columnWidths[0].GetColumnSize(context).Width;
        var columnWidth2 = columnWidths[1].GetColumnSize(context).Width;
        columnWidth1.ShouldBeEquivalentTo(columnWidth2);
    }

    [Fact]
    public void ColumnType_CanConvert_ReturnsTrue()
    {
        var columnType = ColumnType.Equal;
        columnType.CanBeConvertedTo(ColumnType.Percent).ShouldBeTrue();
        columnType.CanBeConvertedTo(ColumnType.Points).ShouldBeTrue();
    }

    [Fact]
    public void ColumnType_CanConvert_ReturnsFalse()
    {
        var columnType = ColumnType.Percent;
        columnType.CanBeConvertedTo(ColumnType.Equal).ShouldBeFalse();
        columnType.CanBeConvertedTo(ColumnType.Points).ShouldBeFalse();
    }

    [Fact]
    public void ColumnType_ToString_ReturnsCorrectStrings()
    {
        ColumnType.Equal.ToString().ShouldBe("Equal Width");
        ColumnType.Percent.ToString().ShouldBe("Percent");
        ColumnType.Points.ToString().ShouldBe("Points");
    }
}
