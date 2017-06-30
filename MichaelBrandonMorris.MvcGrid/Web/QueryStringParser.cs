using System.Collections.Generic;
using System.Linq;
using System.Web;
using MichaelBrandonMorris.MvcGrid.Interfaces;
using MichaelBrandonMorris.MvcGrid.Models;

namespace MichaelBrandonMorris.MvcGrid.Web
{
    internal class QueryStringParser
    {
        public const string QueryStringPrefixPageParameter = "_pp_";
        public const string QueryStringSuffixColumns = "cols";
        public const string QueryStringSuffixEngine = "engine";

        public const string QueryStringSuffixItemsPerPage = "pagesize";

        // NOTE: when adding a new suffix, add code to MVCGridDefinitionTable to verify there is no conflict
        public const string QueryStringSuffixPage = "page";

        public const string QueryStringSuffixSort = "sort";
        public const string QueryStringSuffixSortDir = "dir";

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
                        int pageSize;
                        if (int.TryParse(
                            httpRequest.QueryString[qsKeyPageSize],
                            out pageSize))
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
                    int pageNum;
                    if (int.TryParse(
                        httpRequest.QueryString[qsKeyPage],
                        out pageNum))
                    {
                        options.PageIndex = pageNum - 1;
                        if (options.PageIndex < 0)
                        {
                            options.PageIndex = 0;
                        }
                    }
                }
            }

            if (!grid.Filtering)
            {
                //options.Filters
            }
            else
            {
                var filterableColumns =
                    grid.GetColumns().Where(p => p.EnableFiltering);

                foreach (var col in filterableColumns)
                {
                    var qsKey = grid.QueryStringPrefix + col.ColumnName;

                    if (httpRequest.QueryString[qsKey] != null)
                    {
                        var filterValue = httpRequest.QueryString[qsKey];

                        if (!string.IsNullOrWhiteSpace(filterValue))
                        {
                            options.Filters.Add(col.ColumnName, filterValue);
                        }
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

                // validate SortColumn
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
                    if (string.Compare(sortDir, "dsc", true) == 0)
                    {
                        options.SortDirection = SortDirection.Dsc;
                    }
                    else if (string.Compare(sortDir, "asc", true) == 0)
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


            var gridColumns = grid.GetColumns();
            var requestedColumns = new List<ColumnVisibility>();
            if (httpRequest.QueryString[qsColumns] == null)
            {
                foreach (var gridColumn in gridColumns)
                {
                    requestedColumns.Add(
                        new ColumnVisibility
                        {
                            ColumnName = gridColumn.ColumnName,
                            Visible = gridColumn.Visible
                        });
                }
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

                    if (gridColumn != null)
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