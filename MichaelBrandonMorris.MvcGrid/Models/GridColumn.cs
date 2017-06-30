using System;
using MichaelBrandonMorris.MvcGrid.Interfaces;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    ///     Class GridColumn.
    /// </summary>
    /// <typeparam name="T1">The type of the t1.</typeparam>
    /// <seealso
    ///     cref="MichaelBrandonMorris.MvcGrid.Interfaces.IMvcGridColumn" />
    /// TODO Edit XML Comment Template for GridColumn`1
    public class GridColumn<T1> : IMvcGridColumn
    {
        /// <summary>
        ///     The header text
        /// </summary>
        /// TODO Edit XML Comment Template for _headerText
        private string _headerText;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GridColumn{T1}" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public GridColumn()
            : this(null, null, null, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GridColumn{T1}" /> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="headerText">The header text.</param>
        /// <param name="valueExpression">The value expression.</param>
        /// TODO Edit XML Comment Template for #ctor
        public GridColumn(
            string columnName,
            string headerText,
            Func<T1, GridContext, string> valueExpression)
            : this(columnName, headerText, valueExpression, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GridColumn{T1}" /> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="headerText">The header text.</param>
        /// <param name="valueExpression">The value expression.</param>
        /// <param name="columnDefaults">The column defaults.</param>
        /// TODO Edit XML Comment Template for #ctor
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
        ///     Gets or sets the cell CSS class expression.
        /// </summary>
        /// <value>The cell CSS class expression.</value>
        /// TODO Edit XML Comment Template for CellCssClassExpression
        public Func<T1, GridContext, string> CellCssClassExpression
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the plain text value expression.
        /// </summary>
        /// <value>The plain text value expression.</value>
        /// TODO Edit XML Comment Template for PlainTextValueExpression
        public Func<T1, GridContext, string> PlainTextValueExpression
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the value expression.
        /// </summary>
        /// <value>The value expression.</value>
        /// TODO Edit XML Comment Template for ValueExpression
        public Func<T1, GridContext, string> ValueExpression
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the value template.
        /// </summary>
        /// <value>The value template.</value>
        /// TODO Edit XML Comment Template for ValueTemplate
        public string ValueTemplate
        {
            get;
            set;
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