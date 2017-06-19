using System.Collections.Generic;

namespace MvcGrid.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PagingModel
    {
        /// <summary>
        /// 
        /// </summary>
        public PagingModel()
        {
            PageLinks = new Dictionary<int, string>();
        }

        /// <summary>
        /// 
        /// </summary>
        public int CurrentPage
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int FirstRecord
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int LastRecord
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int NumberOfPages
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<int, string> PageLinks
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int TotalRecords
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagesToDisplay"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
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
    /// 
    /// </summary>
    public class RenderingModel
    {
        /// <summary>
        /// 
        /// </summary>
        public RenderingModel()
        {
            Columns = new List<Column>();
            Rows = new List<Row>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string ClientDataTransferHtmlBlock
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Column> Columns
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string HandlerPath
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string NextButtonCaption
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string NoResultsMessage
        {
            get;
            set;
        }

        /// <summary>
        ///     Paging data. Will be null if paging should not be displayed
        /// </summary>
        public PagingModel PagingModel
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string PreviousButtonCaption
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ProcessingMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Row> Rows
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SummaryMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string TableHtmlId
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// 
        /// </summary>
        public string CalculatedCssClass
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string HtmlText
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string PlainText
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Row
    {
        /// <summary>
        /// 
        /// </summary>
        public Row()
        {
            Cells = new Dictionary<string, Cell>();
        }

        /// <summary>
        /// 
        /// </summary>
        public string CalculatedCssClass
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, Cell> Cells
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Column
    {
        /// <summary>
        /// 
        /// </summary>
        public string HeaderText
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Onclick
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public SortDirection? SortIconDirection
        {
            get;
            set;
        }
    }
}