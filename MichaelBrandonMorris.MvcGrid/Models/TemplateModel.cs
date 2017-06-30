using System.Web.Mvc;
using MichaelBrandonMorris.MvcGrid.Interfaces;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    ///     Class TemplateModel.
    /// </summary>
    /// TODO Edit XML Comment Template for TemplateModel
    public class TemplateModel
    {
        /// <summary>
        ///     Gets the URL.
        /// </summary>
        /// <value>The URL.</value>
        /// TODO Edit XML Comment Template for Url
        public UrlHelper Url => GridContext.UrlHelper;

        /// <summary>
        ///     Gets or sets the grid column.
        /// </summary>
        /// <value>The grid column.</value>
        /// TODO Edit XML Comment Template for GridColumn
        public IMvcGridColumn GridColumn
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the grid context.
        /// </summary>
        /// <value>The grid context.</value>
        /// TODO Edit XML Comment Template for GridContext
        public GridContext GridContext
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        /// TODO Edit XML Comment Template for Item
        public dynamic Item
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the row.
        /// </summary>
        /// <value>The row.</value>
        /// TODO Edit XML Comment Template for Row
        public Row Row
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        /// TODO Edit XML Comment Template for Value
        public string Value
        {
            get;
            set;
        }
    }
}