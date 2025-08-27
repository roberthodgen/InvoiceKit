namespace InvoiceKit.Pdf.Containers.Tables;

using Styles.Text;

public class TableLayoutBuilder(TextStyle defaultTextStyle) : IViewBuilder
{
    private readonly List<TableRow> _headers = [];

    private TextStyle _defaultTextStyle = defaultTextStyle; // TODO make this editable for the table

    public List<TableRow> Rows { get; } = [];

    public List<TableColumn> Columns = [];

    /// <summary>
    /// Specifies how column sizes will be computed.
    /// </summary>
    private ColumnSizing ColumnSizing { get; set; } = ColumnSizing.Equal;

    private List<ColumnWidthPercent> ColumnWidthPercentages { get; set; } = [];

    private TextStyle TableHeaderStyle { get; } = defaultTextStyle with { FontPath = "Open Sans/Bold", };

    public bool ShowRowSeparators { get; private set; } = false;

    public TableLayoutBuilder AddHeader(Action<TableRow> config)
    {
        var row = new TableRow(this, TableHeaderStyle);
        config(row);
        _headers.Add(row);
        return this;
    }

    public TableLayoutBuilder AddRow(Action<TableRow> config)
    {
        var row = new TableRow(this, _defaultTextStyle);
        config(row);
        Rows.Add(row);
        return this;
    }

    // public SKSize Measure(SKSize available)
    // {
    //     var width = available.Width; // always fills the available width
    //     var height = Rows.Sum(row => row.Measure(available).Height);
    //     height += _headers.Sum(row => row.Measure(available).Height);
    //     return new SKSize(width, height);
    // }
    //
    // public void Draw(PageLayout page)
    // {
    //     // var top = page.Available.Top;
    //     // foreach (var row in _headers)
    //     // {
    //     //     var rowHeight = row.Measure(page.Available.Size).Height;
    //     //     row.Draw(page, new SKRect(page.Available.Left, top, page.Available.Left + page.Available.Width, top + rowHeight));
    //     //     top += rowHeight;
    //     // }
    //     //
    //     // foreach (var row in Rows)
    //     // {
    //     //     // TODO detect when page changes and re-draw header row(s)
    //     //     var rowHeight = row.Measure(page.Available.Size).Height;
    //     //     row.Draw(context, new SKRect(page.Available.Left, top, page.Available.Left + page.Available.Width, top + rowHeight));
    //     //     top += rowHeight;
    //     // }
    // }

    public TableLayoutBuilder UseEquallySpaceColumns()
    {
        ColumnSizing = ColumnSizing.Equal;
        return this;
    }

    public TableLayoutBuilder UseFixedColumnWidths(List<ColumnWidthPercent> columnWidths)
    {
        ColumnSizing = ColumnSizing.FixedPercentage;
        ColumnWidthPercentages = columnWidths;
        return this;
    }

    /// <summary>
    /// Adds row separators between rows. 
    /// </summary>
    public TableLayoutBuilder AddRowSeparators()
    {
        ShowRowSeparators = true;
        return this;
    }

    internal TableColumn GetOrAddColumn(int columnIndex)
    {
        var column = Columns.ElementAtOrDefault(columnIndex);
        if (column is null)
        {
            // TODO handle sizing
            column = new TableColumn(columnIndex);
            Columns.Add(column);
        }

        return column;
    }

    public float GetColumnWidth(float available, int columnIndex) =>
        ColumnSizing switch
        {
            ColumnSizing.Equal => available / Columns.Count,
            ColumnSizing.FixedPercentage => available * ColumnWidthPercentages[columnIndex].Percent,
            ColumnSizing.FixedPoints => throw new NotImplementedException("TODO"),
            ColumnSizing.Auto => throw new NotImplementedException("TODO"),
            _ => throw new NotImplementedException(),
        };

    public void Dispose()
    {
    }

    public ILayout ToLayout(PageLayout page)
    {
        throw new NotImplementedException();
    }
}
