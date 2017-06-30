using System;
using MichaelBrandonMorris.MvcGrid.Interfaces;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public class GridColumn<T1> : IMvcGridColumn
    {
        private string _headerText;

        /// <summary>
        /// 
        /// </summary>
        public GridColumn()
            : this(null, null, null, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="headerText"></param>
        /// <param name="valueExpression"></param>
        public GridColumn(
            string columnName,
            string headerText,
            Func<T1, GridContext, string> valueExpression)
            : this(columnName, headerText, valueExpression, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="headerText"></param>
        /// <param name="valueExpression"></param>
        /// <param name="columnDefaults"></param>
        public GridColumn(
            string columnName,
            string headerText,
            Func<T1, GridContext, string> valueExpression,
            ColumnDefaults columnDefaults)
        {
            if (!string.IsNullOrWhiteSpace(columnName))
            {
                ColumnName = columnName;
            }

            if (!string.IsNullOrWhiteSpace(headerText))
            {
                HeaderText = headerText;
            }

            if (valueExpression != null)
            {
                ValueExpression = valueExpression;
            }

            if (columnDefaults == null)
            {
                columnDefaults = new ColumnDefaults();
            }

            EnableSorting = columnDefaults.EnableSorting;
            HtmlEncode = columnDefaults.HtmlEncode;
            EnableFiltering = columnDefaults.EnableFiltering;
            Visible = columnDefaults.Visible;
            SortColumnData = columnDefaults.SortColumnData;
            AllowChangeVisibility = columnDefaults.AllowChangeVisibility;
        }

        /// <summary>
        ///     Use this to return a custom css class based on data for the current cell
        /// </summary>
        public Func<T1, GridContext, string> CellCssClassExpression
        {
            get;
            set;
        }

        /// <summary>
        ///     This is how to specify the contents of the current cell when used in an export file, if different that
        ///     ValueExpression
        /// </summary>
        public Func<T1, GridContext, string> PlainTextValueExpression
        {
            get;
            set;
        }

        /// <summary>
        ///     This is how to specify the contents of the current cell. If this contains HTML, set HTMLEncode to false
        /// </summary>
        public Func<T1, GridContext, string> ValueExpression
        {
            get;
            set;
        }

        /// <summary>
        ///     Template for formatting cell value
        /// </summary>
        public string ValueTemplate
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the column visibility can be changed.
        /// </summary>
        public bool AllowChangeVisibility
        {
            get;
            set;
        }

        /// <summary>
        ///     A unique name for this column
        /// </summary>
        public string ColumnName
        {
            get;
            set;
        }


        /// <summary>
        ///     Enables filtering on this column
        /// </summary>
        public bool EnableFiltering
        {
            get;
            set;
        }

        /// <summary>
        ///     Enables sorting on this column
        /// </summary>
        public bool EnableSorting
        {
            get;
            set;
        }

        /// <summary>
        ///     Header text to display for the current column, if different from ColumnName.
        /// </summary>
        public string HeaderText
        {
            get
            {
                if (_headerText == null)
                {
                    return ColumnName;
                }

                return _headerText;
            }
            set => _headerText = value;
        }


        /// <summary>
        ///     Disables html encoding on the data for the current cell. Turn this off if your ValueExpression or ValueTemplate
        ///     returns HTML.
        /// </summary>
        public bool HtmlEncode
        {
            get;
            set;
        }

        /// <summary>
        ///     Object to pass to QueryOptions when this column is sorted on. Only specify if different from ColumnName
        /// </summary>
        public object SortColumnData
        {
            get;
            set;
        }


        /// <summary>
        ///     Indicates whether column is visible.
        /// </summary>
        public bool Visible
        {
            get;
            set;
        }
    }
}