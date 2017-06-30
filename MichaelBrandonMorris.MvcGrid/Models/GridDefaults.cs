using System;
using System.Collections.Generic;
using System.Configuration;
using MichaelBrandonMorris.MvcGrid.Interfaces;
using MichaelBrandonMorris.MvcGrid.Templating;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    ///     Class GridDefaults.
    /// </summary>
    /// <seealso
    ///     cref="MichaelBrandonMorris.MvcGrid.Interfaces.IMvcGridDefinition" />
    /// TODO Edit XML Comment Template for GridDefaults
    public class GridDefaults : IMvcGridDefinition
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GridDefaults" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public GridDefaults()
        {
            PreloadData = true;
            QueryOnPageLoad = true;
            Paging = false;
            ItemsPerPage = 20;
            Sorting = false;
            DefaultSortColumn = null;
            DefaultSortDirection = SortDirection.Unspecified;
            NoResultsMessage = "No results.";
            NextButtonCaption = "Next";
            PreviousButtonCaption = "Previous";
            SummaryMessage = "Showing {0} to {1} of {2} entries";
            ProcessingMessage = "Processing";
            ClientSideLoadingMessageFunctionName = null;
            ClientSideLoadingCompleteFunctionName = null;
            Filtering = false;
            TemplatingEngine = typeof(SimpleTemplatingEngine);
            AdditionalSettings = new Dictionary<string, object>();
            RenderingMode = RenderingMode.RenderingEngine;
            ViewPath = "~/Views/MVCGrid/_Grid.cshtml";
            ContainerViewPath = null;
            ErrorMessageHtml =
                @"<div class=""alert alert-warning"" role=""alert"">There was a problem loading the grid.</div>";
            AdditionalQueryOptionNames = new HashSet<string>();
            PageParameterNames = new HashSet<string>();
            AllowChangingPageSize = false;
            MaxItemsPerPage = null;
            AuthorizationType = AuthorizationType.AllowAnonymous;

            RenderingEngines = new ProviderSettingsCollection
            {
                new ProviderSettings(
                    "BootstrapRenderingEngine",
                    "MichaelBrandonMorris.MvcGrid.Rendering.BootstrapRenderingEngine, MichaelBrandonMorris.MvcGrid"),
                new ProviderSettings(
                    "Export",
                    "MichaelBrandonMorris.MvcGrid.Rendering.CsvRenderingEngine, MichaelBrandonMorris.MvcGrid")
            };

            DefaultRenderingEngineName = "BootstrapRenderingEngine";
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
        /// <exception cref="NotImplementedException"></exception>
        /// TODO Edit XML Comment Template for GetAdditionalSetting`1
        public T GetAdditionalSetting<T>(string name, T defaultValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets the columns.
        /// </summary>
        /// <returns>IEnumerable&lt;IMvcGridColumn&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        /// TODO Edit XML Comment Template for GetColumns
        public IEnumerable<IMvcGridColumn> GetColumns()
        {
            throw new NotImplementedException();
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
    }
}