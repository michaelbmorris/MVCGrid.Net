using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.MvcGrid.Engine;
using MichaelBrandonMorris.MvcGrid.Utility;

namespace MichaelBrandonMorris.MvcGrid.Web
{
    /// <summary>
    /// Class MvcGridHandler.
    /// </summary>
    /// <seealso cref="System.Web.IHttpHandler" />
    /// TODO Edit XML Comment Template for MvcGridHandler
    public class MvcGridHandler : IHttpHandler
    {
        /// <summary>
        /// The lock
        /// </summary>
        /// TODO Edit XML Comment Template for Lock
        private static readonly object Lock = new object();
        /// <summary>
        /// The cached binary resources
        /// </summary>
        /// TODO Edit XML Comment Template for _cachedBinaryResources
        private static Dictionary<string, byte[]> _cachedBinaryResources;
        /// <summary>
        /// The cached text resources
        /// </summary>
        /// TODO Edit XML Comment Template for _cachedTextResources
        private static Dictionary<string, string> _cachedTextResources;
        /// <summary>
        /// The initialize
        /// </summary>
        /// TODO Edit XML Comment Template for _init
        private static bool _init;

        /// <summary>
        /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
        /// </summary>
        /// <value><c>true</c> if this instance is reusable; otherwise, <c>false</c>.</value>
        /// TODO Edit XML Comment Template for IsReusable
        public bool IsReusable => false;

        /// <summary>
        /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
        /// TODO Edit XML Comment Template for ProcessRequest
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
        /// Initializes this instance.
        /// </summary>
        /// TODO Edit XML Comment Template for Init
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

        /// <summary>
        /// Gets the binary resource.
        /// </summary>
        /// <param name="fileSuffix">The file suffix.</param>
        /// <returns>System.Byte[].</returns>
        /// TODO Edit XML Comment Template for GetBinaryResource
        private static byte[] GetBinaryResource(string fileSuffix)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var s = assembly.GetManifestResourceNames();

            var resourceName = s.FirstOrDefault(name => name.Contains(fileSuffix));

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

        /// <summary>
        /// Gets the text resource.
        /// </summary>
        /// <param name="fileSuffix">The file suffix.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GetTextResource
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

        /// <summary>
        /// Handels the GIF image.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="imageName">Name of the image.</param>
        /// TODO Edit XML Comment Template for HandelGifImage
        private static void HandelGifImage(
            HttpContext context,
            string imageName)
        {
            context.Response.Clear();
            context.Response.ContentType = "image/gif";
            context.Response.BinaryWrite(_cachedBinaryResources[imageName]);
            context.Response.Flush();
        }

        /// <summary>
        /// Handels the PNG image.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="imageName">Name of the image.</param>
        /// TODO Edit XML Comment Template for HandelPngImage
        private static void HandelPngImage(
            HttpContext context,
            string imageName)
        {
            context.Response.Clear();
            context.Response.ContentType = "image/png";
            context.Response.BinaryWrite(_cachedBinaryResources[imageName]);
            context.Response.Flush();
        }

        /// <summary>
        /// Handles the script.
        /// </summary>
        /// <param name="context">The context.</param>
        /// TODO Edit XML Comment Template for HandleScript
        private static void HandleScript(HttpContext context)
        {
            context.Response.ContentType = "application/javascript";
            context.Response.Write(_cachedTextResources["MVCGrid.js"]);
        }


        /// <summary>
        /// Handles the table.
        /// </summary>
        /// <param name="context">The context.</param>
        /// TODO Edit XML Comment Template for HandleTable
        private static void HandleTable(HttpContext context)
        {
            var gridName = context.Request["Name"];
            var grid = MvcGridDefinitionTable.GetDefinitionInterface(gridName);
            var options = QueryStringParser.ParseOptions(grid, context.Request);

            var gridContext =
                GridContextUtility.Create(context, gridName, grid, options);

            var engine = new GridEngine();

            if (!engine.CheckAuthorization(gridContext))
            {
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