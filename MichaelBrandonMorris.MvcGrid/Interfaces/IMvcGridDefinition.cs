using System;
using System.Collections.Generic;
using System.Configuration;
using MichaelBrandonMorris.MvcGrid.Models;

namespace MichaelBrandonMorris.MvcGrid.Interfaces
{
    /// <summary>
    ///     Interface IMvcGridDefinition
    /// </summary>
    /// TODO Edit XML Comment Template for IMvcGridDefinition
    public interface IMvcGridDefinition
    {
        /// <summary>
        ///     Gets a value indicating whether [preload data].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [preload data]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for PreloadData
        bool PreloadData
        {
            get;
        }

        /// <summary>
        ///     Gets the query string prefix.
        /// </summary>
        /// <value>The query string prefix.</value>
        /// TODO Edit XML Comment Template for QueryStringPrefix
        string QueryStringPrefix
        {
            get;
        }

        /// <summary>
        ///     Gets or sets the additional query option names.
        /// </summary>
        /// <value>The additional query option names.</value>
        /// TODO Edit XML Comment Template for AdditionalQueryOptionNames
        HashSet<string> AdditionalQueryOptionNames
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the additional settings.
        /// </summary>
        /// <value>The additional settings.</value>
        /// TODO Edit XML Comment Template for AdditionalSettings
        Dictionary<string, object> AdditionalSettings
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
        bool AllowChangingPageSize
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the type of the authorization.
        /// </summary>
        /// <value>The type of the authorization.</value>
        /// TODO Edit XML Comment Template for AuthorizationType
        AuthorizationType AuthorizationType
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
        string ClientSideLoadingCompleteFunctionName
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
        string ClientSideLoadingMessageFunctionName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the container view path.
        /// </summary>
        /// <value>The container view path.</value>
        /// TODO Edit XML Comment Template for ContainerViewPath
        string ContainerViewPath
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the default name of the rendering engine.
        /// </summary>
        /// <value>The default name of the rendering engine.</value>
        /// TODO Edit XML Comment Template for DefaultRenderingEngineName
        string DefaultRenderingEngineName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the default sort column.
        /// </summary>
        /// <value>The default sort column.</value>
        /// TODO Edit XML Comment Template for DefaultSortColumn
        string DefaultSortColumn
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the default sort direction.
        /// </summary>
        /// <value>The default sort direction.</value>
        /// TODO Edit XML Comment Template for DefaultSortDirection
        SortDirection DefaultSortDirection
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the error message HTML.
        /// </summary>
        /// <value>The error message HTML.</value>
        /// TODO Edit XML Comment Template for ErrorMessageHtml
        string ErrorMessageHtml
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
        bool Filtering
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the items per page.
        /// </summary>
        /// <value>The items per page.</value>
        /// TODO Edit XML Comment Template for ItemsPerPage
        int ItemsPerPage
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the maximum items per page.
        /// </summary>
        /// <value>The maximum items per page.</value>
        /// TODO Edit XML Comment Template for MaxItemsPerPage
        int? MaxItemsPerPage
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the next button caption.
        /// </summary>
        /// <value>The next button caption.</value>
        /// TODO Edit XML Comment Template for NextButtonCaption
        string NextButtonCaption
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the no results message.
        /// </summary>
        /// <value>The no results message.</value>
        /// TODO Edit XML Comment Template for NoResultsMessage
        string NoResultsMessage
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the page parameter names.
        /// </summary>
        /// <value>The page parameter names.</value>
        /// TODO Edit XML Comment Template for PageParameterNames
        HashSet<string> PageParameterNames
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
        bool Paging
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the previous button caption.
        /// </summary>
        /// <value>The previous button caption.</value>
        /// TODO Edit XML Comment Template for PreviousButtonCaption
        string PreviousButtonCaption
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the processing message.
        /// </summary>
        /// <value>The processing message.</value>
        /// TODO Edit XML Comment Template for ProcessingMessage
        string ProcessingMessage
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
        bool QueryOnPageLoad
        {
            get;
            set;
        }


        /// <summary>
        ///     Gets or sets the rendering engines.
        /// </summary>
        /// <value>The rendering engines.</value>
        /// TODO Edit XML Comment Template for RenderingEngines
        ProviderSettingsCollection RenderingEngines
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the rendering mode.
        /// </summary>
        /// <value>The rendering mode.</value>
        /// TODO Edit XML Comment Template for RenderingMode
        RenderingMode RenderingMode
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
        bool Sorting
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the summary message.
        /// </summary>
        /// <value>The summary message.</value>
        /// TODO Edit XML Comment Template for SummaryMessage
        string SummaryMessage
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the templating engine.
        /// </summary>
        /// <value>The templating engine.</value>
        /// TODO Edit XML Comment Template for TemplatingEngine
        Type TemplatingEngine
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the view path.
        /// </summary>
        /// <value>The view path.</value>
        /// TODO Edit XML Comment Template for ViewPath
        string ViewPath
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
        T GetAdditionalSetting<T>(string name, T defaultValue);

        /// <summary>
        ///     Gets the columns.
        /// </summary>
        /// <returns>IEnumerable&lt;IMvcGridColumn&gt;.</returns>
        /// TODO Edit XML Comment Template for GetColumns
        IEnumerable<IMvcGridColumn> GetColumns();
    }
}