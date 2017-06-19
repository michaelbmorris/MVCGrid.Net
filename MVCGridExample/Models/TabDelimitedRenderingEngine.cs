using System.IO;
using System.Text;
using System.Web;
using MvcGrid.Interfaces;
using MvcGrid.Models;

namespace MvcGrid.Web.Models
{
    public class TabDelimitedRenderingEngine : IMvcGridRenderingEngine
    {
        public bool AllowsPaging => false;

        public void PrepareResponse(HttpResponse httpResponse)
        {
            httpResponse.Clear();
            httpResponse.ContentType = "text/tab-separated-values";
            httpResponse.AddHeader(
                "content-disposition",
                "attachment; filename=\"" + "export" + ".tsv\"");
            httpResponse.BufferOutput = false;
        }

        public void Render(
            RenderingModel model,
            GridContext gridContext,
            TextWriter outputStream)
        {
            var sw = outputStream;

            var sbHeaderRow = new StringBuilder();
            foreach (var col in model.Columns)
            {
                if (sbHeaderRow.Length != 0)
                {
                    sbHeaderRow.Append("\t");
                }
                sbHeaderRow.Append(Encode(col.Name));
            }

            sbHeaderRow.AppendLine();
            sw.Write(sbHeaderRow.ToString());

            foreach (var item in model.Rows)
            {
                var sbRow = new StringBuilder();
                foreach (var col in model.Columns)
                {
                    var cell = item.Cells[col.Name];

                    if (sbRow.Length != 0)
                    {
                        sbRow.Append("\t");
                    }

                    var val = cell.PlainText;

                    sbRow.Append(Encode(val));
                }

                sbRow.AppendLine();
                sw.Write(sbRow.ToString());
            }
        }

        public void RenderContainer(
            ContainerRenderingModel model,
            TextWriter outputStream)
        {
        }

        private string Encode(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return "";
            }

            if (s.Contains("\t"))
            {
                s = s.Replace("\t", " ");
            }

            return s;
        }
    }
}