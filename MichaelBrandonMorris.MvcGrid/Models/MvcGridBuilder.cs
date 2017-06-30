using System;
using System.Configuration;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    ///     Class MvcGridBuilder.
    /// </summary>
    /// <typeparam name="T1">The type of the t1.</typeparam>
    /// TODO Edit XML Comment Template for MvcGridBuilder`1
    public class MvcGridBuilder<T1>
    {
        /// <summary>
        ///     The column defaults
        /// </summary>
        /// TODO Edit XML Comment Template for _columnDefaults
        private readonly ColumnDefaults _columnDefaults;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="MvcGridBuilder{T1}" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public MvcGridBuilder()
        {
            GridDefinition = new GridDefinition<T1>();
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="MvcGridBuilder{T1}" /> class.
        /// </summary>
        /// <param name="gridDefaults">The grid defaults.</param>
        /// TODO Edit XML Comment Template for #ctor
        public MvcGridBuilder(GridDefaults gridDefaults)
            : this(gridDefaults, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="MvcGridBuilder{T1}" /> class.
        /// </summary>
        /// <param name="columnDefaults">The column defaults.</param>
        /// TODO Edit XML Comment Template for #ctor
        public MvcGridBuilder(ColumnDefaults columnDefaults)
            : this(null, columnDefaults)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="MvcGridBuilder{T1}" /> class.
        /// </summary>
        /// <param name="gridDefaults">The grid defaults.</param>
        /// <param name="columnDefaults">The column defaults.</param>
        /// TODO Edit XML Comment Template for #ctor
        public MvcGridBuilder(
            GridDefaults gridDefaults,
            ColumnDefaults columnDefaults)
        {
            GridDefinition = new GridDefinition<T1>(gridDefaults);

            _columnDefaults = columnDefaults;
        }

        /// <summary>
        ///     Gets or sets the grid definition.
        /// </summary>
        /// <value>The grid definition.</value>
        /// TODO Edit XML Comment Template for GridDefinition
        public GridDefinition<T1> GridDefinition
        {
            get;
            set;
        }

        /// <summary>
        ///     Adds the column.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="headerText">The header text.</param>
        /// <param name="valueExpression">The value expression.</param>
        /// <param name="enableSort">
        ///     if set to <c>true</c> [enable
        ///     sort].
        /// </param>
        /// <param name="htmlEncode">
        ///     if set to <c>true</c> [HTML
        ///     encode].
        /// </param>
        /// <param name="plainTextValueExpression">
        ///     The plain text value
        ///     expression.
        /// </param>
        /// <param name="cellCssClassExpression">
        ///     The cell CSS class
        ///     expression.
        /// </param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for AddColumn
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
        ///     Adds the column.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for AddColumn
        public MvcGridBuilder<T1> AddColumn(GridColumn<T1> column)
        {
            GridDefinition.AddColumn(column);
            return this;
        }

        /// <summary>
        ///     Adds the columns.
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for AddColumns
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
        ///     Adds the rendering engine.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="renderingEngineType">
        ///     Type of the rendering
        ///     engine.
        /// </param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for AddRenderingEngine
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
        ///     Adds the rendering engine.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for AddRenderingEngine
        public MvcGridBuilder<T1> AddRenderingEngine(string name, string type)
        {
            GridDefinition.RenderingEngines.Add(
                new ProviderSettings(name, type));
            return this;
        }

        /// <summary>
        ///     Removes the rendering engine.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for RemoveRenderingEngine
        public MvcGridBuilder<T1> RemoveRenderingEngine(string name)
        {
            GridDefinition.RenderingEngines.Remove(name);
            return this;
        }

        /// <summary>
        ///     Withes the name of the additional query option.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithAdditionalQueryOptionName
        public MvcGridBuilder<T1> WithAdditionalQueryOptionName(string name)
        {
            GridDefinition.AdditionalQueryOptionNames.Add(name);
            return this;
        }

        /// <summary>
        ///     Withes the additional query option names.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithAdditionalQueryOptionNames
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
        ///     Withes the additional setting.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithAdditionalSetting
        public MvcGridBuilder<T1> WithAdditionalSetting(
            string name,
            object value)
        {
            GridDefinition.AdditionalSettings[name] = value;
            return this;
        }

        /// <summary>
        ///     Withes the size of the allow changing page.
        /// </summary>
        /// <param name="allow">if set to <c>true</c> [allow].</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithAllowChangingPageSize
        public MvcGridBuilder<T1> WithAllowChangingPageSize(bool allow)
        {
            GridDefinition.AllowChangingPageSize = allow;
            return this;
        }

        /// <summary>
        ///     Withes the type of the authorization.
        /// </summary>
        /// <param name="authType">Type of the authentication.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithAuthorizationType
        public MvcGridBuilder<T1> WithAuthorizationType(
            AuthorizationType authType)
        {
            GridDefinition.AuthorizationType = authType;
            return this;
        }

        /// <summary>
        ///     Withes the name of the client side loading complete
        ///     function.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithClientSideLoadingCompleteFunctionName
        public MvcGridBuilder<T1> WithClientSideLoadingCompleteFunctionName(
            string name)
        {
            GridDefinition.ClientSideLoadingCompleteFunctionName = name;
            return this;
        }

        /// <summary>
        ///     Withes the name of the client side loading message
        ///     function.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithClientSideLoadingMessageFunctionName
        public MvcGridBuilder<T1> WithClientSideLoadingMessageFunctionName(
            string name)
        {
            GridDefinition.ClientSideLoadingMessageFunctionName = name;
            return this;
        }

        /// <summary>
        ///     Withes the container view path.
        /// </summary>
        /// <param name="containerViewPath">The container view path.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithContainerViewPath
        public MvcGridBuilder<T1> WithContainerViewPath(
            string containerViewPath)
        {
            GridDefinition.ContainerViewPath = containerViewPath;
            return this;
        }

        /// <summary>
        ///     Withes the default name of the rendering engine.
        /// </summary>
        /// <param name="renderingEngineName">
        ///     Name of the rendering
        ///     engine.
        /// </param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithDefaultRenderingEngineName
        public MvcGridBuilder<T1> WithDefaultRenderingEngineName(
            string renderingEngineName)
        {
            GridDefinition.DefaultRenderingEngineName = renderingEngineName;
            return this;
        }

        /// <summary>
        ///     Withes the default sort column.
        /// </summary>
        /// <param name="defaultSortColumn">The default sort column.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithDefaultSortColumn
        public MvcGridBuilder<T1> WithDefaultSortColumn(
            string defaultSortColumn)
        {
            GridDefinition.DefaultSortColumn = defaultSortColumn;
            return this;
        }

        /// <summary>
        ///     Withes the default sort direction.
        /// </summary>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithDefaultSortDirection
        public MvcGridBuilder<T1> WithDefaultSortDirection(
            SortDirection sortDirection)
        {
            GridDefinition.DefaultSortDirection = sortDirection;
            return this;
        }

        /// <summary>
        ///     Withes the error message HTML.
        /// </summary>
        /// <param name="errorMessageHtml">The error message HTML.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithErrorMessageHtml
        public MvcGridBuilder<T1> WithErrorMessageHtml(string errorMessageHtml)
        {
            GridDefinition.ErrorMessageHtml = errorMessageHtml;
            return this;
        }

        /// <summary>
        ///     Withes the filtering.
        /// </summary>
        /// <param name="filtering">if set to <c>true</c> [filtering].</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithFiltering
        public MvcGridBuilder<T1> WithFiltering(bool filtering)
        {
            GridDefinition.Filtering = filtering;
            return this;
        }


        /// <summary>
        ///     Withes the items per page.
        /// </summary>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithItemsPerPage
        public MvcGridBuilder<T1> WithItemsPerPage(int itemsPerPage)
        {
            GridDefinition.ItemsPerPage = itemsPerPage;
            return this;
        }

        /// <summary>
        ///     Withes the maximum items per page.
        /// </summary>
        /// <param name="maxItems">The maximum items.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithMaxItemsPerPage
        public MvcGridBuilder<T1> WithMaxItemsPerPage(int maxItems)
        {
            GridDefinition.MaxItemsPerPage = maxItems;
            return this;
        }


        /// <summary>
        ///     Withes the next button caption.
        /// </summary>
        /// <param name="nextButtonCaption">The next button caption.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithNextButtonCaption
        public MvcGridBuilder<T1> WithNextButtonCaption(
            string nextButtonCaption)
        {
            GridDefinition.NextButtonCaption = nextButtonCaption;
            return this;
        }

        /// <summary>
        ///     Withes the no results message.
        /// </summary>
        /// <param name="noResultsMessage">The no results message.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithNoResultsMessage
        public MvcGridBuilder<T1> WithNoResultsMessage(string noResultsMessage)
        {
            GridDefinition.NoResultsMessage = noResultsMessage;
            return this;
        }

        /// <summary>
        ///     Withouts the additional setting.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithoutAdditionalSetting
        public MvcGridBuilder<T1> WithoutAdditionalSetting(string name)
        {
            if (GridDefinition.AdditionalSettings.ContainsKey(name))
            {
                GridDefinition.AdditionalSettings.Remove(name);
            }
            return this;
        }

        /// <summary>
        ///     Withes the page parameter names.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithPageParameterNames
        public MvcGridBuilder<T1> WithPageParameterNames(params string[] names)
        {
            foreach (var name in names)
            {
                GridDefinition.PageParameterNames.Add(name);
            }

            return this;
        }

        /// <summary>
        ///     Withes the paging.
        /// </summary>
        /// <param name="paging">if set to <c>true</c> [paging].</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithPaging
        public MvcGridBuilder<T1> WithPaging(bool paging)
        {
            GridDefinition.Paging = paging;
            return this;
        }

        /// <summary>
        ///     Withes the paging.
        /// </summary>
        /// <param name="paging">if set to <c>true</c> [paging].</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithPaging
        public MvcGridBuilder<T1> WithPaging(bool paging, int itemsPerPage)
        {
            GridDefinition.Paging = paging;
            GridDefinition.ItemsPerPage = itemsPerPage;
            return this;
        }

        /// <summary>
        ///     Withes the paging.
        /// </summary>
        /// <param name="paging">if set to <c>true</c> [paging].</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <param name="allowChangePageSize">
        ///     if set to <c>true</c>
        ///     [allow change page size].
        /// </param>
        /// <param name="maxItemsPerPage">The maximum items per page.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithPaging
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
        ///     Withes the preload data.
        /// </summary>
        /// <param name="preload">if set to <c>true</c> [preload].</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithPreloadData
        public MvcGridBuilder<T1> WithPreloadData(bool preload)
        {
            GridDefinition.PreloadData = preload;
            return this;
        }

        /// <summary>
        ///     Withes the previous button caption.
        /// </summary>
        /// <param name="previousButtonCaption">
        ///     The previous button
        ///     caption.
        /// </param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithPreviousButtonCaption
        public MvcGridBuilder<T1> WithPreviousButtonCaption(
            string previousButtonCaption)
        {
            GridDefinition.PreviousButtonCaption = previousButtonCaption;
            return this;
        }

        /// <summary>
        ///     Withes the processing message.
        /// </summary>
        /// <param name="processingMessage">The processing message.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithProcessingMessage
        public MvcGridBuilder<T1> WithProcessingMessage(
            string processingMessage)
        {
            GridDefinition.ProcessingMessage = processingMessage;
            return this;
        }

        /// <summary>
        ///     Withes the query on page load.
        /// </summary>
        /// <param name="queryOnPageLoad">
        ///     if set to <c>true</c> [query
        ///     on page load].
        /// </param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithQueryOnPageLoad
        public MvcGridBuilder<T1> WithQueryOnPageLoad(bool queryOnPageLoad)
        {
            GridDefinition.QueryOnPageLoad = queryOnPageLoad;
            return this;
        }

        /// <summary>
        ///     Withes the query string prefix.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithQueryStringPrefix
        public MvcGridBuilder<T1> WithQueryStringPrefix(string prefix)
        {
            GridDefinition.QueryStringPrefix = prefix;
            return this;
        }

        /// <summary>
        ///     Withes the rendering mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithRenderingMode
        public MvcGridBuilder<T1> WithRenderingMode(RenderingMode mode)
        {
            GridDefinition.RenderingMode = mode;
            return this;
        }


        /// <summary>
        ///     Withes the retrieve data method.
        /// </summary>
        /// <param name="retrieveData">The retrieve data.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithRetrieveDataMethod
        public MvcGridBuilder<T1> WithRetrieveDataMethod(
            Func<GridContext, QueryResult<T1>> retrieveData)
        {
            GridDefinition.RetrieveData = retrieveData;
            return this;
        }

        /// <summary>
        ///     Withes the row CSS class expression.
        /// </summary>
        /// <param name="rowCssClassExpression">
        ///     The row CSS class
        ///     expression.
        /// </param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithRowCssClassExpression
        public MvcGridBuilder<T1> WithRowCssClassExpression(
            Func<T1, GridContext, string> rowCssClassExpression)
        {
            GridDefinition.RowCssClassExpression = rowCssClassExpression;
            return this;
        }

        /// <summary>
        ///     Withes the row CSS class expression.
        /// </summary>
        /// <param name="rowCssClassExpression">
        ///     The row CSS class
        ///     expression.
        /// </param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithRowCssClassExpression
        public MvcGridBuilder<T1> WithRowCssClassExpression(
            Func<T1, string> rowCssClassExpression)
        {
            GridDefinition.RowCssClassExpression =
                (t1, gridContext) => rowCssClassExpression(t1);
            return this;
        }

        /// <summary>
        ///     Withes the sorting.
        /// </summary>
        /// <param name="sorting">if set to <c>true</c> [sorting].</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithSorting
        public MvcGridBuilder<T1> WithSorting(bool sorting)
        {
            GridDefinition.Sorting = sorting;
            return this;
        }

        /// <summary>
        ///     Withes the sorting.
        /// </summary>
        /// <param name="sorting">if set to <c>true</c> [sorting].</param>
        /// <param name="defaultSortColumn">The default sort column.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithSorting
        public MvcGridBuilder<T1> WithSorting(
            bool sorting,
            string defaultSortColumn)
        {
            GridDefinition.Sorting = sorting;
            GridDefinition.DefaultSortColumn = defaultSortColumn;
            return this;
        }

        /// <summary>
        ///     Withes the sorting.
        /// </summary>
        /// <param name="sorting">if set to <c>true</c> [sorting].</param>
        /// <param name="defaultSortColumn">The default sort column.</param>
        /// <param name="defaultSortDirection">
        ///     The default sort
        ///     direction.
        /// </param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithSorting
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
        ///     Withes the summary message.
        /// </summary>
        /// <param name="summaryMessage">The summary message.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithSummaryMessage
        public MvcGridBuilder<T1> WithSummaryMessage(string summaryMessage)
        {
            GridDefinition.SummaryMessage = summaryMessage;
            return this;
        }

        /// <summary>
        ///     Withes the templating engine.
        /// </summary>
        /// <param name="templatingEngine">The templating engine.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithTemplatingEngine
        public MvcGridBuilder<T1> WithTemplatingEngine(Type templatingEngine)
        {
            GridDefinition.TemplatingEngine = templatingEngine;
            return this;
        }

        /// <summary>
        ///     Withes the view path.
        /// </summary>
        /// <param name="viewPath">The view path.</param>
        /// <returns>MvcGridBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithViewPath
        public MvcGridBuilder<T1> WithViewPath(string viewPath)
        {
            GridDefinition.ViewPath = viewPath;
            return this;
        }
    }
}