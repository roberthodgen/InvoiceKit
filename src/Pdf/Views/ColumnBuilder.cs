namespace InvoiceKit.Pdf.Views;

public sealed class ColumnBuilder : IColumnBuilder
{
    private readonly List<IColumnWidth> _columnWidths = [];

    public ColumnType ColumnType { get; private set; } = ColumnType.Equal;

    public IReadOnlyList<IColumnWidth> ColumnWidths => _columnWidths.AsReadOnly();

    private float _columnSum;

    public IColumnBuilder AddColumnPercent(float percent)
    {
        if (ColumnType.CanBeConvertedTo(ColumnType.Percent) == false)
        {
            throw new ApplicationException("Can only have either percents or points, not both.");
        }

        _columnSum += percent;
        if (_columnSum > 100)
        {
            throw new ApplicationException("Can only have a maximum of 100 percent points.");
        }

        ColumnType = ColumnType.Percent;
        _columnWidths.Add(ColumnWidthPercent.FromPercent(percent));
        return this;
    }

    public IColumnBuilder AddColumnPoints(float points)
    {
        if (ColumnType.CanBeConvertedTo(ColumnType.Points) == false)
        {
            throw new ApplicationException("Can only have either percents or points, not both.");
        }

        ColumnType = ColumnType.Points;
        _columnWidths.Add(ColumnWidthPoints.FromPoints(points));
        return this;
    }
}
