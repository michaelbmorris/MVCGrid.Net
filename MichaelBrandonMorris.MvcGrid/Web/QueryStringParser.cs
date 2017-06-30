using System.Collections.Generic;
using System.Linq;
using System.Web;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.MvcGrid.Interfaces;
using MichaelBrandonMorris.MvcGrid.Models;

namespace MichaelBrandonMorris.MvcGrid.Web
{
    /// <summary>
    ///     Class QueryStringParser.
    /// </summary>
    /// TODO Edit XML Comment Template for QueryStringParser
    internal class QueryStringParser
    {
        /// <summary>
        ///     The query string prefix page parameter
        /// </summary>
        /// TODO Edit XML Comment Template for QueryStringPrefixPageParameter
        public const string QueryStringPrefixPageParameter = "_pp_";

        /// <summary>
        ///     The query string suffix columns
        /// </summary>
        /// TODO Edit XML Comment Template for QueryStringSuffixColumns
        public const string QueryStringSuffixColumns = "cols";

        /// <summary>
        ///     The query string suffix engine
        /// </summary>
        /// TODO Edit XML Comment Template for QueryStringSuffixEngine
        public const string QueryStringSuffixEngine = "engine";

        /// <summary>
        ///     The query string suffix items per page
        /// </summary>
        /// TODO Edit XML Comment Template for QueryStringSuffixItemsPerPage
        public const string QueryStringSuffixItemsPerPage = "pagesize";

        /// <summary>
        ///     The query string suffix page
        /// </summary>
        /// TODO Edit XML Comment Template for QueryStringSuffixPage
        public const string QueryStringSuffixPage = "page";

        /// <summary>
        ///     The query string suffix sort
        /// </summary>
        /// TODO Edit XML Comment Template for QueryStringSuffixSort
        public const string QueryStringSuffixSort = "sort";

        /// <summary>
        ///     The query string suffix sort dir
        /// </summary>
        /// TODO Edit XML Comment Template for QueryStringSuffixSortDir
        public const string QueryStringSuffixSortDir = "dir";

        /// <summary>
        ///     Parses the options.
        /// </summary>
        /// <param name="grid">The grid.</param>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <returns>QueryOptions.</returns>
        /// TODO Edit XML Comment Template for ParseOptions
        public static QueryOptions ParseOptions(
            IMvcGridDefinition grid,
            HttpRequest httpRequest)
        {
            var qsKeyPage = grid.QueryStringPrefix + QueryStringSuffixPage;
            var qsKeySort = grid.QueryStringPrefix + QueryStringSuffixSort;

            var qsKeyDirection = grid.QueryStringPrefix
                                 + QueryStringSuffixSortDir;

            var qsKeyEngine = grid.QueryStringPrefix + QueryStringSuffixEngine;

            var qsKeyPageSize = grid.QueryStringPrefix
                                + QueryStringSuffixItemsPerPage;

            var qsColumns = grid.QueryStringPrefix + QueryStringSuffixColumns;

            var options = new QueryOptions();

            if (httpRequest.QueryString[qsKeyEngine] != null)
            {
                var re = httpRequest.QueryString[qsKeyEngine];
                options.RenderingEngineName = re;
            }

            if (!grid.Paging)
            {
                options.ItemsPerPage = null;
                options.PageIndex = null;
            }
            else
            {
                options.ItemsPerPage = grid.ItemsPerPage;

                if (grid.AllowChangingPageSize)
                {
                    if (httpRequest.QueryString[qsKeyPageSize] != null)
                    {
                        if (int.TryParse(
                            httpRequest.QueryString[qsKeyPageSize],
                            out int pageSize))
                        {
                            options.ItemsPerPage = pageSize;
                        }
                    }

                    if (grid.MaxItemsPerPage.HasValue
                        && grid.MaxItemsPerPage.Value < options.ItemsPerPage)
                    {
                        options.ItemsPerPage = grid.MaxItemsPerPage.Value;
                    }
                }

                if (options.ItemsPerPage <= 0)
                {
                    options.ItemsPerPage = 20;
                }

                options.PageIndex = 0;
                if (httpRequest.QueryString[qsKeyPage] != null)
                {
                    if (int.TryParse(
                        httpRequest.QueryString[qsKeyPage],
                        out int pageNum))
                    {
                        options.PageIndex = pageNum - 1;
                        if (options.PageIndex < 0)
                        {
                            options.PageIndex = 0;
                        }
                    }
                }
            }

            if (grid.Filtering)
            {
                var filterableColumns =
                    grid.GetColumns().Where(p => p.EnableFiltering);

                foreach (var col in filterableColumns)
                {
                    var qsKey = grid.QueryStringPrefix + col.ColumnName;

                    if (httpRequest.QueryString[qsKey] == null)
                    {
                        continue;
                    }

                    var filterValue = httpRequest.QueryString[qsKey];

                    if (!string.IsNullOrWhiteSpace(filterValue))
                    {
                        options.Filters.Add(col.ColumnName, filterValue);
                    }
                }
            }

            if (!grid.Sorting)
            {
                options.SortColumnName = null;
                options.SortColumnData = null;
                options.SortDirection = SortDirection.Unspecified;
            }
            else
            {
                options.SortColumnName = null;

                string sortColName = null;
                if (httpRequest.QueryString[qsKeySort] != null)
                {
                    sortColName = httpRequest.QueryString[qsKeySort];
                }

                if (string.IsNullOrWhiteSpace(sortColName))
                {
                    sortColName = grid.DefaultSortColumn;
                }

                var thisSortColName = sortColName.Trim().ToLower();

                var colDef = grid.GetColumns()
                    .SingleOrDefault(
                        p => p.ColumnName.ToLower() == thisSortColName);

                if (colDef != null
                    && !colDef.EnableSorting)
                {
                    colDef = null;
                }

                if (colDef != null)
                {
                    options.SortColumnName = colDef.ColumnName;
                    options.SortColumnData = colDef.SortColumnData;
                }

                options.SortDirection = grid.DefaultSortDirection;

                if (httpRequest.QueryString[qsKeyDirection] != null)
                {
                    var sortDir = httpRequest.QueryString[qsKeyDirection];

                    if (sortDir.EqualsOrdinalIgnoreCase("dsc"))
                    {
                        options.SortDirection = SortDirection.Dsc;
                    }
                    else if (sortDir.EqualsOrdinalIgnoreCase("asc"))
                    {
                        options.SortDirection = SortDirection.Asc;
                    }
                }
            }

            if (grid.AdditionalQueryOptionNames.Count > 0)
            {
                foreach (var aqon in grid.AdditionalQueryOptionNames)
                {
                    var qsKeyAqo = grid.QueryStringPrefix + aqon;
                    var val = "";

                    if (httpRequest.QueryString[qsKeyAqo] != null)
                    {
                        val = httpRequest.QueryString[qsKeyAqo];
                    }

                    options.AdditionalQueryOptions.Add(aqon, val);
                }
            }

            if (grid.PageParameterNames.Count > 0)
            {
                foreach (var aqon in grid.PageParameterNames)
                {
                    var qsKeyAqo = QueryStringPrefixPageParameter
                                   + grid.QueryStringPrefix
                                   + aqon;
                    var val = "";

                    if (httpRequest.QueryString[qsKeyAqo] != null)
                    {
                        val = httpRequest.QueryString[qsKeyAqo];
                    }

                    options.PageParameters.Add(aqon, val);
                }
            }


            var gridColumns = grid.GetColumns().ToList();
            var requestedColumns = new List<ColumnVisibility>();

            if (httpRequest.QueryString[qsColumns] == null)
            {
                requestedColumns.AddRange(
                    gridColumns.Select(
                        gridColumn => new ColumnVisibility
                        {
                            ColumnName = gridColumn.ColumnName,
                            Visible = gridColumn.Visible
                        }));
            }
            else
            {
                var cols = httpRequest.QueryString[qsColumns];
                var colParts = cols.Split(',', ';');

                foreach (var colPart in colParts)
                {
                    if (string.IsNullOrWhiteSpace(colPart))
                    {
                        continue;
                    }

                    var thisColPart = colPart.ToLower().Trim();

                    var gridColumn = gridColumns.SingleOrDefault(
                        p => p.ColumnName.ToLower() == thisColPart);

                    if (gridColumn == null)
                    {
                        continue;
                    }

                    {
                        if (requestedColumns.SingleOrDefault(
                                p => p.ColumnName == gridColumn.ColumnName)
                            == null)
                        {
                            requestedColumns.Add(
                                new ColumnVisibility
                                {
                                    ColumnName = gridColumn.ColumnName,
                                    Visible = true
                                });
                        }
                    }
                }
            }

            foreach (var gridColumn in gridColumns)
            {
                var requestedCol =
                    requestedColumns.SingleOrDefault(
                        p => p.ColumnName == gridColumn.ColumnName);

                if (requestedCol == null)
                {
                    requestedCol = new ColumnVisibility
                    {
                        ColumnName = gridColumn.ColumnName,
                        Visible = false
                    };

                    requestedColumns.Add(requestedCol);
                }

                if (!requestedCol.Visible
                    && gridColumn.Visible
                    && !gridColumn.AllowChangeVisibility)
                {
                    requestedCol.Visible = true;
                }
            }

            options.ColumnVisibility.AddRange(requestedColumns);
            return options;
        }
    }
}