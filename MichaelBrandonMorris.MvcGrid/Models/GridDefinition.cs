using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MichaelBrandonMorris.MvcGrid.Interfaces;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    ///     Class GridDefinitionBase.
    /// </summary>
    /// TODO Edit XML Comment Template for GridDefinitionBase
    public abstract class GridDefinitionBase
    {
        /// <summary>
        ///     Gets the data.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="totalRecords">The total records.</param>
        /// <returns>List&lt;Row&gt;.</returns>
        /// TODO Edit XML Comment Template for GetData
        internal abstract List<Row> GetData(
            GridContext context,
            out int? totalRecords);
    }

    /// <summary>
    ///     Class GridDefinition.
    /// </summary>
    /// <typeparam name="T1">The type of the t1.</typeparam>
    /// <seealso
    ///     cref="MichaelBrandonMorris.MvcGrid.Models.GridDefinitionBase" />
    /// <seealso
    ///     cref="MichaelBrandonMorris.MvcGrid.Interfaces.IMvcGridDefinition" />
    /// TODO Edit XML Comment Template for GridDefinition`1
    public class GridDefinition<T1> : GridDefinitionBase, IMvcGridDefinition
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GridDefinition{T1}" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public GridDefinition()
            : this(null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GridDefinition{T1}" /> class.
        /// </summary>
        /// <param name="gridDefaults">The grid defaults.</param>
        /// TODO Edit XML Comment Template for #ctor
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
        ///     Gets or sets the retrieve data.
        /// </summary>
        /// <value>The retrieve data.</value>
        /// TODO Edit XML Comment Template for RetrieveData
        public Func<GridContext, QueryResult<T1>> RetrieveData
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the row CSS class expression.
        /// </summary>
        /// <value>The row CSS class expression.</value>
        /// TODO Edit XML Comment Template for RowCssClassExpression
        public Func<T1, GridContext, string> RowCssClassExpression
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the columns.
        /// </summary>
        /// <value>The columns.</value>
        /// TODO Edit XML Comment Template for Columns
        private List<GridColumn<T1>> Columns
        {
            get;
        }

        /// <summary>
        ///     Gets or sets the additional query option names.
        /// </summary>
        /// <value>The additional query option names.</value>
        /// TODO Edit XML Comment Template for AdditionalQueryOptionNames
        public HashSet<string> AdditionalQueryOptionNames
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the additional settings.
        /// </summary>
        /// <value>The additional settings.</value>
        /// TODO Edit XML Comment Template for AdditionalSettings
        public Dictionary<string, object> AdditionalSettings
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [allow changing
        ///     page size].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [allow changing page size];
        ///     otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for AllowChangingPageSize
        public bool AllowChangingPageSize
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the type of the authorization.
        /// </summary>
        /// <value>The type of the authorization.</value>
        /// TODO Edit XML Comment Template for AuthorizationType
        public AuthorizationType AuthorizationType
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the name of the client side loading
        ///     complete function.
        /// </summary>
        /// <value>
        ///     The name of the client side loading complete
        ///     function.
        /// </value>
        /// TODO Edit XML Comment Template for ClientSideLoadingCompleteFunctionName
        public string ClientSideLoadingCompleteFunctionName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the name of the client side loading
        ///     message function.
        /// </summary>
        /// <value>
        ///     The name of the client side loading message
        ///     function.
        /// </value>
        /// TODO Edit XML Comment Template for ClientSideLoadingMessageFunctionName
        public string ClientSideLoadingMessageFunctionName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the container view path.
        /// </summary>
        /// <value>The container view path.</value>
        /// TODO Edit XML Comment Template for ContainerViewPath
        public string ContainerViewPath
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the default name of the rendering engine.
        /// </summary>
        /// <value>The default name of the rendering engine.</value>
        /// TODO Edit XML Comment Template for DefaultRenderingEngineName
        public string DefaultRenderingEngineName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the default sort column.
        /// </summary>
        /// <value>The default sort column.</value>
        /// TODO Edit XML Comment Template for DefaultSortColumn
        public string DefaultSortColumn
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the default sort direction.
        /// </summary>
        /// <value>The default sort direction.</value>
        /// TODO Edit XML Comment Template for DefaultSortDirection
        public SortDirection DefaultSortDirection
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the error message HTML.
        /// </summary>
        /// <value>The error message HTML.</value>
        /// TODO Edit XML Comment Template for ErrorMessageHtml
        public string ErrorMessageHtml
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this
        ///     <see cref="IMvcGridDefinition" /> is filtering.
        /// </summary>
        /// <value><c>true</c> if filtering; otherwise, <c>false</c>.</value>
        /// TODO Edit XML Comment Template for Filtering
        public bool Filtering
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the additional setting.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>T.</returns>
        /// TODO Edit XML Comment Template for GetAdditionalSetting`1
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
        ///     Gets the columns.
        /// </summary>
        /// <returns>IEnumerable&lt;IMvcGridColumn&gt;.</returns>
        /// TODO Edit XML Comment Template for GetColumns
        public IEnumerable<IMvcGridColumn> GetColumns()
        {
            return Columns;
        }

        /// <summary>
        ///     Gets or sets the items per page.
        /// </summary>
        /// <value>The items per page.</value>
        /// TODO Edit XML Comment Template for ItemsPerPage
        public int ItemsPerPage
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the maximum items per page.
        /// </summary>
        /// <value>The maximum items per page.</value>
        /// TODO Edit XML Comment Template for MaxItemsPerPage
        public int? MaxItemsPerPage
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
        ///     Gets or sets the page parameter names.
        /// </summary>
        /// <value>The page parameter names.</value>
        /// TODO Edit XML Comment Template for PageParameterNames
        public HashSet<string> PageParameterNames
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this
        ///     <see cref="IMvcGridDefinition" /> is paging.
        /// </summary>
        /// <value><c>true</c> if paging; otherwise, <c>false</c>.</value>
        /// TODO Edit XML Comment Template for Paging
        public bool Paging
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets a value indicating whether [preload data].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [preload data]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for PreloadData
        public bool PreloadData
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
        ///     Gets or sets a value indicating whether [query on page
        ///     load].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [query on page load]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for QueryOnPageLoad
        public bool QueryOnPageLoad
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the query string prefix.
        /// </summary>
        /// <value>The query string prefix.</value>
        /// TODO Edit XML Comment Template for QueryStringPrefix
        public string QueryStringPrefix
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the rendering engines.
        /// </summary>
        /// <value>The rendering engines.</value>
        /// TODO Edit XML Comment Template for RenderingEngines
        public ProviderSettingsCollection RenderingEngines
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the rendering mode.
        /// </summary>
        /// <value>The rendering mode.</value>
        /// TODO Edit XML Comment Template for RenderingMode
        public RenderingMode RenderingMode
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this
        ///     <see cref="IMvcGridDefinition" /> is sorting.
        /// </summary>
        /// <value><c>true</c> if sorting; otherwise, <c>false</c>.</value>
        /// TODO Edit XML Comment Template for Sorting
        public bool Sorting
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

        //public Type RenderingEngine { get; set; }

        /// <summary>
        ///     Gets or sets the templating engine.
        /// </summary>
        /// <value>The templating engine.</value>
        /// TODO Edit XML Comment Template for TemplatingEngine
        public Type TemplatingEngine
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the view path.
        /// </summary>
        /// <value>The view path.</value>
        /// TODO Edit XML Comment Template for ViewPath
        public string ViewPath
        {
            get;
            set;
        }

        /// <summary>
        ///     Adds the column.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <exception cref="ArgumentException">
        ///     Please specify a unique column name for each column -
        ///     column
        ///     or
        ///     column
        ///     or
        ///     column
        /// </exception>
        /// TODO Edit XML Comment Template for AddColumn
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
                p => string.Equals(
                    p.ColumnName,
                    column.ColumnName,
                    StringComparison.CurrentCultureIgnoreCase)))
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

        /// <summary>
        ///     Gets the data.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="totalRecords">The total records.</param>
        /// <returns>List&lt;Row&gt;.</returns>
        /// <exception cref="Exception">
        ///     When paging is enabled,
        ///     QueryResult must contain the TotalRecords
        /// </exception>
        /// TODO Edit XML Comment Template for GetData
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

                    var cellCss =
                        col.CellCssClassExpression?.Invoke(item, context);

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