﻿using System;
using System.Collections.Generic;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public class GridColumnListBuilder<T1>
    {
        private readonly ColumnDefaults _columnDefaults;

        /// <summary>
        /// 
        /// </summary>
        public GridColumnListBuilder()
            : this(null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnDefaults"></param>
        public GridColumnListBuilder(ColumnDefaults columnDefaults)
        {
            ColumnBuilders = new List<GridColumnBuilder<T1>>();

            _columnDefaults = columnDefaults;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<GridColumnBuilder<T1>> ColumnBuilders
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GridColumnBuilder<T1> Add()
        {
            return Add(null, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public GridColumnBuilder<T1> Add(string columnName)
        {
            return Add(columnName, null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="headerText"></param>
        /// <param name="valueExpression"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
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
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public class GridColumnBuilder<T1>
    {
        /// <summary>
        /// 
        /// </summary>
        public GridColumnBuilder()
            : this(null, null, null, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        public GridColumnBuilder(string columnName)
            : this(columnName, null, null, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="headerText"></param>
        /// <param name="valueExpression"></param>
        public GridColumnBuilder(
            string columnName,
            string headerText,
            Func<T1, string> valueExpression)
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
        /// 
        /// </summary>
        public GridColumn<T1> GridColumn
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the column visibility can be changed.
        /// </summary>
        public GridColumnBuilder<T1> WithAllowChangeVisibility(bool allow)
        {
            GridColumn.AllowChangeVisibility = allow;
            return this;
        }

        /// <summary>
        ///     Use this to return a custom css class based on data for the current cell
        /// </summary>
        public GridColumnBuilder<T1> WithCellCssClassExpression(
            Func<T1, GridContext, string> expression)
        {
            GridColumn.CellCssClassExpression = expression;
            return this;
        }

        /// <summary>
        ///     Use this to return a custom css class based on data for the current cell
        /// </summary>
        public GridColumnBuilder<T1> WithCellCssClassExpression(
            Func<T1, string> expression)
        {
            GridColumn.CellCssClassExpression =
                (t1, gridContext) => expression(t1);
            return this;
        }


        /// <summary>
        ///     A unique name for this column
        /// </summary>
        public GridColumnBuilder<T1> WithColumnName(string name)
        {
            GridColumn.ColumnName = name;
            return this;
        }

        /// <summary>
        ///     Enables filtering on this column
        /// </summary>
        public GridColumnBuilder<T1> WithFiltering(bool enableFiltering)
        {
            GridColumn.EnableFiltering = enableFiltering;
            return this;
        }

        /// <summary>
        ///     Header text to display for the current column, if different from ColumnName.
        /// </summary>
        public GridColumnBuilder<T1> WithHeaderText(string text)
        {
            GridColumn.HeaderText = text;
            return this;
        }

        /// <summary>
        ///     Disables html encoding on the data for the current cell. Turn this off if your ValueExpression or ValueTemplate
        ///     returns HTML.
        /// </summary>
        public GridColumnBuilder<T1> WithHtmlEncoding(bool htmlEncode)
        {
            GridColumn.HtmlEncode = htmlEncode;
            return this;
        }


        /// <summary>
        ///     This is how to specify the contents of the current cell when used in an export file, if different that
        ///     ValueExpression
        /// </summary>
        public GridColumnBuilder<T1> WithPlainTextValueExpression(
            Func<T1, GridContext, string> expression)
        {
            GridColumn.PlainTextValueExpression = expression;
            return this;
        }

        /// <summary>
        ///     This is how to specify the contents of the current cell when used in an export file, if different that
        ///     ValueExpression
        /// </summary>
        public GridColumnBuilder<T1> WithPlainTextValueExpression(
            Func<T1, string> expression)
        {
            GridColumn.PlainTextValueExpression =
                (t1, gridContext) => expression(t1);
            return this;
        }

        /// <summary>
        ///     Object to pass to QueryOptions when this column is sorted on. Only specify if different from ColumnName
        /// </summary>
        public GridColumnBuilder<T1> WithSortColumnData(object sortColumnData)
        {
            GridColumn.SortColumnData = sortColumnData;
            return this;
        }


        /// <summary>
        ///     Enables sorting on this column
        /// </summary>
        public GridColumnBuilder<T1> WithSorting(bool enableSorting)
        {
            GridColumn.EnableSorting = enableSorting;
            return this;
        }

        /// <summary>
        ///     This is how to specify the contents of the current cell. If this contains HTML, set HTMLEncode to false
        /// </summary>
        public GridColumnBuilder<T1> WithValueExpression(
            Func<T1, GridContext, string> expression)
        {
            GridColumn.ValueExpression = expression;
            return this;
        }

        /// <summary>
        ///     This is how to specify the contents of the current cell. If this contains HTML, set HTMLEncode to false
        /// </summary>
        public GridColumnBuilder<T1> WithValueExpression(
            Func<T1, string> expression)
        {
            GridColumn.ValueExpression = (t1, gridContext) => expression(t1);
            return this;
        }


        /// <summary>
        ///     Template for formatting cell value
        /// </summary>
        public GridColumnBuilder<T1> WithValueTemplate(string template)
        {
            GridColumn.ValueTemplate = template;
            return this;
        }

        /// <summary>
        ///     Template for formatting cell value
        /// </summary>
        public GridColumnBuilder<T1> WithValueTemplate(
            string template,
            bool htmlEncode)
        {
            GridColumn.ValueTemplate = template;
            GridColumn.HtmlEncode = htmlEncode;
            return this;
        }

        /// <summary>
        ///     Indicates whether column is visible.
        /// </summary>
        public GridColumnBuilder<T1> WithVisibility(bool visible)
        {
            GridColumn.Visible = visible;
            return this;
        }

        /// <summary>
        ///     Indicates whether column is visible.
        /// </summary>
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