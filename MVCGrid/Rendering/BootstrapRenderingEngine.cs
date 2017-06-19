using System.IO;
using System.Text;
using System.Web;
using MichaelBrandonMorris.MvcGrid.Interfaces;
using MichaelBrandonMorris.MvcGrid.Models;

namespace MichaelBrandonMorris.MvcGrid.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    public class BootstrapRenderingEngine : IMvcGridRenderingEngine
    {
        /// <summary>
        /// 
        /// </summary>
        public const string SettingNameTableClass = "TableClass";
        private readonly string _defaultTableCss;
        private string _htmlImageSort;
        private string _htmlImageSortAsc;
        private string _htmlImageSortDsc;

        /// <summary>
        /// 
        /// </summary>
        public BootstrapRenderingEngine()
        {
            _defaultTableCss = "table table-striped table-bordered";
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AllowsPaging => true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        public void PrepareResponse(HttpResponse response)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="gridContext"></param>
        /// <param name="outputStream"></param>
        public void Render(
            RenderingModel model,
            GridContext gridContext,
            TextWriter outputStream)
        {
            _htmlImageSortAsc =
                $"<img src='{model.HandlerPath}/sortup.png' class='pull-right' />";
            _htmlImageSortDsc =
                $"<img src='{model.HandlerPath}/sortdown.png' class='pull-right' />";
            _htmlImageSort =
                $"<img src='{model.HandlerPath}/sort.png' class='pull-right' />";

            var tableCss = gridContext.GridDefinition.GetAdditionalSetting(
                SettingNameTableClass,
                _defaultTableCss);

            var sbHtml = new StringBuilder();

            sbHtml.AppendFormat("<table id='{0}'", model.TableHtmlId);
            AppendCssAttribute(tableCss, sbHtml);
            sbHtml.Append(">");

            RenderHeader(model, sbHtml);

            if (model.Rows.Count > 0)
            {
                RenderBody(model, sbHtml);
            }
            else
            {
                sbHtml.Append("<tbody>");
                sbHtml.Append("<tr>");
                sbHtml.AppendFormat(
                    "<td colspan='{0}'>",
                    model.Columns.Count);
                sbHtml.Append(model.NoResultsMessage);
                sbHtml.Append("</td>");
                sbHtml.Append("</tr>");
                sbHtml.Append("</tbody>");
            }
            sbHtml.AppendLine("</table>");

            RenderPaging(model, sbHtml);

            outputStream.Write(sbHtml.ToString());
            outputStream.Write(model.ClientDataTransferHtmlBlock);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="outputStream"></param>
        public void RenderContainer(
            ContainerRenderingModel model,
            TextWriter outputStream)
        {
            outputStream.Write(model.InnerHtmlBlock);
        }

        private static void AppendCssAttribute(
            string classString,
            StringBuilder sbHtml)
        {
            if (!string.IsNullOrWhiteSpace(classString))
            {
                sbHtml.Append($" class='{classString}'");
            }
        }

        private static void RenderBody(RenderingModel model, StringBuilder sbHtml)
        {
            sbHtml.AppendLine("<tbody>");

            foreach (var row in model.Rows)
            {
                sbHtml.Append("<tr");
                AppendCssAttribute(row.CalculatedCssClass, sbHtml);
                sbHtml.AppendLine(">");

                foreach (var col in model.Columns)
                {
                    var cell = row.Cells[col.Name];

                    sbHtml.Append("<td");
                    AppendCssAttribute(cell.CalculatedCssClass, sbHtml);
                    sbHtml.Append(">");
                    sbHtml.Append(cell.HtmlText);
                    sbHtml.Append("</td>");
                }

                sbHtml.AppendLine("  </tr>");
            }

            sbHtml.AppendLine("</tbody>");
        }

        private void RenderHeader(RenderingModel model, StringBuilder sbHtml)
        {
            sbHtml.AppendLine("<thead>");
            sbHtml.AppendLine("  <tr>");
            foreach (var col in model.Columns)
            {
                sbHtml.Append("<th");

                if (!string.IsNullOrWhiteSpace(col.Onclick))
                {
                    sbHtml.Append(" style='cursor: pointer;'");
                    sbHtml.AppendFormat(" onclick='{0}'", col.Onclick);
                }
                sbHtml.Append(">");

                sbHtml.Append(col.HeaderText);

                if (col.SortIconDirection.HasValue)
                {
                    switch (col.SortIconDirection)
                    {
                        case SortDirection.Asc:
                            sbHtml.Append(" ");
                            sbHtml.Append(_htmlImageSortAsc);
                            break;
                        case SortDirection.Dsc:
                            sbHtml.Append(" ");
                            sbHtml.Append(_htmlImageSortDsc);
                            break;
                        case SortDirection.Unspecified:
                            sbHtml.Append(" ");
                            sbHtml.Append(_htmlImageSort);
                            break;
                    }
                }

                sbHtml.AppendLine("</th>");
            }

            sbHtml.AppendLine("  </tr>");
            sbHtml.AppendLine("</thead>");
        }

        private void RenderPaging(RenderingModel model, StringBuilder sbHtml)
        {
            if (model.PagingModel == null)
            {
                return;
            }

            var pagingModel = model.PagingModel;

            sbHtml.Append("<div class=\"row\">");
            sbHtml.Append("<div class=\"col-xs-6\">");
            sbHtml.AppendFormat(
                model.SummaryMessage,
                pagingModel.FirstRecord,
                pagingModel.LastRecord,
                pagingModel.TotalRecords);
            sbHtml.Append("</div>");


            sbHtml.Append("<div class=\"col-xs-6\">");
            int pageToStart;
            int pageToEnd;
            pagingModel.CalculatePageStartAndEnd(
                5,
                out pageToStart,
                out pageToEnd);

            sbHtml.Append(
                "<ul class='pagination pull-right' style='margin-top: 0;'>");

            sbHtml.Append("<li");
            if (pageToStart == pagingModel.CurrentPage)
            {
                sbHtml.Append(" class='disabled'");
            }
            sbHtml.Append(">");

            sbHtml.AppendFormat(
                "<a href='#' aria-label='{0}' ",
                model.PreviousButtonCaption);
            if (pageToStart < pagingModel.CurrentPage
                && pagingModel.PageLinks.Count > pagingModel.CurrentPage - 1)
            {
                sbHtml.AppendFormat(
                    "onclick='{0}'",
                    pagingModel.PageLinks[pagingModel.CurrentPage - 1]);
            }
            else
            {
                sbHtml.AppendFormat("onclick='{0}'", "return false;");
            }
            sbHtml.Append(">");
            sbHtml.AppendFormat(
                "<span aria-hidden='true'>&laquo; {0}</span></a></li>",
                model.PreviousButtonCaption);

            for (var i = pageToStart; i <= pageToEnd; i++)
            {
                sbHtml.Append("<li");
                if (i == pagingModel.CurrentPage)
                {
                    sbHtml.Append(" class='active'");
                }
                sbHtml.Append(">");
                sbHtml.AppendFormat(
                    "<a href='#' onclick='{0}'>{1}</a></li>",
                    pagingModel.PageLinks[i],
                    i);
            }


            sbHtml.Append("<li");
            if (pageToEnd == pagingModel.CurrentPage)
            {
                sbHtml.Append(" class='disabled'");
            }
            sbHtml.Append(">");

            sbHtml.AppendFormat(
                "<a href='#' aria-label='{0}' ",
                model.NextButtonCaption);
            sbHtml.AppendFormat(
                "onclick='{0}'",
                pageToEnd > pagingModel.CurrentPage
                    ? pagingModel.PageLinks[pagingModel.CurrentPage + 1]
                    : "return false;");
            sbHtml.Append(">");
            sbHtml.AppendFormat(
                "<span aria-hidden='true'>{0} &raquo;</span></a></li>",
                model.NextButtonCaption);

            sbHtml.Append("</ul>");
            sbHtml.Append("</div>");
            sbHtml.Append("</div>");
        }
    }
}