using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using MvcGrid.Models;

namespace MvcGrid.Utility
{
    /// <summary>
    /// </summary>
    public class HtmlUtility
    {
        /// <summary>
        /// </summary>
        public const string ContainerCssClass = "MVCGridContainer";

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetContainerHtmlId(string name)
        {
            return $"MVCGridContainer_{name}";
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
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
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetTableHolderHtmlId(string name)
        {
            return $"MVCGridTableHolder_{name}";
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetTableHtmlId(string name)
        {
            return $"MVCGridTable_{name}";
        }

        /// <summary>
        /// </summary>
        /// <param name="classes"></param>
        /// <returns></returns>
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
        /// </summary>
        /// <param name="classString"></param>
        /// <returns></returns>
        public static string MakeCssClassAttributeStirng(string classString)
        {
            return string.IsNullOrWhiteSpace(classString)
                ? ""
                : $" class='{classString}'";
        }

        /// <summary>
        /// </summary>
        /// <param name="classes"></param>
        /// <returns></returns>
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
        /// </summary>
        /// <param name="gridName"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public static string MakeGotoPageLink(string gridName, int pageNum)
        {
            return $"MVCGrid.setPage(\"{gridName}\", {pageNum}); return false;";
        }

        /// <summary>
        /// </summary>
        /// <param name="gridName"></param>
        /// <param name="columnName"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
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