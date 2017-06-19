using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MichaelBrandonMorris.MvcGrid.Web;

namespace MVCGrid.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MvcGridConfig.RegisterGrids();
            GridRegistration.RegisterAllGrids();
        }
    }
}