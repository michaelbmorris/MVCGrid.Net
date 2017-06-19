using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MvcGrid.Interfaces;

namespace MvcGrid.Models
{
    /// <summary>
    /// </summary>
    public abstract class GridDefinitionBase
    {
        internal abstract List<Row> GetData(
            GridContext context,
            out int? totalRecords);
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public class GridDefinition<T1> : GridDefinitionBase, IMvcGridDefinition
    {
        /// <summary>
        /// </summary>
        public GridDefinition()
            : this(null)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="gridDefaults"></param>
        public GridDefinition(GridDefaults gridDefaults)
        {
            Columns = new List<GridColumn<T1>>();

            if (gridDefaults == null)
            {
                gridDefaults = new GridDefaults();
            }
            PreloadData = gridDefaults.PreloadData;
            QueryOnPageLoad = gridDefaults.QueryOnPageLoad;
            Paging = gridDefaults.Paging;
            ItemsPerPage = gridDefaults.ItemsPerPage;
            Sorting = gridDefaults.Sorting;
            DefaultSortColumn = gridDefaults.DefaultSortColumn;
            NoResultsMessage = gridDefaults.NoResultsMessage;
            NextButtonCaption = gridDefaults.NextButtonCaption;
            PreviousButtonCaption = gridDefaults.PreviousButtonCaption;
            SummaryMessage = gridDefaults.SummaryMessage;
            ProcessingMessage = gridDefaults.ProcessingMessage;
            ClientSideLoadingMessageFunctionName = gridDefaults
                .ClientSideLoadingMessageFunctionName;
            ClientSideLoadingCompleteFunctionName = gridDefaults
                .ClientSideLoadingCompleteFunctionName;
            Filtering = gridDefaults.Filtering;
            //this.RenderingEngine = gridDefaults.RenderingEngine;
            TemplatingEngine = gridDefaults.TemplatingEngine;
            AdditionalSettings = gridDefaults.AdditionalSettings;
            RenderingMode = gridDefaults.RenderingMode;
            ViewPath = gridDefaults.ViewPath;
            ContainerViewPath = gridDefaults.ContainerViewPath;
            QueryStringPrefix = gridDefaults.QueryStringPrefix;
            ErrorMessageHtml = gridDefaults.ErrorMessageHtml;
            AdditionalQueryOptionNames =
                gridDefaults.AdditionalQueryOptionNames;
            PageParameterNames = gridDefaults.PageParameterNames;
            AllowChangingPageSize = gridDefaults.AllowChangingPageSize;
            MaxItemsPerPage = gridDefaults.MaxItemsPerPage;
            AuthorizationType = gridDefaults.AuthorizationType;

            RenderingEngines = gridDefaults.RenderingEngines;
            DefaultRenderingEngineName =
                gridDefaults.DefaultRenderingEngineName;
        }

        /// <summary>
        ///     This is the method that will actually query the data to populate the grid. Use entity framework, a module from you
        ///     IoC container, direct SQL queries, etc. to get the data. Inside the providee GridContext there is a QueryOptions
        ///     object which will be populated with the currently requested sorting, paging, and filtering options which you must
        ///     take into account. See the QueryOptions documentation below. You must return a QueryResult object which takes an
        ///     enumerable of your type and a count of the total number of records which must be provided if paging is enabled.
        /// </summary>
        public Func<GridContext, QueryResult<T1>> RetrieveData
        {
            get;
            set;
        }

        /// <summary>
        ///     Use this to specify a custom css class based on data for the current row
        /// </summary>
        public Func<T1, GridContext, string> RowCssClassExpression
        {
            get;
            set;
        }

        private List<GridColumn<T1>> Columns
        {
            get;
        }

        /// <summary>
        ///     Names of additional parameters that can be passed from client to server side
        /// </summary>
        public HashSet<string> AdditionalQueryOptionNames
        {
            get;
            set;
        }

        /// <summary>
        ///     Arbitrary additional settings
        /// </summary>
        public Dictionary<string, object> AdditionalSettings
        {
            get;
            set;
        }

        /// <summary>
        ///     Allows changing of page size from client-side
        /// </summary>
        public bool AllowChangingPageSize
        {
            get;
            set;
        }

        /// <summary>
        ///     Indicated the authorization type. Anonymous access is the default.
        /// </summary>
        public AuthorizationType AuthorizationType
        {
            get;
            set;
        }

        /// <summary>
        ///     Name of function to call before ajax call ends
        /// </summary>
        public string ClientSideLoadingCompleteFunctionName
        {
            get;
            set;
        }

        /// <summary>
        ///     Name of function to call before ajax call begins
        /// </summary>
        public string ClientSideLoadingMessageFunctionName
        {
            get;
            set;
        }

        /// <summary>
        ///     When RenderingMode is set to Controller, this is the path to the container razor view to use.
        /// </summary>
        public string ContainerViewPath
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public string DefaultRenderingEngineName
        {
            get;
            set;
        }

        /// <summary>
        ///     The default column to sort by when no sort is specified
        /// </summary>
        public string DefaultSortColumn
        {
            get;
            set;
        }

        /// <summary>
        ///     The default order to sort by when no sort is specified
        /// </summary>
        public SortDirection DefaultSortDirection
        {
            get;
            set;
        }

        /// <summary>
        ///     HTML to display in place of the grid when an error occurs
        /// </summary>
        public string ErrorMessageHtml
        {
            get;
            set;
        }

        /// <summary>
        ///     Enables filtering on the grid. Note, filtering must also be enabled on each column where filtering is wanted
        /// </summary>
        public bool Filtering
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetAdditionalSetting<T>(string name, T defaultValue)
        {
            //return (T)SortColumnData;
            if (!AdditionalSettings.ContainsKey(name))
            {
                return defaultValue;
            }

            var val = (T) AdditionalSettings[name];

            return val;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMvcGridColumn> GetColumns()
        {
            return Columns;
        }

        /// <summary>
        ///     Number of items to display on each page
        /// </summary>
        public int ItemsPerPage
        {
            get;
            set;
        }

        /// <summary>
        ///     Sets the maximum of items per page allowed when AllowChangingPageSize is enabled
        /// </summary>
        public int? MaxItemsPerPage
        {
            get;
            set;
        }

        /// <summary>
        ///     Text to display on the "next" button.
        /// </summary>
        public string NextButtonCaption
        {
            get;
            set;
        }

        /// <summary>
        ///     Text to display when there are no results.
        /// </summary>
        public string NoResultsMessage
        {
            get;
            set;
        }

        /// <summary>
        ///     Names of page parameters that will be passed from the view
        /// </summary>
        public HashSet<string> PageParameterNames
        {
            get;
            set;
        }

        /// <summary>
        ///     Enables paging on the grid
        /// </summary>
        public bool Paging
        {
            get;
            set;
        }

        /// <summary>
        ///     Enables data loading when the page is first loaded so that the initial ajax request can be skipped.
        /// </summary>
        public bool PreloadData
        {
            get;
            set;
        }

        /// <summary>
        ///     Text to display on the "previous" button.
        /// </summary>
        public string PreviousButtonCaption
        {
            get;
            set;
        }

        /// <summary>
        ///     Text to display when query is processed
        /// </summary>
        public string ProcessingMessage
        {
            get;
            set;
        }

        /// <summary>
        ///     Specified if the data should be loaded as soon as the page loads
        /// </summary>
        public bool QueryOnPageLoad
        {
            get;
            set;
        }

        /// <summary>
        ///     A prefix to add to all query string parameters for this grid, for when there are more than 1 grids on the same page
        /// </summary>
        public string QueryStringPrefix
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public ProviderSettingsCollection RenderingEngines
        {
            get;
            set;
        }

        /// <summary>
        ///     The rendering mode to use for this grid. By default it will use the RenderingEngine rendering mode. If you want to
        ///     use a custom Razor view to display your grid, change this to Controller
        /// </summary>
        public RenderingMode RenderingMode
        {
            get;
            set;
        }

        /// <summary>
        ///     Enables sorting on the grid. Note, sorting must also be enabled on each column where sorting is wanted
        /// </summary>
        public bool Sorting
        {
            get;
            set;
        }

        /// <summary>
        ///     Summary text to display in grid footer
        /// </summary>
        public string SummaryMessage
        {
            get;
            set;
        }

        //public Type RenderingEngine { get; set; }

        /// <summary>
        /// </summary>
        public Type TemplatingEngine
        {
            get;
            set;
        }

        /// <summary>
        ///     When RenderingMode is set to Controller, this is the path to the razor view to use.
        /// </summary>
        public string ViewPath
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        public void AddColumn(GridColumn<T1> column)
        {
            var thisName = column.ColumnName;
            if (string.IsNullOrWhiteSpace(thisName))
            {
                throw new ArgumentException(
                    "Please specify a unique column name for each column",
                    nameof(column));
            }

            column.ColumnName = column.ColumnName.Trim();

            if (Columns.Any(
                p => string.Equals(p.ColumnName, column.ColumnName, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw new ArgumentException(
                    $"There is already a column added with the name '{column.ColumnName}'",
                    nameof(column));
            }

            if (column.ValueExpression == null
                && column.ValueTemplate == null)
            {
                throw new ArgumentException(
                    $"Column '{column.ColumnName}' is missing a value expression.",
                    nameof(column));
            }

            Columns.Add(column);
        }

        internal override List<Row> GetData(
            GridContext context,
            out int? totalRecords)
        {
            var resultRows = new List<Row>();

            var queryResult = RetrieveData(context);
            totalRecords = queryResult.TotalRecords;

            if (context.GridDefinition.Paging
                && !totalRecords.HasValue)
            {
                throw new Exception(
                    "When paging is enabled, QueryResult must contain the TotalRecords");
            }

            var templatingEngine =
                (IMvcGridTemplatingEngine) Activator.CreateInstance(
                    context.GridDefinition.TemplatingEngine,
                    true);

            foreach (var item in queryResult.Items)
            {
                var thisRow = new Row();

                var rowCss = RowCssClassExpression?.Invoke(item, context);

                if (!string.IsNullOrWhiteSpace(rowCss))
                {
                    thisRow.CalculatedCssClass = rowCss;
                }

                foreach (var col in Columns)
                {
                    var thisCell = new Cell();
                    thisRow.Cells.Add(col.ColumnName, thisCell);

                    thisCell.HtmlText = "";

                    if (col.ValueExpression != null)
                    {
                        thisCell.HtmlText = col.ValueExpression(item, context);
                    }

                    if (!string.IsNullOrWhiteSpace(col.ValueTemplate))
                    {
                        var templateModel = new TemplateModel
                        {
                            Item = item,
                            GridContext = context,
                            GridColumn = col,
                            Row = thisRow,
                            Value = thisCell.HtmlText
                        };

                        thisCell.HtmlText = templatingEngine.Process(
                            col.ValueTemplate,
                            templateModel);
                    }

                    if (col.HtmlEncode)
                    {
                        thisCell.HtmlText =
                            HttpUtility.HtmlEncode(thisCell.HtmlText);
                    }

                    thisCell.PlainText = thisCell.HtmlText;
                    if (col.PlainTextValueExpression != null)
                    {
                        thisCell.PlainText =
                            col.PlainTextValueExpression(item, context);
                    }

                    var cellCss = col.CellCssClassExpression?.Invoke(item, context);

                    if (!string.IsNullOrWhiteSpace(cellCss))
                    {
                        thisCell.CalculatedCssClass = cellCss;
                    }
                }

                resultRows.Add(thisRow);
            }

            return resultRows;
        }
    }
}