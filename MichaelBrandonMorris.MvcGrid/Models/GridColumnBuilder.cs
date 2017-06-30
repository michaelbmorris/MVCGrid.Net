using System;
using System.Collections.Generic;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    ///     Class GridColumnListBuilder.
    /// </summary>
    /// <typeparam name="T1">The type of the t1.</typeparam>
    /// TODO Edit XML Comment Template for GridColumnListBuilder`1
    public class GridColumnListBuilder<T1>
    {
        /// <summary>
        ///     The column defaults
        /// </summary>
        /// TODO Edit XML Comment Template for _columnDefaults
        private readonly ColumnDefaults _columnDefaults;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GridColumnListBuilder{T1}" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public GridColumnListBuilder()
            : this(null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GridColumnListBuilder{T1}" /> class.
        /// </summary>
        /// <param name="columnDefaults">The column defaults.</param>
        /// TODO Edit XML Comment Template for #ctor
        public GridColumnListBuilder(ColumnDefaults columnDefaults)
        {
            ColumnBuilders = new List<GridColumnBuilder<T1>>();

            _columnDefaults = columnDefaults;
        }

        /// <summary>
        ///     Gets or sets the column builders.
        /// </summary>
        /// <value>The column builders.</value>
        /// TODO Edit XML Comment Template for ColumnBuilders
        public List<GridColumnBuilder<T1>> ColumnBuilders
        {
            get;
            set;
        }

        /// <summary>
        ///     Adds this instance.
        /// </summary>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for Add
        public GridColumnBuilder<T1> Add()
        {
            return Add(null, null, null);
        }

        /// <summary>
        ///     Adds the specified column name.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for Add
        public GridColumnBuilder<T1> Add(string columnName)
        {
            return Add(columnName, null, null);
        }

        /// <summary>
        ///     Adds the specified column name.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="headerText">The header text.</param>
        /// <param name="valueExpression">The value expression.</param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for Add
        public GridColumnBuilder<T1> Add(
            string columnName,
            string headerText,
            Func<T1, string> valueExpression)
        {
            var col = new GridColumnBuilder<T1>(
                columnName,
                headerText,
                valueExpression,
                _columnDefaults);

            ColumnBuilders.Add(col);

            return col;
        }

        /// <summary>
        ///     Adds the specified column.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for Add
        public GridColumnBuilder<T1> Add(GridColumn<T1> column)
        {
            var col = new GridColumnBuilder<T1>
            {
                GridColumn = column
            };

            ColumnBuilders.Add(col);
            return col;
        }
    }

    /// <summary>
    ///     Class GridColumnBuilder.
    /// </summary>
    /// <typeparam name="T1">The type of the t1.</typeparam>
    /// TODO Edit XML Comment Template for GridColumnBuilder`1
    public class GridColumnBuilder<T1>
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GridColumnBuilder{T1}" /> class.
        /// </summary>
        /// TODO Edit XML Comment Template for #ctor
        public GridColumnBuilder()
            : this(null, null, null, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GridColumnBuilder{T1}" /> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// TODO Edit XML Comment Template for #ctor
        public GridColumnBuilder(string columnName)
            : this(columnName, null, null, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GridColumnBuilder{T1}" /> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="headerText">The header text.</param>
        /// <param name="valueExpression">The value expression.</param>
        /// TODO Edit XML Comment Template for #ctor
        public GridColumnBuilder(
            string columnName,
            string headerText,
            Func<T1, string> valueExpression)
            : this(columnName, headerText, valueExpression, null)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="GridColumnBuilder{T1}" /> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="headerText">The header text.</param>
        /// <param name="valueExpression">The value expression.</param>
        /// <param name="columnDefaults">The column defaults.</param>
        /// TODO Edit XML Comment Template for #ctor
        public GridColumnBuilder(
            string columnName,
            string headerText,
            Func<T1, string> valueExpression,
            ColumnDefaults columnDefaults)
        {
            Func<T1, GridContext, string> newVe = null;
            if (valueExpression != null)
            {
                newVe = (t1, gridContext) => valueExpression(t1);
            }

            GridColumn = new GridColumn<T1>(
                columnName,
                headerText,
                newVe,
                columnDefaults);
        }

        /// <summary>
        ///     Gets or sets the grid column.
        /// </summary>
        /// <value>The grid column.</value>
        /// TODO Edit XML Comment Template for GridColumn
        public GridColumn<T1> GridColumn
        {
            get;
            set;
        }

        /// <summary>
        ///     Withes the allow change visibility.
        /// </summary>
        /// <param name="allow">if set to <c>true</c> [allow].</param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithAllowChangeVisibility
        public GridColumnBuilder<T1> WithAllowChangeVisibility(bool allow)
        {
            GridColumn.AllowChangeVisibility = allow;
            return this;
        }

        /// <summary>
        ///     Withes the cell CSS class expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithCellCssClassExpression
        public GridColumnBuilder<T1> WithCellCssClassExpression(
            Func<T1, GridContext, string> expression)
        {
            GridColumn.CellCssClassExpression = expression;
            return this;
        }

        /// <summary>
        ///     Withes the cell CSS class expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithCellCssClassExpression
        public GridColumnBuilder<T1> WithCellCssClassExpression(
            Func<T1, string> expression)
        {
            GridColumn.CellCssClassExpression =
                (t1, gridContext) => expression(t1);
            return this;
        }


        /// <summary>
        ///     Withes the name of the column.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithColumnName
        public GridColumnBuilder<T1> WithColumnName(string name)
        {
            GridColumn.ColumnName = name;
            return this;
        }

        /// <summary>
        ///     Withes the filtering.
        /// </summary>
        /// <param name="enableFiltering">
        ///     if set to <c>true</c> [enable
        ///     filtering].
        /// </param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithFiltering
        public GridColumnBuilder<T1> WithFiltering(bool enableFiltering)
        {
            GridColumn.EnableFiltering = enableFiltering;
            return this;
        }

        /// <summary>
        ///     Withes the header text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithHeaderText
        public GridColumnBuilder<T1> WithHeaderText(string text)
        {
            GridColumn.HeaderText = text;
            return this;
        }

        /// <summary>
        ///     Withes the HTML encoding.
        /// </summary>
        /// <param name="htmlEncode">
        ///     if set to <c>true</c> [HTML
        ///     encode].
        /// </param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithHtmlEncoding
        public GridColumnBuilder<T1> WithHtmlEncoding(bool htmlEncode)
        {
            GridColumn.HtmlEncode = htmlEncode;
            return this;
        }


        /// <summary>
        ///     Withes the plain text value expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithPlainTextValueExpression
        public GridColumnBuilder<T1> WithPlainTextValueExpression(
            Func<T1, GridContext, string> expression)
        {
            GridColumn.PlainTextValueExpression = expression;
            return this;
        }

        /// <summary>
        ///     Withes the plain text value expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithPlainTextValueExpression
        public GridColumnBuilder<T1> WithPlainTextValueExpression(
            Func<T1, string> expression)
        {
            GridColumn.PlainTextValueExpression =
                (t1, gridContext) => expression(t1);
            return this;
        }

        /// <summary>
        ///     Withes the sort column data.
        /// </summary>
        /// <param name="sortColumnData">The sort column data.</param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithSortColumnData
        public GridColumnBuilder<T1> WithSortColumnData(object sortColumnData)
        {
            GridColumn.SortColumnData = sortColumnData;
            return this;
        }


        /// <summary>
        ///     Withes the sorting.
        /// </summary>
        /// <param name="enableSorting">
        ///     if set to <c>true</c> [enable
        ///     sorting].
        /// </param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithSorting
        public GridColumnBuilder<T1> WithSorting(bool enableSorting)
        {
            GridColumn.EnableSorting = enableSorting;
            return this;
        }

        /// <summary>
        ///     Withes the value expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithValueExpression
        public GridColumnBuilder<T1> WithValueExpression(
            Func<T1, GridContext, string> expression)
        {
            GridColumn.ValueExpression = expression;
            return this;
        }

        /// <summary>
        ///     Withes the value expression.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithValueExpression
        public GridColumnBuilder<T1> WithValueExpression(
            Func<T1, string> expression)
        {
            GridColumn.ValueExpression = (t1, gridContext) => expression(t1);
            return this;
        }


        /// <summary>
        ///     Withes the value template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithValueTemplate
        public GridColumnBuilder<T1> WithValueTemplate(string template)
        {
            GridColumn.ValueTemplate = template;
            return this;
        }

        /// <summary>
        ///     Withes the value template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="htmlEncode">
        ///     if set to <c>true</c> [HTML
        ///     encode].
        /// </param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithValueTemplate
        public GridColumnBuilder<T1> WithValueTemplate(
            string template,
            bool htmlEncode)
        {
            GridColumn.ValueTemplate = template;
            GridColumn.HtmlEncode = htmlEncode;
            return this;
        }

        /// <summary>
        ///     Withes the visibility.
        /// </summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithVisibility
        public GridColumnBuilder<T1> WithVisibility(bool visible)
        {
            GridColumn.Visible = visible;
            return this;
        }

        /// <summary>
        ///     Withes the visibility.
        /// </summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        /// <param name="allowChangeVisibility">
        ///     if set to <c>true</c>
        ///     [allow change visibility].
        /// </param>
        /// <returns>GridColumnBuilder&lt;T1&gt;.</returns>
        /// TODO Edit XML Comment Template for WithVisibility
        public GridColumnBuilder<T1> WithVisibility(
            bool visible,
            bool allowChangeVisibility)
        {
            GridColumn.Visible = visible;
            GridColumn.AllowChangeVisibility = allowChangeVisibility;
            return this;
        }
    }
}