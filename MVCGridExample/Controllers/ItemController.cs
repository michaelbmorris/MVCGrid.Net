using System.Web.Mvc;

namespace MvcGrid.Web.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Detail(string id)
        {
            return Content("Detail page for item " + id);
        }
    }
}