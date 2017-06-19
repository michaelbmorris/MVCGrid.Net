using System.Web.Mvc;

namespace MvcGrid.Web.Controllers
{
    public class SupportController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}