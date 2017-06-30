using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MichaelBrandonMorris.MvcGrid.Interfaces;
using MichaelBrandonMorris.MvcGrid.Models;

namespace MichaelBrandonMorris.MvcGrid.Utility
{
    /// <summary>
    ///     Class GridContextUtility.
    /// </summary>
    /// TODO Edit XML Comment Template for GridContextUtility
    public class GridContextUtility
    {
        /// <summary>
        ///     Creates the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="gridName">Name of the grid.</param>
        /// <param name="grid">The grid.</param>
        /// <param name="options">The options.</param>
        /// <returns>GridContext.</returns>
        /// TODO Edit XML Comment Template for Create
        internal static GridContext Create(
            HttpContext context,
            string gridName,
            IMvcGridDefinition grid,
            QueryOptions options)
        {
            var httpContext = new HttpContextWrapper(context);
            var urlHelper =
                new UrlHelper(new RequestContext(httpContext, new RouteData()));

            var gridContext = new GridContext
            {
                GridName = gridName,
                CurrentHttpContext = context,
                GridDefinition = grid,
                QueryOptions = options,
                UrlHelper = urlHelper
            };

            return gridContext;
        }
    }
}