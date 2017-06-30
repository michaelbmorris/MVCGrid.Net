using System;
using System.IO;
using System.Text;
using System.Web;
using MichaelBrandonMorris.MvcGrid.Interfaces;
using MichaelBrandonMorris.MvcGrid.Models;

namespace MichaelBrandonMorris.MvcGrid.Rendering
{
    /// <summary>
    ///     Class CsvRenderingEngine.
    /// </summary>
    /// <seealso
    ///     cref="MichaelBrandonMorris.MvcGrid.Interfaces.IMvcGridRenderingEngine" />
    /// TODO Edit XML Comment Template for CsvRenderingEngine
    public class CsvRenderingEngine : IMvcGridRenderingEngine
    {
        /// <summary>
        ///     Gets a value indicating whether [allows paging].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [allows paging]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for AllowsPaging
        public bool AllowsPaging => false;

        /// <summary>
        ///     Prepares the response.
        /// </summary>
        /// <param name="httpResponse">The HTTP response.</param>
        /// TODO Edit XML Comment Template for PrepareResponse
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
        ///     Renders the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="gridContext">The grid context.</param>
        /// <param name="outputStream">The output stream.</param>
        /// TODO Edit XML Comment Template for Render
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
        ///     Renders the container.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="outputStream">The output stream.</param>
        /// <exception cref="NotImplementedException">
        ///     Csv Rendering
        ///     Engine has no container
        /// </exception>
        /// TODO Edit XML Comment Template for RenderContainer
        public void RenderContainer(
            ContainerRenderingModel model,
            TextWriter outputStream)
        {
            throw new NotImplementedException(
                "Csv Rendering Engine has no container");
        }

        /// <summary>
        ///     Gets the filename.
        /// </summary>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GetFilename
        public virtual string GetFilename()
        {
            return "export.csv";
        }

        /// <summary>
        ///     CSVs the encode.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for CsvEncode
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