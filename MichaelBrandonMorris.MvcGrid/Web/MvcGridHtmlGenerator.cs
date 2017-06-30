using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using MichaelBrandonMorris.MvcGrid.Interfaces;
using MichaelBrandonMorris.MvcGrid.Models;
using MichaelBrandonMorris.MvcGrid.Utility;

namespace MichaelBrandonMorris.MvcGrid.Web
{
    /// <summary>
    ///     Class MvcGridHtmlGenerator.
    /// </summary>
    /// TODO Edit XML Comment Template for MvcGridHtmlGenerator
    internal class MvcGridHtmlGenerator
    {
        /// <summary>
        ///     The render loading div setting name
        /// </summary>
        /// TODO Edit XML Comment Template for RenderLoadingDivSettingName
        private const string RenderLoadingDivSettingName = "RenderLoadingDiv";

        /// <summary>
        ///     Generates the base page HTML.
        /// </summary>
        /// <param name="gridName">Name of the grid.</param>
        /// <param name="def">The definition.</param>
        /// <param name="pageParameters">The page parameters.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GenerateBasePageHtml
        internal static string GenerateBasePageHtml(
            string gridName,
            IMvcGridDefinition def,
            object pageParameters)
        {
            var definitionJson =
                GenerateClientDefinitionJson(gridName, def, pageParameters);

            var sbHtml = new StringBuilder();

            sbHtml.AppendFormat(
                "<div id='{0}' class='{1}'>",
                HtmlUtility.GetContainerHtmlId(gridName),
                HtmlUtility.ContainerCssClass);

            sbHtml.AppendFormat(
                "<input type='hidden' name='MVCGridName' value='{0}' />",
                gridName);

            sbHtml.AppendFormat(
                "<div id='MVCGrid_{0}_JsonData' style='display: none'>{1}</div>",
                gridName,
                definitionJson);

            sbHtml.AppendFormat(
                "<div id='MVCGrid_ErrorMessage_{0}' style='display: none;'>",
                gridName);

            sbHtml.Append(
                string.IsNullOrWhiteSpace(def.ErrorMessageHtml)
                    ? "An error has occured."
                    : def.ErrorMessageHtml);

            sbHtml.Append("</div>");

            var renderLoadingDiv =
                def.GetAdditionalSetting(RenderLoadingDivSettingName, true);

            if (renderLoadingDiv)
            {
                sbHtml.AppendFormat(
                    "<div id='MVCGrid_Loading_{0}' class='text-center' style='visibility: hidden'>",
                    gridName);
                sbHtml.AppendFormat(
                    "&nbsp;&nbsp;&nbsp;<img src='{0}/ajaxloader.gif' alt='{1}' style='width: 15px; height: 15px;' />",
                    HtmlUtility.GetHandlerPath(),
                    def.ProcessingMessage);
                sbHtml.AppendFormat("&nbsp;{0}...", def.ProcessingMessage);
                sbHtml.Append("</div>");
            }

            sbHtml.AppendFormat(
                "<div id='{0}'>",
                HtmlUtility.GetTableHolderHtmlId(gridName));
            sbHtml.Append("%%PRELOAD%%");
            sbHtml.Append("</div>");

            sbHtml.AppendLine("</div>");

            return sbHtml.ToString();
        }

        /// <summary>
        ///     Generates the client data transfer HTML.
        /// </summary>
        /// <param name="gridContext">The grid context.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GenerateClientDataTransferHtml
        internal static string GenerateClientDataTransferHtml(
            GridContext gridContext)
        {
            var sb = new StringBuilder();

            sb.AppendFormat(
                "<div id='MVCGrid_{0}_ContextJsonData' style='display: none;'>",
                gridContext.GridName);

            sb.Append("{");

            sb.AppendFormat(
                "\"name\": \"{0}\"",
                HttpUtility.JavaScriptStringEncode(gridContext.GridName));

            sb.Append(",");

            sb.AppendFormat(
                "\"sortColumn\": \"{0}\"",
                HttpUtility.JavaScriptStringEncode(
                    gridContext.QueryOptions.SortColumnName));

            sb.Append(",");

            sb.AppendFormat(
                "\"sortDirection\": \"{0}\"",
                gridContext.QueryOptions.SortDirection);

            sb.Append(",");

            sb.AppendFormat(
                "\"itemsPerPage\": {0}",
                gridContext.QueryOptions.ItemsPerPage.HasValue
                    ? gridContext.QueryOptions.ItemsPerPage.ToString()
                    : "\"\"");

            sb.Append(",");

            sb.AppendFormat(
                "\"pageNumber\": {0}",
                gridContext.QueryOptions.PageIndex.HasValue
                    ? (gridContext.QueryOptions.PageIndex + 1).ToString()
                    : "\"\"");

            sb.Append(",");
            sb.Append("\"columnVisibility\": {");
            sb.Append(GenerateClientJsonVisibility(gridContext));
            sb.Append("}");
            sb.Append(",");
            sb.Append("\"filters\": {");
            sb.Append(GenerateClientJsonFilter(gridContext));
            sb.Append("}");
            sb.Append(",");
            sb.Append("\"additionalQueryOptions\": {");
            sb.Append(GenerateClientJsonAdditional(gridContext));
            sb.Append("}");
            sb.Append("}");
            sb.Append("</div>");
            return sb.ToString();
        }

        /// <summary>
        ///     Generates the client definition json.
        /// </summary>
        /// <param name="gridName">Name of the grid.</param>
        /// <param name="def">The definition.</param>
        /// <param name="pageParameters">The page parameters.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GenerateClientDefinitionJson
        private static string GenerateClientDefinitionJson(
            string gridName,
            IMvcGridDefinition def,
            object pageParameters)
        {
            var sbJson = new StringBuilder();

            sbJson.Append("{");
            sbJson.AppendFormat("\"name\": \"{0}\"", gridName);
            sbJson.Append(",");
            sbJson.AppendFormat("\"qsPrefix\": \"{0}\"", def.QueryStringPrefix);
            sbJson.Append(",");

            var preloadedAlready = def.PreloadData || !def.QueryOnPageLoad;

            sbJson.AppendFormat(
                "\"preloaded\": {0}",
                preloadedAlready.ToString().ToLower());

            sbJson.Append(",");

            sbJson.AppendFormat(
                "\"clientLoading\": \"{0}\"",
                def.ClientSideLoadingMessageFunctionName);

            sbJson.Append(",");

            sbJson.AppendFormat(
                "\"clientLoadingComplete\": \"{0}\"",
                def.ClientSideLoadingCompleteFunctionName);

            sbJson.Append(",");

            sbJson.AppendFormat(
                "\"renderingMode\": \"{0}\"",
                def.RenderingMode.ToString().ToLower());

            sbJson.Append(",");
            sbJson.Append("\"pageParameters\": {");
            sbJson.Append(GenerateJsonPageParameters(pageParameters));
            sbJson.Append("}");
            sbJson.Append("}");
            return sbJson.ToString();
        }

        /// <summary>
        ///     Generates the client json additional.
        /// </summary>
        /// <param name="gridContext">The grid context.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GenerateClientJsonAdditional
        private static string GenerateClientJsonAdditional(
            GridContext gridContext)
        {
            var sb = new StringBuilder();

            foreach (var aqon in gridContext.GridDefinition
                .AdditionalQueryOptionNames)
            {
                var val = "";

                if (gridContext.QueryOptions.AdditionalQueryOptions
                    .ContainsKey(aqon))
                {
                    val = gridContext.QueryOptions.AdditionalQueryOptions[aqon];
                }

                if (sb.Length > 0)
                {
                    sb.Append(",");
                }

                sb.AppendFormat(
                    "\"{0}\": \"{1}\"",
                    aqon,
                    HttpUtility.JavaScriptStringEncode(val));
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Generates the client json filter.
        /// </summary>
        /// <param name="gridContext">The grid context.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GenerateClientJsonFilter
        private static string GenerateClientJsonFilter(GridContext gridContext)
        {
            var sb = new StringBuilder();

            var filterableColumns = gridContext.GridDefinition.GetColumns()
                .Where(p => p.EnableFiltering);

            foreach (var col in filterableColumns)
            {
                var val = "";

                if (gridContext.QueryOptions.Filters
                    .ContainsKey(col.ColumnName))
                {
                    val = gridContext.QueryOptions.Filters[col.ColumnName];
                }

                if (sb.Length > 0)
                {
                    sb.Append(",");
                }

                sb.AppendFormat(
                    "\"{0}\": \"{1}\"",
                    col.ColumnName,
                    HttpUtility.JavaScriptStringEncode(val));
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Generates the client json visibility.
        /// </summary>
        /// <param name="gridContext">The grid context.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GenerateClientJsonVisibility
        private static string GenerateClientJsonVisibility(
            GridContext gridContext)
        {
            var gridColumns = gridContext.GridDefinition.GetColumns().ToList();
            var sb = new StringBuilder();

            foreach (var cv in gridContext.QueryOptions.ColumnVisibility)
            {
                var gridColumn =
                    gridColumns.SingleOrDefault(
                        p => p.ColumnName == cv.ColumnName);

                if (sb.Length > 0)
                {
                    sb.Append(",");
                }

                sb.AppendFormat("\"{0}\": {{", cv.ColumnName);

                if (gridColumn == null)
                {
                    // TODO Edit exception
                    throw new Exception();
                }

                sb.AppendFormat(
                    "\"{0}\": \"{1}\"",
                    "headerText",
                    HttpUtility.JavaScriptStringEncode(gridColumn.HeaderText));
                sb.Append(",");

                sb.AppendFormat(
                    "\"{0}\": {1}",
                    "visible",
                    cv.Visible.ToString().ToLower());

                sb.Append(",");

                sb.AppendFormat(
                    "\"{0}\": {1}",
                    "allow",
                    gridColumn.AllowChangeVisibility.ToString().ToLower());

                sb.Append("}");
            }

            return sb.ToString();
        }

        /// <summary>
        ///     Generates the json page parameters.
        /// </summary>
        /// <param name="pageParameters">The page parameters.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GenerateJsonPageParameters
        private static string GenerateJsonPageParameters(object pageParameters)
        {
            var sb = new StringBuilder();

            var pageParamsDict = new Dictionary<string, string>();

            if (pageParameters != null)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor
                    .GetProperties(pageParameters))
                {
                    var obj2 = descriptor.GetValue(pageParameters);

                    if (obj2 == null)
                    {
                        // TODO Edit exception
                        throw new Exception();
                    }

                    pageParamsDict.Add(descriptor.Name, obj2.ToString());
                }
            }

            foreach (var col in pageParamsDict)
            {
                var val = col.Value;

                if (sb.Length > 0)
                {
                    sb.Append(",");
                }

                sb.AppendFormat(
                    "\"{0}\": \"{1}\"",
                    col.Key,
                    HttpUtility.JavaScriptStringEncode(val));
            }

            return sb.ToString();
        }
    }
}