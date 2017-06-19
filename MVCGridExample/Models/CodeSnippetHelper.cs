using System.IO;
using System.Text;
using System.Web;

namespace MVCGrid.Web.Models
{
    public class CodeSnippetHelper
    {
        public static string GetCodeSnippet(string gridName)
        {
            var cacheKey = string.Format("GetCodeSnippet_{0}", gridName);

            var cached = HttpContext.Current.Cache[cacheKey] as string;

            if (cached == null)
            {
                var s = GetCodeSnippetInternal(gridName);

                if (s != null)
                {
                    HttpContext.Current.Cache.Insert(cacheKey, s);
                }
                return s;
            }

            return cached;
        }

        public static string GetCodeSnippetInternal(string gridName)
        {
            try
            {
                var appDataPath =
                    HttpContext.Current.Server.MapPath("~/Content");
                var codeFilename =
                    Path.Combine(appDataPath, "MVCGridConfig.txt");

                string contents;
                using (var sr = new StreamReader(codeFilename))
                {
                    contents = sr.ReadToEnd();
                }

                var startPos =
                    contents.IndexOf(
                        string.Format(
                            "MVCGridDefinitionTable.Add(\"{0}\"",
                            gridName));
                startPos = contents.LastIndexOf("\n", startPos) + 1;
                var endPos = contents.IndexOf(
                    "MVCGridDefinitionTable.Add",
                    startPos + 19);

                var snippet = contents.Substring(startPos, endPos - startPos);

                var indentLength = snippet.IndexOf("MVCGridDefinitionTable");
                var sbNew = new StringBuilder();
                foreach (var line in snippet.Split('\n', '\r'))
                {
                    var newLine = line;

                    if (string.IsNullOrWhiteSpace(newLine))
                    {
                        continue;
                    }

                    //for (int i = 0; i < indentLength; i++)
                    //{

                    //}

                    if (newLine.Length > indentLength)
                    {
                        newLine = line.Substring(indentLength);
                    }
                    sbNew.AppendLine(newLine);
                }

                snippet = sbNew.ToString();
                return snippet;
            }
            catch
            {
            }

            return null;
        }
    }
}