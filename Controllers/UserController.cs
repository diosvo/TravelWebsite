using System.Web.Mvc;

namespace TravelWebsite.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}