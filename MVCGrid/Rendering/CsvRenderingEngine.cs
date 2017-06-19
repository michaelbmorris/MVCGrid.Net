using System;
using System.IO;
using System.Text;
using System.Web;
using MvcGrid.Interfaces;
using MvcGrid.Models;

namespace MvcGrid.Rendering
{
    /// <summary>
    /// 
    /// </summary>
    public class CsvRenderingEngine : IMvcGridRenderingEngine
    {
        /// <summary>
        /// 
        /// </summary>
        public bool AllowsPaging => false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpResponse"></param>
        public virtual void PrepareResponse(HttpResponse httpResponse)
        {
            httpResponse.Clear();
            httpResponse.ContentType = "text/csv";
            httpResponse.AddHeader(
                "content-disposition",
                "attachment; filename=\"" + GetFilename() + "\"");
            httpResponse.BufferOutput = false;
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
            var sw = outputStream;

            var sbHeaderRow = new StringBuilder();
            foreach (var col in model.Columns)
            {
                if (sbHeaderRow.Length != 0)
                {
                    sbHeaderRow.Append(",");
                }
                sbHeaderRow.Append(CsvEncode(col.Name));
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
                        sbRow.Append(",");
                    }

                    var val = cell.PlainText;

                    sbRow.Append(CsvEncode(val));
                }

                sbRow.AppendLine();
                sw.Write(sbRow.ToString());
            }
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
            throw new NotImplementedException(
                "Csv Rendering Engine has no container");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string GetFilename()
        {
            return "export.csv";
        }

        private static string CsvEncode(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return "\"\"";
            }

            var esc = s.Replace("\"", "\"\"");

            return $"\"{esc}\"";
        }
    }
}