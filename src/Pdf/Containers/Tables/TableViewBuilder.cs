namespace InvoiceKit.Pdf.Containers.Tables;

using SkiaSharp;
using Styles.Text;

public class TableViewBuilder : IViewBuilder
{
    private readonly List<TableRowViewBuilder> _headers = [];

    private readonly List<TableRowViewBuilder> _rows = [];

    private readonly TextStyle _defaultTextStyle; // TODO make this editable for the table

    /// <summary>
    /// Specifies how column sizes will be computed.
    /// </summary>
    private ColumnSizing ColumnSizing { get; set; } = ColumnSizing.Equal;

    private List<ColumnWidthPercent> ColumnWidthPercentages { get; set; } = [];

    private TextStyle TableHeaderStyle { get; }

    private bool ShowRowSeparators { get; set; }

    public IReadOnlyCollection<IViewBuilder> Children => [];

    internal TableViewBuilder(TextStyle defaultTextStyle)
    {
        _defaultTextStyle = defaultTextStyle;
        TableHeaderStyle = defaultTextStyle with {FontPath = "Open Sans/Bold"};
    }

    public TableViewBuilder AddHeader(Action<TableRowViewBuilder> config)
    {
        var row = new TableRowViewBuilder(TableHeaderStyle, ColumnWidthPercentages);
        config(row);
        _headers.Add(row);
        return this;
    }

    public TableViewBuilder AddRow(Action<TableRowViewBuilder> config)
    {
        var row = new TableRowViewBuilder(_defaultTextStyle, ColumnWidthPercentages);
        config(row);
        _rows.Add(row);
        return this;
    }

    public TableViewBuilder UseEquallySpaceColumns()
    {
        ColumnSizing = ColumnSizing.Equal;
        return this;
    }

    public TableViewBuilder UseFixedColumnWidths(List<ColumnWidthPercent> columnWidths)
    {
        ColumnSizing = ColumnSizing.FixedPercentage;
        ColumnWidthPercentages = columnWidths;
        return this;
    }

    /// <summary>
    /// Adds row separators between rows. 
    /// </summary>
    public TableViewBuilder AddRowSeparators()
    {
        ShowRowSeparators = true;
        return this;
    }

    public float GetColumnWidth(float available, int numberOfColumns, int columnIndex) =>
        ColumnSizing switch
        {
            ColumnSizing.Equal => available / numberOfColumns,
            ColumnSizing.FixedPercentage => available * ColumnWidthPercentages[columnIndex].Percent,
            ColumnSizing.FixedPoints => throw new NotImplementedException("TODO"),
            ColumnSizing.Auto => throw new NotImplementedException("TODO"),
            _ => throw new NotImplementedException(),
        };

    public ILayout ToLayout()
    {
        return new TableLayout(_headers, _rows, ShowRowSeparators);
    }

    public void Dispose()
    {
    }
}
