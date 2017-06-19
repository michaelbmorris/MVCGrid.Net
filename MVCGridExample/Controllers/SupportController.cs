using System.Web.Mvc;

namespace MVCGrid.Web.Controllers
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