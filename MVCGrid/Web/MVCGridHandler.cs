using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using MvcGrid.Engine;
using MvcGrid.Utility;

namespace MvcGrid.Web
{
    /// <summary>
    /// </summary>
    public class MvcGridHandler : IHttpHandler
    {
        private static readonly object Lock = new object();
        private static Dictionary<string, byte[]> _cachedBinaryResources;
        private static Dictionary<string, string> _cachedTextResources;
        private static bool _init;

        /// <summary>
        /// </summary>
        public bool IsReusable => false;

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            Init();

            if (context.Request.Path.ToLower().EndsWith("/script.js"))
            {
                HandleScript(context);
            }
            else if (context.Request.Path.ToLower().EndsWith("/ajaxloader.gif"))
            {
                HandelGifImage(context, "ajaxloader.gif");
            }
            else if (context.Request.Path.ToLower().EndsWith("/sort.png"))
            {
                HandelPngImage(context, "sort.png");
            }
            else if (context.Request.Path.ToLower().EndsWith("/sortdown.png"))
            {
                HandelPngImage(context, "sortdown.png");
            }
            else if (context.Request.Path.ToLower().EndsWith("/sortup.png"))
            {
                HandelPngImage(context, "sortup.png");
            }
            else
            {
                HandleTable(context);
            }
        }

        /// <summary>
        /// </summary>
        public void Init()
        {
            if (_init)
            {
                return;
            }

            lock (Lock)
            {
                if (_init)
                {
                    return;
                }

                _cachedBinaryResources = new Dictionary<string, byte[]>();
                _cachedTextResources = new Dictionary<string, string>();


                var script = GetTextResource("MVCGrid.js");
                var handlerPath = HttpContext.Current.Request
                    .CurrentExecutionFilePath;
                script = script.Replace("%%HANDLERPATH%%", handlerPath);

                var showErrorDetails =
                    ConfigUtility.GetShowErrorDetailsSetting();
                script = script.Replace(
                    "%%ERRORDETAILS%%",
                    showErrorDetails.ToString().ToLower());


                var controllerPath = HttpContext.Current.Request
                    .ApplicationPath;
                controllerPath += "mvcgrid/grid";

                try
                {
                    var urlHelper =
                        new UrlHelper(
                            HttpContext.Current.Request.RequestContext);
                    controllerPath = urlHelper.Action("Grid", "MVCGrid");
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }

                script = script.Replace("%%CONTROLLERPATH%%", controllerPath);

                _cachedTextResources.Add("MVCGrid.js", script);

                _cachedBinaryResources.Add(
                    "ajaxloader.gif",
                    GetBinaryResource("ajaxloader.gif"));
                _cachedBinaryResources.Add(
                    "sort.png",
                    GetBinaryResource("sort.png"));
                _cachedBinaryResources.Add(
                    "sortdown.png",
                    GetBinaryResource("sortdown.png"));
                _cachedBinaryResources.Add(
                    "sortup.png",
                    GetBinaryResource("sortup.png"));

                _init = true;
            }
        }

        private static byte[] GetBinaryResource(string fileSuffix)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var s = assembly.GetManifestResourceNames();

            string resourceName = null;
            foreach (var name in s)
            {
                if (name.Contains(fileSuffix))
                {
                    resourceName = name;
                    break;
                }
            }

            using (var resFilestream =
                assembly.GetManifestResourceStream(resourceName))
            {
                if (resFilestream == null)
                {
                    return null;
                }

                var ba = new byte[resFilestream.Length];
                resFilestream.Read(ba, 0, ba.Length);
                return ba;
            }
        }

        private static string GetTextResource(string fileSuffix)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var s = assembly.GetManifestResourceNames();

            var resourceName =
                s.FirstOrDefault(name => name.Contains(fileSuffix));

            string script;

            if (resourceName == null)
            {
                return null;
            }

            using (var stream =
                assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    return null;
                }

                using (var textStreamReader = new StreamReader(stream))
                {
                    script = textStreamReader.ReadToEnd();
                }
            }


            return script;
        }

        private static void HandelGifImage(
            HttpContext context,
            string imageName)
        {
            context.Response.Clear();
            context.Response.ContentType = "image/gif";
            context.Response.BinaryWrite(_cachedBinaryResources[imageName]);
            context.Response.Flush();
        }

        private static void HandelPngImage(
            HttpContext context,
            string imageName)
        {
            context.Response.Clear();
            context.Response.ContentType = "image/png";
            context.Response.BinaryWrite(_cachedBinaryResources[imageName]);
            context.Response.Flush();
        }

        private static void HandleScript(HttpContext context)
        {
            context.Response.ContentType = "application/javascript";
            context.Response.Write(_cachedTextResources["MVCGrid.js"]);
        }


        private static void HandleTable(HttpContext context)
        {
            var gridName = context.Request["Name"];

            //StringBuilder sbDebug = new StringBuilder();
            //foreach (string key in context.Request.QueryString.AllKeys)
            //{
            //    sbDebug.Append(key);
            //    sbDebug.Append(" = ");
            //    sbDebug.Append(context.Request.QueryString[key]);
            //    sbDebug.Append("<br />");
            //}

            var grid = MvcGridDefinitionTable.GetDefinitionInterface(gridName);

            var options = QueryStringParser.ParseOptions(grid, context.Request);

            var gridContext =
                GridContextUtility.Create(context, gridName, grid, options);

            var engine = new GridEngine();
            if (!engine.CheckAuthorization(gridContext))
            {
                //Forbidden
                context.Response.StatusCode = 403;
                context.Response.End();
                return;
            }

            var renderingEngine = GridEngine.GetRenderingEngine(gridContext);

            renderingEngine.PrepareResponse(context.Response);
            engine.Run(renderingEngine, gridContext, context.Response.Output);
        }
    }
}