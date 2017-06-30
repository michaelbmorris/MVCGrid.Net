using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.Extensions.PrimitiveExtensions;
using MichaelBrandonMorris.MvcGrid.Interfaces;
using MichaelBrandonMorris.MvcGrid.Models;
using MichaelBrandonMorris.MvcGrid.Utility;
using MichaelBrandonMorris.MvcGrid.Web;

namespace MichaelBrandonMorris.MvcGrid.Engine
{
    /// <summary>
    ///     Class GridEngine.
    /// </summary>
    /// TODO Edit XML Comment Template for GridEngine
    public class GridEngine
    {
        /// <summary>
        ///     The local encoding
        /// </summary>
        /// TODO Edit XML Comment Template for LocalEncoding
        private static readonly Encoding LocalEncoding = Encoding.UTF8;

        /// <summary>
        ///     Gets the rendering engine.
        /// </summary>
        /// <param name="gridContext">The grid context.</param>
        /// <returns>IMvcGridRenderingEngine.</returns>
        /// TODO Edit XML Comment Template for GetRenderingEngine
        public static IMvcGridRenderingEngine GetRenderingEngine(
            GridContext gridContext)
        {
            IMvcGridRenderingEngine renderingEngine = null;

            if (string.IsNullOrWhiteSpace(
                gridContext.QueryOptions.RenderingEngineName))
            {
                return GetRenderingEngineInternal(gridContext.GridDefinition);
            }

            foreach (ProviderSettings configuredEngine in gridContext
                .GridDefinition.RenderingEngines)
            {
                if (gridContext.QueryOptions.RenderingEngineName
                    .EqualsOrdinalIgnoreCase(configuredEngine.Name))
                {
                    continue;
                }

                var engineName = gridContext.QueryOptions.RenderingEngineName;

                var typeString = gridContext.GridDefinition
                    .RenderingEngines[engineName]
                    .Type;

                var engineType = Type.GetType(typeString, true);

                renderingEngine =
                    (IMvcGridRenderingEngine) Activator.CreateInstance(
                        engineType,
                        true);
            }

            return renderingEngine
                   ?? GetRenderingEngineInternal(gridContext.GridDefinition);
        }

        /// <summary>
        ///     Checks the authorization.
        /// </summary>
        /// <param name="gridContext">The grid context.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="Exception">Unsupported AuthorizationType</exception>
        /// TODO Edit XML Comment Template for CheckAuthorization
        public bool CheckAuthorization(GridContext gridContext)
        {
            bool allowAccess;

            switch (gridContext.GridDefinition.AuthorizationType)
            {
                case AuthorizationType.AllowAnonymous:
                    allowAccess = true;
                    break;
                case AuthorizationType.Authorized:
                    allowAccess = gridContext.CurrentHttpContext.User.Identity
                        .IsAuthenticated;
                    break;
                default: throw new Exception("Unsupported AuthorizationType");
            }

            return allowAccess;
        }

        /// <summary>
        ///     Generates the model.
        /// </summary>
        /// <param name="gridContext">The grid context.</param>
        /// <returns>RenderingModel.</returns>
        /// TODO Edit XML Comment Template for GenerateModel
        public RenderingModel GenerateModel(GridContext gridContext)
        {
            var rows =
                ((GridDefinitionBase) gridContext.GridDefinition).GetData(
                    gridContext,
                    out int? totalRecords);

            if (rows.Count == 0
                && totalRecords.HasValue
                && totalRecords.Value > 0)
            {
                gridContext.QueryOptions.PageIndex = 0;
                rows =
                    ((GridDefinitionBase) gridContext.GridDefinition).GetData(
                        gridContext,
                        out totalRecords);
            }

            var model = PrepModel(totalRecords, rows, gridContext);
            return model;
        }

        /// <summary>
        ///     Gets the base page HTML.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="gridName">Name of the grid.</param>
        /// <param name="grid">The grid.</param>
        /// <param name="pageParameters">The page parameters.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for GetBasePageHtml
        public string GetBasePageHtml(
            HtmlHelper helper,
            string gridName,
            IMvcGridDefinition grid,
            object pageParameters)
        {
            var preload = "";
            if (grid.QueryOnPageLoad
                && grid.PreloadData)
            {
                try
                {
                    preload = RenderPreloadedGridHtml(
                        helper,
                        grid,
                        gridName,
                        pageParameters);
                }
                catch (Exception ex)
                {
                    var showDetails = ConfigUtility
                        .GetShowErrorDetailsSetting();

                    if (showDetails)
                    {
                        var detail = "<div class='alert alert-danger'>";

                        detail += HttpUtility.HtmlEncode(ex.ToString())
                            .Replace("\r\n", "<br />");

                        detail += "</div>";
                        preload = detail;
                    }
                    else
                    {
                        preload = grid.ErrorMessageHtml;
                    }
                }
            }

            var baseGridHtml =
                MvcGridHtmlGenerator.GenerateBasePageHtml(
                    gridName,
                    grid,
                    pageParameters);

            baseGridHtml = baseGridHtml.Replace("%%PRELOAD%%", preload);

            var containerRenderingModel = new ContainerRenderingModel
            {
                InnerHtmlBlock = baseGridHtml
            };

            var html =
                RenderContainerHtml(helper, grid, containerRenderingModel);

            return html;
        }

        /// <summary>
        ///     Runs the specified rendering engine.
        /// </summary>
        /// <param name="renderingEngine">The rendering engine.</param>
        /// <param name="gridContext">The grid context.</param>
        /// <param name="outputStream">The output stream.</param>
        /// TODO Edit XML Comment Template for Run
        public void Run(
            IMvcGridRenderingEngine renderingEngine,
            GridContext gridContext,
            TextWriter outputStream)
        {
            if (!renderingEngine.AllowsPaging)
            {
                gridContext.QueryOptions.ItemsPerPage = null;
            }

            var model = GenerateModel(gridContext);

            renderingEngine.Render(model, gridContext, outputStream);
        }

        /// <summary>
        ///     Gets the rendering engine internal.
        /// </summary>
        /// <param name="gridDefinition">The grid definition.</param>
        /// <returns>IMvcGridRenderingEngine.</returns>
        /// <exception cref="Exception"></exception>
        /// TODO Edit XML Comment Template for GetRenderingEngineInternal
        internal static IMvcGridRenderingEngine GetRenderingEngineInternal(
            IMvcGridDefinition gridDefinition)
        {
            var engineName = gridDefinition.DefaultRenderingEngineName;

            if (gridDefinition.RenderingEngines[engineName] == null)
            {
                throw new Exception(
                    $"The requested default rendering engine '{engineName}' was not found.");
            }

            var typeString = gridDefinition.RenderingEngines[engineName].Type;
            var engineType = Type.GetType(typeString, true);

            var renderingEngine =
                (IMvcGridRenderingEngine) Activator.CreateInstance(
                    engineType,
                    true);

            return renderingEngine;
        }

        /// <summary>
        ///     Preps the columns.
        /// </summary>
        /// <param name="gridContext">The grid context.</param>
        /// <param name="model">The model.</param>
        /// TODO Edit XML Comment Template for PrepColumns
        private static void PrepColumns(
            GridContext gridContext,
            RenderingModel model)
        {
            foreach (var col in gridContext.GetVisibleColumns())
            {
                var renderingColumn = new Column();
                model.Columns.Add(renderingColumn);
                renderingColumn.Name = col.ColumnName;
                renderingColumn.HeaderText = col.HeaderText;

                if (!gridContext.GridDefinition.Sorting
                    || !col.EnableSorting)
                {
                    continue;
                }

                SortDirection linkDirection;
                SortDirection iconDirection;

                if (gridContext.QueryOptions.SortColumnName == col.ColumnName
                    && gridContext.QueryOptions.SortDirection
                    == SortDirection.Asc)
                {
                    iconDirection = SortDirection.Asc;
                    linkDirection = SortDirection.Dsc;
                }
                else if (gridContext.QueryOptions.SortColumnName
                         == col.ColumnName
                         && gridContext.QueryOptions.SortDirection
                         == SortDirection.Dsc)
                {
                    iconDirection = SortDirection.Dsc;
                    linkDirection = SortDirection.Asc;
                }
                else
                {
                    iconDirection = SortDirection.Unspecified;
                    linkDirection = SortDirection.Asc;
                }

                renderingColumn.Onclick = HtmlUtility.MakeSortLink(
                    gridContext.GridName,
                    col.ColumnName,
                    linkDirection);
                renderingColumn.SortIconDirection = iconDirection;
            }
        }

        /// <summary>
        ///     Renders the container HTML.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="grid">The grid.</param>
        /// <param name="containerRenderingModel">
        ///     The container
        ///     rendering model.
        /// </param>
        /// <returns>System.String.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception">
        ///     When rendering a container, you must output
        ///     Model.InnerHtmlBlock inside the container (Raw).
        /// </exception>
        /// TODO Edit XML Comment Template for RenderContainerHtml
        private static string RenderContainerHtml(
            HtmlHelper helper,
            IMvcGridDefinition grid,
            ContainerRenderingModel containerRenderingModel)
        {
            var container = containerRenderingModel.InnerHtmlBlock;

            switch (grid.RenderingMode)
            {
                case RenderingMode.RenderingEngine:
                    GetRenderingEngineInternal(grid);
                    container = RenderContainerUsingRenderingEngine(
                        grid,
                        containerRenderingModel);
                    break;
                case RenderingMode.Controller:
                    if (!string.IsNullOrWhiteSpace(grid.ContainerViewPath))
                    {
                        container = RenderContainerUsingController(
                            grid,
                            helper,
                            containerRenderingModel);
                    }
                    break;
                default: throw new InvalidOperationException();
            }

            if (!container.Contains(containerRenderingModel.InnerHtmlBlock))
            {
                throw new Exception(
                    "When rendering a container, you must output Model.InnerHtmlBlock inside the container (Raw).");
            }

            return container;
        }

        /// <summary>
        ///     Renders the container using controller.
        /// </summary>
        /// <param name="gridDefinition">The grid definition.</param>
        /// <param name="helper">The helper.</param>
        /// <param name="model">The model.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for RenderContainerUsingController
        private static string RenderContainerUsingController(
            IMvcGridDefinition gridDefinition,
            HtmlHelper helper,
            ContainerRenderingModel model)
        {
            var controllerContext = helper.ViewContext.Controller
                .ControllerContext;

            var vdd = new ViewDataDictionary(model);
            var tdd = new TempDataDictionary();

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(
                    controllerContext,
                    gridDefinition.ContainerViewPath);

                var viewContext = new ViewContext(
                    controllerContext,
                    viewResult.View,
                    vdd,
                    tdd,
                    sw);

                viewResult.View.Render(viewContext, sw);

                viewResult.ViewEngine.ReleaseView(
                    controllerContext,
                    viewResult.View);

                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        ///     Renders the container using rendering engine.
        /// </summary>
        /// <param name="gridDefinition">The grid definition.</param>
        /// <param name="model">The model.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for RenderContainerUsingRenderingEngine
        private static string RenderContainerUsingRenderingEngine(
            IMvcGridDefinition gridDefinition,
            ContainerRenderingModel model)
        {
            var renderingEngine = GetRenderingEngineInternal(gridDefinition);

            using (var ms = new MemoryStream())
            {
                using (TextWriter tw = new StreamWriter(ms))
                {
                    renderingEngine.RenderContainer(model, tw);
                }

                return LocalEncoding.GetString(ms.ToArray());
            }
        }

        /// <summary>
        ///     Renders the preloaded grid HTML.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="grid">The grid.</param>
        /// <param name="gridName">Name of the grid.</param>
        /// <param name="pageParameters">The page parameters.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// TODO Edit XML Comment Template for RenderPreloadedGridHtml
        private static string RenderPreloadedGridHtml(
            HtmlHelper helper,
            IMvcGridDefinition grid,
            string gridName,
            object pageParameters)
        {
            string preload;

            var options =
                QueryStringParser.ParseOptions(
                    grid,
                    HttpContext.Current.Request);

            var pageParamsDict =
                new Dictionary<string, string>(
                    StringComparer.OrdinalIgnoreCase);

            if (pageParameters != null)
            {
                foreach (PropertyDescriptor descriptor in TypeDescriptor
                    .GetProperties(pageParameters))
                {
                    var obj2 = descriptor.GetValue(pageParameters);

                    if (obj2 != null)
                    {
                        pageParamsDict.Add(descriptor.Name, obj2.ToString());
                    }
                }
            }

            if (grid.PageParameterNames.Count > 0)
            {
                foreach (var aqon in grid.PageParameterNames)
                {
                    var val = "";

                    if (pageParamsDict.ContainsKey(aqon))
                    {
                        val = pageParamsDict[aqon];
                    }

                    options.PageParameters[aqon] = val;
                }
            }

            var gridContext = GridContextUtility.Create(
                HttpContext.Current,
                gridName,
                grid,
                options);

            var engine = new GridEngine();

            switch (grid.RenderingMode)
            {
                case RenderingMode.RenderingEngine:
                    preload = RenderUsingRenderingEngine(engine, gridContext);
                    break;
                case RenderingMode.Controller:
                    preload =
                        RenderUsingController(engine, gridContext, helper);
                    break;
                default: throw new InvalidOperationException();
            }

            return preload;
        }

        /// <summary>
        ///     Renders the using controller.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="gridContext">The grid context.</param>
        /// <param name="helper">The helper.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for RenderUsingController
        private static string RenderUsingController(
            GridEngine engine,
            GridContext gridContext,
            HtmlHelper helper)
        {
            var model = engine.GenerateModel(gridContext);

            var controllerContext = helper.ViewContext.Controller
                .ControllerContext;

            var vdd = new ViewDataDictionary(model);
            var tdd = new TempDataDictionary();

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(
                    controllerContext,
                    gridContext.GridDefinition.ViewPath);

                var viewContext = new ViewContext(
                    controllerContext,
                    viewResult.View,
                    vdd,
                    tdd,
                    sw);

                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(
                    controllerContext,
                    viewResult.View);

                return sw.GetStringBuilder().ToString();
            }
        }

        /// <summary>
        ///     Renders the using rendering engine.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="gridContext">The grid context.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for RenderUsingRenderingEngine
        private static string RenderUsingRenderingEngine(
            GridEngine engine,
            GridContext gridContext)
        {
            var renderingEngine = GetRenderingEngine(gridContext);

            using (var ms = new MemoryStream())
            {
                using (TextWriter tw = new StreamWriter(ms))
                {
                    engine.Run(renderingEngine, gridContext, tw);
                }

                var result = LocalEncoding.GetString(ms.ToArray());
                return result;
            }
        }

        /// <summary>
        ///     Preps the model.
        /// </summary>
        /// <param name="totalRecords">The total records.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="gridContext">The grid context.</param>
        /// <returns>RenderingModel.</returns>
        /// TODO Edit XML Comment Template for PrepModel
        private static RenderingModel PrepModel(
            int? totalRecords,
            List<Row> rows,
            GridContext gridContext)
        {
            var model = new RenderingModel
            {
                HandlerPath = HtmlUtility.GetHandlerPath(),
                TableHtmlId = HtmlUtility.GetTableHtmlId(gridContext.GridName)
            };

            PrepColumns(gridContext, model);
            model.Rows = rows;

            if (model.Rows.Count == 0)
            {
                model.NoResultsMessage =
                    gridContext.GridDefinition.NoResultsMessage;
            }

            model.NextButtonCaption = gridContext.GridDefinition
                .NextButtonCaption;

            model.PreviousButtonCaption = gridContext.GridDefinition
                .PreviousButtonCaption;

            model.SummaryMessage = gridContext.GridDefinition.SummaryMessage;

            model.ProcessingMessage = gridContext.GridDefinition
                .ProcessingMessage;

            model.PagingModel = null;

            if (gridContext.QueryOptions.ItemsPerPage.HasValue)
            {
                model.PagingModel = new PagingModel();

                if (gridContext.QueryOptions.PageIndex != null)
                {
                    var currentPageIndex =
                        gridContext.QueryOptions.PageIndex.Value;

                    if (totalRecords != null)
                    {
                        model.PagingModel.TotalRecords = totalRecords.Value;
                    }

                    model.PagingModel.FirstRecord =
                        currentPageIndex
                        * gridContext.QueryOptions.ItemsPerPage.Value
                        + 1;

                    if (model.PagingModel.FirstRecord
                        > model.PagingModel.TotalRecords)
                    {
                        model.PagingModel.FirstRecord =
                            model.PagingModel.TotalRecords;
                    }

                    model.PagingModel.LastRecord =
                        model.PagingModel.FirstRecord
                        + gridContext.QueryOptions.ItemsPerPage.Value
                        - 1;

                    if (model.PagingModel.LastRecord
                        > model.PagingModel.TotalRecords)
                    {
                        model.PagingModel.LastRecord =
                            model.PagingModel.TotalRecords;
                    }

                    model.PagingModel.CurrentPage = currentPageIndex + 1;
                }

                var numberOfPagesD = (model.PagingModel.TotalRecords + 0.0)
                                     / (gridContext.QueryOptions.ItemsPerPage
                                            .Value
                                        + 0.0);

                model.PagingModel.NumberOfPages =
                    (int) Math.Ceiling(numberOfPagesD);

                for (var i = 1; i <= model.PagingModel.NumberOfPages; i++)
                {
                    model.PagingModel.PageLinks.Add(
                        i,
                        HtmlUtility.MakeGotoPageLink(gridContext.GridName, i));
                }
            }

            model.ClientDataTransferHtmlBlock = MvcGridHtmlGenerator
                .GenerateClientDataTransferHtml(gridContext);

            return model;
        }
    }
}