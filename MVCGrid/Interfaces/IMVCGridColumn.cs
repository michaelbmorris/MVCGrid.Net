﻿namespace MvcGrid.Interfaces
{
    /// <summary>
    /// </summary>
    public interface IMvcGridColumn
    {
        /// <summary>
        ///     A unique name for this column
        /// </summary>
        string ColumnName
        {
            get;
        }


        /// <summary>
        ///     Enables filtering on this column
        /// </summary>
        bool EnableFiltering
        {
            get;
        }

        /// <summary>
        ///     Enables sorting on this column
        /// </summary>
        bool EnableSorting
        {
            get;
        }

        /// <summary>
        ///     Header text to display for the current column, if different from ColumnName.
        /// </summary>
        string HeaderText
        {
            get;
        }

        /// <summary>
        ///     Disables html encoding on the data for the current cell. Turn this off if your ValueExpression or ValueTemplate
        ///     returns HTML.
        /// </summary>
        bool HtmlEncode
        {
            get;
        }

        /// <summary>
        ///     Object to pass to QueryOptions when this column is sorted on. Only specify if different from ColumnName
        /// </summary>
        object SortColumnData
        {
            get;
        }

        /// <summary>
        ///     Indicates whether column is visible.
        /// </summary>
        bool Visible
        {
            get;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the column visibility can be changed.
        /// </summary>
        bool AllowChangeVisibility
        {
            get;
            set;
        }
    }
}