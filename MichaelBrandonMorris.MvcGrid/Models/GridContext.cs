using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.MvcGrid.Interfaces;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    ///     Class GridContext.
    /// </summary>
    /// TODO Edit XML Comment Template for GridContext
    public class GridContext
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GridContext" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public GridContext()
        {
            Items = new Dictionary<string, object>();
        }

        /// <summary>
        ///     Gets or sets the current HTTP context.
        /// </summary>
        /// <value>The current HTTP context.</value>
        /// TODO Edit XML Comment Template for CurrentHttpContext
        public HttpContext CurrentHttpContext
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the name of the grid.
        /// </summary>
        /// <value>The name of the grid.</value>
        /// TODO Edit XML Comment Template for GridName
        public string GridName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        /// TODO Edit XML Comment Template for Items
        public Dictionary<string, object> Items
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the query options.
        /// </summary>
        /// <value>The query options.</value>
        /// TODO Edit XML Comment Template for QueryOptions
        public QueryOptions QueryOptions
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the URL helper.
        /// </summary>
        /// <value>The URL helper.</value>
        /// TODO Edit XML Comment Template for UrlHelper
        public UrlHelper UrlHelper
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the grid definition.
        /// </summary>
        /// <value>The grid definition.</value>
        /// TODO Edit XML Comment Template for GridDefinition
        internal IMvcGridDefinition GridDefinition
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the visible columns.
        /// </summary>
        /// <returns>IEnumerable&lt;IMvcGridColumn&gt;.</returns>
        /// TODO Edit XML Comment Template for GetVisibleColumns
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