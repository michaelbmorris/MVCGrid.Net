namespace MichaelBrandonMorris.MvcGrid.Interfaces
{
    /// <summary>
    ///     Interface IMvcGridColumn
    /// </summary>
    /// TODO Edit XML Comment Template for IMvcGridColumn
    public interface IMvcGridColumn
    {
        /// <summary>
        ///     Gets the name of the column.
        /// </summary>
        /// <value>The name of the column.</value>
        /// TODO Edit XML Comment Template for ColumnName
        string ColumnName
        {
            get;
        }

        /// <summary>
        ///     Gets a value indicating whether [enable filtering].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [enable filtering]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for EnableFiltering
        bool EnableFiltering
        {
            get;
        }

        /// <summary>
        ///     Gets a value indicating whether [enable sorting].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [enable sorting]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for EnableSorting
        bool EnableSorting
        {
            get;
        }

        /// <summary>
        ///     Gets the header text.
        /// </summary>
        /// <value>The header text.</value>
        /// TODO Edit XML Comment Template for HeaderText
        string HeaderText
        {
            get;
        }

        /// <summary>
        ///     Gets a value indicating whether [HTML encode].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [HTML encode]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for HtmlEncode
        bool HtmlEncode
        {
            get;
        }

        /// <summary>
        ///     Gets the sort column data.
        /// </summary>
        /// <value>The sort column data.</value>
        /// TODO Edit XML Comment Template for SortColumnData
        object SortColumnData
        {
            get;
        }

        /// <summary>
        ///     Gets a value indicating whether this
        ///     <see cref="IMvcGridColumn" /> is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        /// TODO Edit XML Comment Template for Visible
        bool Visible
        {
            get;
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
        bool AllowChangeVisibility
        {
            get;
            set;
        }
    }
}