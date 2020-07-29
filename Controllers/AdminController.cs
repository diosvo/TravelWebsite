using System.Linq;
using System.Web.Mvc;
using TravelWebsite.Models;

namespace TravelWebsite.Controllers
{
    public class AdminController : Controller
    {
        cs c = new cs();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {
            var x = c.Logins.FirstOrDefault(a => a.Email.Equals(login.Email) && a.Password.Equals(login.Password));
            if (x != null)
            {
                return View("AdminDashboard");
            }
            else
            {
                ViewBag.m = "Wrong Email or Password !!!";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Reg()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Reg(Login login)
        {
            c.Logins.Add(login);
            c.SaveChanges();
            return View("Index");
        }
    }
}