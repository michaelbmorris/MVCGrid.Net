using MichaelBrandonMorris.MvcGrid.Interfaces;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    ///     Class ColumnDefaults.
    /// </summary>
    /// <seealso
    ///     cref="MichaelBrandonMorris.MvcGrid.Interfaces.IMvcGridColumn" />
    /// TODO Edit XML Comment Template for ColumnDefaults
    public class ColumnDefaults : IMvcGridColumn
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ColumnDefaults" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public ColumnDefaults()
        {
            ColumnName = null;
            HeaderText = null;
            EnableSorting = false;
            HtmlEncode = true;
            EnableFiltering = false;
            Visible = true;
            SortColumnData = null;
            AllowChangeVisibility = false;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether [allow change
        ///     visibility].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [allow change visibility]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for AllowChangeVisibility
        public bool AllowChangeVisibility
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the name of the column.
        /// </summary>
        /// <value>The name of the column.</value>
        /// TODO Edit XML Comment Template for ColumnName
        public string ColumnName
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets a value indicating whether [enable filtering].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [enable filtering]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for EnableFiltering
        public bool EnableFiltering
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets a value indicating whether [enable sorting].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [enable sorting]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for EnableSorting
        public bool EnableSorting
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the header text.
        /// </summary>
        /// <value>The header text.</value>
        /// TODO Edit XML Comment Template for HeaderText
        public string HeaderText
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets a value indicating whether [HTML encode].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [HTML encode]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for HtmlEncode
        public bool HtmlEncode
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the sort column data.
        /// </summary>
        /// <value>The sort column data.</value>
        /// TODO Edit XML Comment Template for SortColumnData
        public object SortColumnData
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets a value indicating whether this
        ///     <see cref="IMvcGridColumn" /> is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        /// TODO Edit XML Comment Template for Visible
        public bool Visible
        {
            get;
            set;
        }
    }
}