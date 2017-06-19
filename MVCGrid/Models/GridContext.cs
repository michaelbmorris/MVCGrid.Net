using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcGrid.Interfaces;

namespace MvcGrid.Models
{
    /// <summary>
    /// </summary>
    public class GridContext
    {
        /// <summary>
        /// </summary>
        public GridContext()
        {
            Items = new Dictionary<string, object>();
        }

        /// <summary>
        /// </summary>
        public HttpContext CurrentHttpContext
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public string GridName
        {
            get;
            set;
        }

        /// <summary>
        ///     Arbitrary settings for this context
        /// </summary>
        public Dictionary<string, object> Items
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public QueryOptions QueryOptions
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        public UrlHelper UrlHelper
        {
            get;
            set;
        }

        internal IMvcGridDefinition GridDefinition
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMvcGridColumn> GetVisibleColumns()
        {
            var visibleColumns = new List<IMvcGridColumn>();

            var gridColumns = GridDefinition.GetColumns();

            if (QueryOptions.ColumnVisibility == null
                || QueryOptions.ColumnVisibility.Count == 0)
            {
                visibleColumns.AddRange(gridColumns.Where(col => col.Visible));
            }
            else
            {
                visibleColumns.AddRange(
                    from colVis in QueryOptions.ColumnVisibility
                    let gridColumn =
                    gridColumns.SingleOrDefault(
                        p => p.ColumnName == colVis.ColumnName)
                    where colVis.Visible
                    select gridColumn);
            }

            if (visibleColumns.Count == 0)
            {
                visibleColumns.Add(GridDefinition.GetColumns().ElementAt(0));
            }

            return visibleColumns;
        }
    }
}