using System.Web;
using System.Web.Routing;
using MvcGrid.Interfaces;
using MvcGrid.Models;

namespace MvcGrid.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class GridContextUtility
    {
        internal static GridContext Create(
            HttpContext context,
            string gridName,
            IMvcGridDefinition grid,
            QueryOptions options)
        {
            var httpContext = new HttpContextWrapper(context);
            var urlHelper = new System.Web.Mvc.UrlHelper(
                new RequestContext(httpContext, new RouteData()));

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