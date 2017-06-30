using System;
using System.Collections.Generic;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryOptions
    {
        private object _sortColumnData;

        /// <summary>
        /// 
        /// </summary>
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
        /// 
        /// </summary>
        public Dictionary<string, string> AdditionalQueryOptions
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<ColumnVisibility> ColumnVisibility
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Filters
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int? ItemsPerPage
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int? PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> PageParameters
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string RenderingEngineName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public object SortColumnData
        {
            get => _sortColumnData ?? SortColumnName;
            set => _sortColumnData = value;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SortColumnName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public SortDirection SortDirection
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <returns></returns>
        public int? GetLimitRowcount()
        {
            return ItemsPerPage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSortColumnData<T>()
        {
            return (T) SortColumnData;
        }
    }
}