using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using MichaelBrandonMorris.MvcGrid.Models;

namespace MichaelBrandonMorris.MvcGrid.Utility
{
    /// <summary>
    ///     Class HtmlUtility.
    /// </summary>
    /// TODO Edit XML Comment Template for HtmlUtility
    public class HtmlUtility
    {
        /// <summary>
        ///     The container CSS class
        /// </summary>
        /// TODO Edit XML Comment Template for ContainerCssClass
        public const string ContainerCssClass = "MVCGridContainer";

        /// <summary>
        ///     Gets the container HTML identifier.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GetContainerHtmlId
        public static string GetContainerHtmlId(string name)
        {
            return $"MVCGridContainer_{name}";
        }

        /// <summary>
        ///     Gets the handler path.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <exception cref="Exception"></exception>
        /// TODO Edit XML Comment Template for GetHandlerPath
        public static string GetHandlerPath()
        {
            if (HttpContext.Current.Request.ApplicationPath == null)
            {
                throw new Exception();
            }

            var appPath =
                HttpContext.Current.Request.ApplicationPath.TrimEnd('/');

            return appPath + "/MVCGridHandler.axd";
        }

        /// <summary>
        ///     Gets the table holder HTML identifier.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GetTableHolderHtmlId
        public static string GetTableHolderHtmlId(string name)
        {
            return $"MVCGridTableHolder_{name}";
        }

        /// <summary>
        ///     Gets the table HTML identifier.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GetTableHtmlId
        public static string GetTableHtmlId(string name)
        {
            return $"MVCGridTable_{name}";
        }

        /// <summary>
        ///     Makes the CSS class attribute stirng.
        /// </summary>
        /// <param name="classes">The classes.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for MakeCssClassAttributeStirng
        public static string MakeCssClassAttributeStirng(
            HashSet<string> classes)
        {
            if (classes == null
                || classes.Count == 0)
            {
                return "";
            }

            return $" class='{MakeCssClassStirng(classes)}'";
        }

        /// <summary>
        ///     Makes the CSS class attribute stirng.
        /// </summary>
        /// <param name="classString">The class string.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for MakeCssClassAttributeStirng
        public static string MakeCssClassAttributeStirng(string classString)
        {
            return string.IsNullOrWhiteSpace(classString)
                ? ""
                : $" class='{classString}'";
        }

        /// <summary>
        ///     Makes the CSS class stirng.
        /// </summary>
        /// <param name="classes">The classes.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for MakeCssClassStirng
        public static string MakeCssClassStirng(HashSet<string> classes)
        {
            var sb = new StringBuilder();

            foreach (var c in classes)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" ");
                }
                sb.Append(c);
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Makes the goto page link.
        /// </summary>
        /// <param name="gridName">Name of the grid.</param>
        /// <param name="pageNum">The page number.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for MakeGotoPageLink
        public static string MakeGotoPageLink(string gridName, int pageNum)
        {
            return $"MVCGrid.setPage(\"{gridName}\", {pageNum}); return false;";
        }

        /// <summary>
        ///     Makes the sort link.
        /// </summary>
        /// <param name="gridName">Name of the grid.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="direction">The direction.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for MakeSortLink
        public static string MakeSortLink(
            string gridName,
            string columnName,
            SortDirection direction)
        {
            return
                $"MVCGrid.setSort(\"{gridName}\", \"{columnName}\", \"{direction}\"); return false;";
        }
    }
}