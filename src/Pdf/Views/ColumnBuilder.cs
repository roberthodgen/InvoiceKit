namespace InvoiceKit.Pdf.Views;

using Containers.Tables;

public sealed class ColumnBuilder : IColumnBuilder
{
    public List<ColumnWidth> ColumnWidths { get; } = [];

    public List<ColumnWidth> Build()
    {
        return ColumnWidths;
    }

    public IColumnBuilder AddColumnPercent(float percent)
    {
        ColumnWidths.Add(new ColumnWidth(ColumnWidthType.Percentage, percent));
        return this;
    }

    public IColumnBuilder AddColumnPoints(float points)
    {
        ColumnWidths.Add(new ColumnWidth(ColumnWidthType.Points, points));
        return this;
    }
}
