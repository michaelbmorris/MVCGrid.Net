using System.Collections.Generic;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    ///     Class PagingModel.
    /// </summary>
    /// TODO Edit XML Comment Template for PagingModel
    public class PagingModel
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="PagingModel" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public PagingModel()
        {
            PageLinks = new Dictionary<int, string>();
        }

        /// <summary>
        ///     Gets or sets the current page.
        /// </summary>
        /// <value>The current page.</value>
        /// TODO Edit XML Comment Template for CurrentPage
        public int CurrentPage
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the first record.
        /// </summary>
        /// <value>The first record.</value>
        /// TODO Edit XML Comment Template for FirstRecord
        public int FirstRecord
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the last record.
        /// </summary>
        /// <value>The last record.</value>
        /// TODO Edit XML Comment Template for LastRecord
        public int LastRecord
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the number of pages.
        /// </summary>
        /// <value>The number of pages.</value>
        /// TODO Edit XML Comment Template for NumberOfPages
        public int NumberOfPages
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the page links.
        /// </summary>
        /// <value>The page links.</value>
        /// TODO Edit XML Comment Template for PageLinks
        public Dictionary<int, string> PageLinks
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the total records.
        /// </summary>
        /// <value>The total records.</value>
        /// TODO Edit XML Comment Template for TotalRecords
        public int TotalRecords
        {
            get;
            set;
        }

        /// <summary>
        ///     Calculates the page start and end.
        /// </summary>
        /// <param name="pagesToDisplay">The pages to display.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// TODO Edit XML Comment Template for CalculatePageStartAndEnd
        public void CalculatePageStartAndEnd(
            int pagesToDisplay,
            out int start,
            out int end)
        {
            var pageToStart = CurrentPage - (pagesToDisplay - 1) / 2;
            if (pageToStart < 1)
            {
                pageToStart = 1;
            }

            var pageToEnd = pageToStart + (pagesToDisplay - 1);

            if (pageToEnd > NumberOfPages)
            {
                var diff = pageToEnd - NumberOfPages;

                pageToEnd = NumberOfPages;
                pageToStart = pageToStart - diff;
            }
            if (pageToStart < 1)
            {
                pageToStart = 1;
            }

            start = pageToStart;
            end = pageToEnd;
        }
    }

    /// <summary>
    ///     Class RenderingModel.
    /// </summary>
    /// TODO Edit XML Comment Template for RenderingModel
    public class RenderingModel
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="RenderingModel" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public RenderingModel()
        {
            Columns = new List<Column>();
            Rows = new List<Row>();
        }

        /// <summary>
        ///     Gets or sets the client data transfer HTML block.
        /// </summary>
        /// <value>The client data transfer HTML block.</value>
        /// TODO Edit XML Comment Template for ClientDataTransferHtmlBlock
        public string ClientDataTransferHtmlBlock
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the columns.
        /// </summary>
        /// <value>The columns.</value>
        /// TODO Edit XML Comment Template for Columns
        public List<Column> Columns
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the handler path.
        /// </summary>
        /// <value>The handler path.</value>
        /// TODO Edit XML Comment Template for HandlerPath
        public string HandlerPath
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the next button caption.
        /// </summary>
        /// <value>The next button caption.</value>
        /// TODO Edit XML Comment Template for NextButtonCaption
        public string NextButtonCaption
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the no results message.
        /// </summary>
        /// <value>The no results message.</value>
        /// TODO Edit XML Comment Template for NoResultsMessage
        public string NoResultsMessage
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the paging model.
        /// </summary>
        /// <value>The paging model.</value>
        /// TODO Edit XML Comment Template for PagingModel
        public PagingModel PagingModel
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the previous button caption.
        /// </summary>
        /// <value>The previous button caption.</value>
        /// TODO Edit XML Comment Template for PreviousButtonCaption
        public string PreviousButtonCaption
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the processing message.
        /// </summary>
        /// <value>The processing message.</value>
        /// TODO Edit XML Comment Template for ProcessingMessage
        public string ProcessingMessage
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        /// TODO Edit XML Comment Template for Rows
        public List<Row> Rows
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the summary message.
        /// </summary>
        /// <value>The summary message.</value>
        /// TODO Edit XML Comment Template for SummaryMessage
        public string SummaryMessage
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the table HTML identifier.
        /// </summary>
        /// <value>The table HTML identifier.</value>
        /// TODO Edit XML Comment Template for TableHtmlId
        public string TableHtmlId
        {
            get;
            set;
        }
    }

    /// <summary>
    ///     Class Cell.
    /// </summary>
    /// TODO Edit XML Comment Template for Cell
    public class Cell
    {
        /// <summary>
        ///     Gets or sets the calculated CSS class.
        /// </summary>
        /// <value>The calculated CSS class.</value>
        /// TODO Edit XML Comment Template for CalculatedCssClass
        public string CalculatedCssClass
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the HTML text.
        /// </summary>
        /// <value>The HTML text.</value>
        /// TODO Edit XML Comment Template for HtmlText
        public string HtmlText
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the plain text.
        /// </summary>
        /// <value>The plain text.</value>
        /// TODO Edit XML Comment Template for PlainText
        public string PlainText
        {
            get;
            set;
        }
    }

    /// <summary>
    ///     Class Row.
    /// </summary>
    /// TODO Edit XML Comment Template for Row
    public class Row
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Row" />
        ///     class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public Row()
        {
            Cells = new Dictionary<string, Cell>();
        }

        /// <summary>
        ///     Gets or sets the calculated CSS class.
        /// </summary>
        /// <value>The calculated CSS class.</value>
        /// TODO Edit XML Comment Template for CalculatedCssClass
        public string CalculatedCssClass
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the cells.
        /// </summary>
        /// <value>The cells.</value>
        /// TODO Edit XML Comment Template for Cells
        public Dictionary<string, Cell> Cells
        {
            get;
            set;
        }
    }

    /// <summary>
    ///     Class Column.
    /// </summary>
    /// TODO Edit XML Comment Template for Column
    public class Column
    {
        /// <summary>
        ///     Gets or sets the header text.
        /// </summary>
        /// <value>The header text.</value>
        /// TODO Edit XML Comment Template for HeaderText
        public string HeaderText
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        /// TODO Edit XML Comment Template for Name
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the onclick.
        /// </summary>
        /// <value>The onclick.</value>
        /// TODO Edit XML Comment Template for Onclick
        public string Onclick
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the sort icon direction.
        /// </summary>
        /// <value>The sort icon direction.</value>
        /// TODO Edit XML Comment Template for SortIconDirection
        public SortDirection? SortIconDirection
        {
            get;
            set;
        }
    }
}