namespace InvoiceKit.Pdf.Views;

public sealed class ColumnBuilder : IColumnBuilder
{
    private readonly List<IColumnWidth> _columnWidths = [];

    private Type? _columnType;

    private float _columnSum;

    public List<IColumnWidth> Build()
    {
        return _columnWidths;
    }

    public IColumnBuilder AddColumnPercent(float percent)
    {
        if (_columnType is not null && _columnType != typeof(ColumnWidthPercent))
        {
            throw new ApplicationException("Can only have either percents or points, not both.");
        }

        _columnSum += percent;
        if (_columnSum > 100)
        {
            throw new ApplicationException("Can only have a maximum of 100 percent points.");
        }

        _columnType = typeof(ColumnWidthPercent);
        _columnWidths.Add(ColumnWidthPercent.FromPercent(percent));
        return this;
    }

    public IColumnBuilder AddColumnPoints(float points)
    {
        if (_columnType is not null && _columnType != typeof(ColumnWidthPoints))
        {
            throw new ApplicationException("Can only have either percents or points, not both.");
        }

        _columnType = typeof(ColumnWidthPoints);
        _columnWidths.Add(ColumnWidthPoints.FromPoints(points));
        return this;
    }
}
