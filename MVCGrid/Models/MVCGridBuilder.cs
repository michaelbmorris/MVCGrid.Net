using System;
using System.Configuration;

namespace MvcGrid.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public class MvcGridBuilder<T1>
    {
        private readonly ColumnDefaults _columnDefaults;

        /// <summary>
        /// 
        /// </summary>
        public MvcGridBuilder()
        {
            GridDefinition = new GridDefinition<T1>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridDefaults"></param>
        public MvcGridBuilder(GridDefaults gridDefaults)
            : this(gridDefaults, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnDefaults"></param>
        public MvcGridBuilder(ColumnDefaults columnDefaults)
            : this(null, columnDefaults)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridDefaults"></param>
        /// <param name="columnDefaults"></param>
        public MvcGridBuilder(
            GridDefaults gridDefaults,
            ColumnDefaults columnDefaults)
        {
            GridDefinition = new GridDefinition<T1>(gridDefaults);

            _columnDefaults = columnDefaults;
        }

        /// <summary>
        /// 
        /// </summary>
        public GridDefinition<T1> GridDefinition
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="headerText"></param>
        /// <param name="valueExpression"></param>
        /// <param name="enableSort"></param>
        /// <param name="htmlEncode"></param>
        /// <param name="plainTextValueExpression"></param>
        /// <param name="cellCssClassExpression"></param>
        /// <returns></returns>
        public MvcGridBuilder<T1> AddColumn(
            string name,
            string headerText,
            Func<T1, GridContext, string> valueExpression,
            bool enableSort = true,
            bool htmlEncode = true,
            Func<T1, GridContext, string> plainTextValueExpression = null,
            Func<T1, GridContext, string> cellCssClassExpression = null)
        {
            var col = new GridColumn<T1>
            {
                ColumnName = name,
                HeaderText = headerText,
                ValueExpression = valueExpression,
                HtmlEncode = htmlEncode,
                EnableSorting = enableSort,
                PlainTextValueExpression = plainTextValueExpression,
                CellCssClassExpression = cellCssClassExpression
            };

            GridDefinition.AddColumn(col);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public MvcGridBuilder<T1> AddColumn(GridColumn<T1> column)
        {
            GridDefinition.AddColumn(column);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public MvcGridBuilder<T1> AddColumns(
            Action<GridColumnListBuilder<T1>> columns)
        {
            var cols = new GridColumnListBuilder<T1>(_columnDefaults);
            columns.Invoke(cols);

            foreach (var col in cols.ColumnBuilders)
            {
                GridDefinition.AddColumn(col.GridColumn);
            }

            return this;
        }

        /// <summary>
        ///     Adds a rendering engine to the list of configured rendering engines.
        /// </summary>
        /// <param name="name">A unique name.</param>
        /// <param name="renderingEngineType"></param>
        /// <returns></returns>
        public MvcGridBuilder<T1> AddRenderingEngine(
            string name,
            Type renderingEngineType)
        {
            var fullyQualifiedName = renderingEngineType.AssemblyQualifiedName;
            GridDefinition.RenderingEngines.Add(
                new ProviderSettings(name, fullyQualifiedName));
            return this;
        }

        /// <summary>
        ///     Adds a rendering engine to the list of configured rendering engines.
        /// </summary>
        /// <param name="name">A unique name.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public MvcGridBuilder<T1> AddRenderingEngine(string name, string type)
        {
            GridDefinition.RenderingEngines.Add(
                new ProviderSettings(name, type));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public MvcGridBuilder<T1> RemoveRenderingEngine(string name)
        {
            GridDefinition.RenderingEngines.Remove(name);
            return this;
        }

        /// <summary>
        ///     Add a name to additional query options which are additional parameters that can be passed from client to server
        ///     side
        /// </summary>
        public MvcGridBuilder<T1> WithAdditionalQueryOptionName(string name)
        {
            GridDefinition.AdditionalQueryOptionNames.Add(name);
            return this;
        }

        /// <summary>
        ///     Names of additional parameters that can be passed from client to server side
        /// </summary>
        public MvcGridBuilder<T1> WithAdditionalQueryOptionNames(
            params string[] names)
        {
            foreach (var name in names)
            {
                GridDefinition.AdditionalQueryOptionNames.Add(name);
            }

            return this;
        }

        /// <summary>
        ///     Add an arbitrary additional settings
        /// </summary>
        public MvcGridBuilder<T1> WithAdditionalSetting(
            string name,
            object value)
        {
            GridDefinition.AdditionalSettings[name] = value;
            return this;
        }

        /// <summary>
        ///     Allows changing of page size from client-side
        /// </summary>
        public MvcGridBuilder<T1> WithAllowChangingPageSize(bool allow)
        {
            GridDefinition.AllowChangingPageSize = allow;
            return this;
        }

        /// <summary>
        ///     Indicated the authorization type. Anonymous access is the default.
        /// </summary>
        public MvcGridBuilder<T1> WithAuthorizationType(
            AuthorizationType authType)
        {
            GridDefinition.AuthorizationType = authType;
            return this;
        }

        /// <summary>
        ///     Name of function to call before ajax call ends
        /// </summary>
        public MvcGridBuilder<T1> WithClientSideLoadingCompleteFunctionName(
            string name)
        {
            GridDefinition.ClientSideLoadingCompleteFunctionName = name;
            return this;
        }

        /// <summary>
        ///     Name of function to call before ajax call begins
        /// </summary>
        public MvcGridBuilder<T1> WithClientSideLoadingMessageFunctionName(
            string name)
        {
            GridDefinition.ClientSideLoadingMessageFunctionName = name;
            return this;
        }

        /// <summary>
        ///     When RenderingMode is set to Controller, this is the path to the container razor view to use.
        /// </summary>
        public MvcGridBuilder<T1> WithContainerViewPath(
            string containerViewPath)
        {
            GridDefinition.ContainerViewPath = containerViewPath;
            return this;
        }

        /// <summary>
        ///     Sets the default rendering engine name (which should match a name from the RenderingEngines property) which
        ///     will be used when no rendering engine name is specified in the request
        /// </summary>
        /// <param name="renderingEngineName">Name of the rendering engine.</param>
        /// <returns></returns>
        public MvcGridBuilder<T1> WithDefaultRenderingEngineName(
            string renderingEngineName)
        {
            GridDefinition.DefaultRenderingEngineName = renderingEngineName;
            return this;
        }

        /// <summary>
        ///     The default column to sort by when no sort is specified
        /// </summary>
        public MvcGridBuilder<T1> WithDefaultSortColumn(
            string defaultSortColumn)
        {
            GridDefinition.DefaultSortColumn = defaultSortColumn;
            return this;
        }

        /// <summary>
        ///     The default order to sort by when no sort is specified
        /// </summary>
        public MvcGridBuilder<T1> WithDefaultSortDirection(
            SortDirection sortDirection)
        {
            GridDefinition.DefaultSortDirection = sortDirection;
            return this;
        }

        /// <summary>
        ///     HTML to display in place of the grid when an error occurs
        /// </summary>
        public MvcGridBuilder<T1> WithErrorMessageHtml(string errorMessageHtml)
        {
            GridDefinition.ErrorMessageHtml = errorMessageHtml;
            return this;
        }

        /// <summary>
        ///     Enables filtering on the grid. Note, filtering must also be enabled on each column where filtering is wanted
        /// </summary>
        public MvcGridBuilder<T1> WithFiltering(bool filtering)
        {
            GridDefinition.Filtering = filtering;
            return this;
        }


        /// <summary>
        ///     Number of items to display on each page
        /// </summary>
        public MvcGridBuilder<T1> WithItemsPerPage(int itemsPerPage)
        {
            GridDefinition.ItemsPerPage = itemsPerPage;
            return this;
        }

        /// <summary>
        ///     Sets the maximum of items per page allowed when AllowChangingPageSize is enabled
        /// </summary>
        public MvcGridBuilder<T1> WithMaxItemsPerPage(int maxItems)
        {
            GridDefinition.MaxItemsPerPage = maxItems;
            return this;
        }


        /// <summary>
        ///     Text to display on the "next" button.
        /// </summary>
        /// <param name="nextButtonCaption"></param>
        /// <returns></returns>
        public MvcGridBuilder<T1> WithNextButtonCaption(
            string nextButtonCaption)
        {
            GridDefinition.NextButtonCaption = nextButtonCaption;
            return this;
        }

        /// <summary>
        ///     Text to display when there are no results.
        /// </summary>
        public MvcGridBuilder<T1> WithNoResultsMessage(string noResultsMessage)
        {
            GridDefinition.NoResultsMessage = noResultsMessage;
            return this;
        }

        /// <summary>
        ///     Remove an additional setting
        /// </summary>
        public MvcGridBuilder<T1> WithoutAdditionalSetting(string name)
        {
            if (GridDefinition.AdditionalSettings.ContainsKey(name))
            {
                GridDefinition.AdditionalSettings.Remove(name);
            }
            return this;
        }

        /// <summary>
        ///     Names of page parameters that will be passed from the view
        /// </summary>
        public MvcGridBuilder<T1> WithPageParameterNames(params string[] names)
        {
            foreach (var name in names)
            {
                GridDefinition.PageParameterNames.Add(name);
            }

            return this;
        }

        /// <summary>
        ///     Enables paging on the grid
        /// </summary>
        public MvcGridBuilder<T1> WithPaging(bool paging)
        {
            GridDefinition.Paging = paging;
            return this;
        }

        /// <summary>
        ///     Enables paging on the grid
        /// </summary>
        public MvcGridBuilder<T1> WithPaging(bool paging, int itemsPerPage)
        {
            GridDefinition.Paging = paging;
            GridDefinition.ItemsPerPage = itemsPerPage;
            return this;
        }

        /// <summary>
        ///     Enables paging on the grid
        /// </summary>
        public MvcGridBuilder<T1> WithPaging(
            bool paging,
            int itemsPerPage,
            bool allowChangePageSize,
            int maxItemsPerPage)
        {
            GridDefinition.Paging = paging;
            GridDefinition.ItemsPerPage = itemsPerPage;
            GridDefinition.AllowChangingPageSize = allowChangePageSize;
            GridDefinition.MaxItemsPerPage = maxItemsPerPage;
            return this;
        }

        /// <summary>
        ///     Enables data loading when the page is first loaded so that the initial ajax request can be skipped.
        /// </summary>
        public MvcGridBuilder<T1> WithPreloadData(bool preload)
        {
            GridDefinition.PreloadData = preload;
            return this;
        }

        /// <summary>
        ///     Text to display on the "previous" button.
        /// </summary>
        /// <param name="previousButtonCaption"></param>
        /// <returns></returns>
        public MvcGridBuilder<T1> WithPreviousButtonCaption(
            string previousButtonCaption)
        {
            GridDefinition.PreviousButtonCaption = previousButtonCaption;
            return this;
        }

        /// <summary>
        ///     Text to display when query is processed
        /// </summary>
        /// <param name="processingMessage"></param>
        /// <returns></returns>
        public MvcGridBuilder<T1> WithProcessingMessage(
            string processingMessage)
        {
            GridDefinition.ProcessingMessage = processingMessage;
            return this;
        }

        /// <summary>
        ///     Specifies if the data should be loaded as soon as the page loads
        /// </summary>
        public MvcGridBuilder<T1> WithQueryOnPageLoad(bool queryOnPageLoad)
        {
            GridDefinition.QueryOnPageLoad = queryOnPageLoad;
            return this;
        }

        /// <summary>
        ///     A prefix to add to all query string parameters for this grid, for when there are more than 1 grids on the same page
        /// </summary>
        public MvcGridBuilder<T1> WithQueryStringPrefix(string prefix)
        {
            GridDefinition.QueryStringPrefix = prefix;
            return this;
        }

        /// <summary>
        ///     The rendering mode to use for this grid. By default it will use the RenderingEngine rendering mode. If you want to
        ///     use a custom Razor view to display your grid, change this to Controller
        /// </summary>
        public MvcGridBuilder<T1> WithRenderingMode(RenderingMode mode)
        {
            GridDefinition.RenderingMode = mode;
            return this;
        }


        /// <summary>
        ///     This is the method that will actually query the data to populate the grid. Use entity framework, a module from you
        ///     IoC container, direct SQL queries, etc. to get the data. Inside the providee GridContext there is a QueryOptions
        ///     object which will be populated with the currently requested sorting, paging, and filtering options which you must
        ///     take into account. See the QueryOptions documentation below. You must return a QueryResult object which takes an
        ///     enumerable of your type and a count of the total number of records which must be provided if paging is enabled.
        /// </summary>
        public MvcGridBuilder<T1> WithRetrieveDataMethod(
            Func<GridContext, QueryResult<T1>> retrieveData)
        {
            GridDefinition.RetrieveData = retrieveData;
            return this;
        }

        /// <summary>
        ///     Use this to specify a custom css class based on data for the current row
        /// </summary>
        public MvcGridBuilder<T1> WithRowCssClassExpression(
            Func<T1, GridContext, string> rowCssClassExpression)
        {
            GridDefinition.RowCssClassExpression = rowCssClassExpression;
            return this;
        }

        /// <summary>
        ///     Use this to specify a custom css class based on data for the current row
        /// </summary>
        public MvcGridBuilder<T1> WithRowCssClassExpression(
            Func<T1, string> rowCssClassExpression)
        {
            GridDefinition.RowCssClassExpression =
                (t1, gridContext) => rowCssClassExpression(t1);
            return this;
        }

        /// <summary>
        ///     Enables sorting on the grid. Note, sorting must also be enabled on each column where sorting is wanted
        /// </summary>
        public MvcGridBuilder<T1> WithSorting(bool sorting)
        {
            GridDefinition.Sorting = sorting;
            return this;
        }

        /// <summary>
        ///     Enables sorting on the grid. Note, sorting must also be enabled on each column where sorting is wanted
        /// </summary>
        public MvcGridBuilder<T1> WithSorting(
            bool sorting,
            string defaultSortColumn)
        {
            GridDefinition.Sorting = sorting;
            GridDefinition.DefaultSortColumn = defaultSortColumn;
            return this;
        }

        /// <summary>
        ///     Enables sorting on the grid. Note, sorting must also be enabled on each column where sorting is wanted
        /// </summary>
        public MvcGridBuilder<T1> WithSorting(
            bool sorting,
            string defaultSortColumn,
            SortDirection defaultSortDirection)
        {
            GridDefinition.Sorting = sorting;
            GridDefinition.DefaultSortColumn = defaultSortColumn;
            GridDefinition.DefaultSortDirection = defaultSortDirection;
            return this;
        }

        /// <summary>
        ///     Summary text to display in grid footer. Defaults to "Showing {0} to {1} of {2} entries"
        ///     {0} = first record number shown on page
        ///     {1} = last record number shown on page
        ///     {2} = total number of records on all pages
        /// </summary>
        /// <param name="summaryMessage"></param>
        /// <returns></returns>
        public MvcGridBuilder<T1> WithSummaryMessage(string summaryMessage)
        {
            GridDefinition.SummaryMessage = summaryMessage;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templatingEngine"></param>
        /// <returns></returns>
        public MvcGridBuilder<T1> WithTemplatingEngine(Type templatingEngine)
        {
            GridDefinition.TemplatingEngine = templatingEngine;
            return this;
        }

        /// <summary>
        ///     When RenderingMode is set to Controller, this is the path to the razor view to use.
        /// </summary>
        public MvcGridBuilder<T1> WithViewPath(string viewPath)
        {
            GridDefinition.ViewPath = viewPath;
            return this;
        }
    }
}