using System;
using System.Collections.Generic;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    ///     Class QueryOptions.
    /// </summary>
    /// TODO Edit XML Comment Template for QueryOptions
    public class QueryOptions
    {
        /// <summary>
        ///     The sort column data
        /// </summary>
        /// TODO Edit XML Comment Template for _sortColumnData
        private object _sortColumnData;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="QueryOptions" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public QueryOptions()
        {
            Filters =
                new Dictionary<string, string>(
                    StringComparer.OrdinalIgnoreCase);
            AdditionalQueryOptions =
                new Dictionary<string, string>(
                    StringComparer.OrdinalIgnoreCase);
            PageParameters =
                new Dictionary<string, string>(
                    StringComparer.OrdinalIgnoreCase);
            ColumnVisibility = new List<ColumnVisibility>();
        }

        /// <summary>
        ///     Gets or sets the additional query options.
        /// </summary>
        /// <value>The additional query options.</value>
        /// TODO Edit XML Comment Template for AdditionalQueryOptions
        public Dictionary<string, string> AdditionalQueryOptions
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the column visibility.
        /// </summary>
        /// <value>The column visibility.</value>
        /// TODO Edit XML Comment Template for ColumnVisibility
        public List<ColumnVisibility> ColumnVisibility
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the filters.
        /// </summary>
        /// <value>The filters.</value>
        /// TODO Edit XML Comment Template for Filters
        public Dictionary<string, string> Filters
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the items per page.
        /// </summary>
        /// <value>The items per page.</value>
        /// TODO Edit XML Comment Template for ItemsPerPage
        public int? ItemsPerPage
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        /// TODO Edit XML Comment Template for PageIndex
        public int? PageIndex
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the page parameters.
        /// </summary>
        /// <value>The page parameters.</value>
        /// TODO Edit XML Comment Template for PageParameters
        public Dictionary<string, string> PageParameters
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the name of the rendering engine.
        /// </summary>
        /// <value>The name of the rendering engine.</value>
        /// TODO Edit XML Comment Template for RenderingEngineName
        public string RenderingEngineName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the sort column data.
        /// </summary>
        /// <value>The sort column data.</value>
        /// TODO Edit XML Comment Template for SortColumnData
        public object SortColumnData
        {
            get => _sortColumnData ?? SortColumnName;
            set => _sortColumnData = value;
        }

        /// <summary>
        ///     Gets or sets the name of the sort column.
        /// </summary>
        /// <value>The name of the sort column.</value>
        /// TODO Edit XML Comment Template for SortColumnName
        public string SortColumnName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the sort direction.
        /// </summary>
        /// <value>The sort direction.</value>
        /// TODO Edit XML Comment Template for SortDirection
        public SortDirection SortDirection
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the additional query option string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GetAdditionalQueryOptionString
        public string GetAdditionalQueryOptionString(string name)
        {
            if (!AdditionalQueryOptions.ContainsKey(name))
            {
                return null;
            }
            if (AdditionalQueryOptions[name] == null)
            {
                return null;
            }

            var val = AdditionalQueryOptions[name].Trim();

            return string.IsNullOrWhiteSpace(val) ? null : val;
        }

        /// <summary>
        ///     Gets the filter string.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GetFilterString
        public string GetFilterString(string columnName)
        {
            if (!Filters.ContainsKey(columnName))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(Filters[columnName]))
            {
                return null;
            }

            var val = Filters[columnName].Trim();

            return string.IsNullOrWhiteSpace(val) ? null : Filters[columnName];
        }

        /// <summary>
        ///     Gets the limit offset.
        /// </summary>
        /// <returns>System.Nullable&lt;System.Int32&gt;.</returns>
        /// TODO Edit XML Comment Template for GetLimitOffset
        public int? GetLimitOffset()
        {
            if (!ItemsPerPage.HasValue)
            {
                return null;
            }

            if (!PageIndex.HasValue)
            {
                PageIndex = 0;
            }

            return PageIndex * ItemsPerPage;
        }

        /// <summary>
        ///     Gets the limit rowcount.
        /// </summary>
        /// <returns>System.Nullable&lt;System.Int32&gt;.</returns>
        /// TODO Edit XML Comment Template for GetLimitRowcount
        public int? GetLimitRowcount()
        {
            return ItemsPerPage;
        }

        /// <summary>
        ///     Gets the page parameter string.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GetPageParameterString
        public string GetPageParameterString(string name)
        {
            if (!PageParameters.ContainsKey(name))
            {
                return null;
            }
            if (PageParameters[name] == null)
            {
                return null;
            }

            var val = PageParameters[name].Trim();

            return string.IsNullOrWhiteSpace(val) ? null : val;
        }

        /// <summary>
        ///     Gets the sort column data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T.</returns>
        /// TODO Edit XML Comment Template for GetSortColumnData`1
        public T GetSortColumnData<T>()
        {
            return (T) SortColumnData;
        }
    }
}