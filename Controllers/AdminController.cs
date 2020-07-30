using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelWebsite.Models;

namespace TravelWebsite.Controllers
{
    public class AdminController : Controller
    {
        cs c = new cs();

        /* Admin */
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
                return View("AddPackages");
            }
            else
            {
                ViewBag.m = "Wrong Email or Password !!!";
                return View();
            }
        }

        /* Sign Up - Registration (Reg) */
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

        /* Packages */
        [HttpGet]
        public ActionResult Packages()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPackages(Package p, string Offers, HttpPostedFileBase image)
        {
            string picture = Path.Combine(Server.MapPath("~/Images"), image.FileName);
            image.SaveAs(picture);
            var pkg = c.Packages.Where(e => e.Offer == Offers);
            p.Image = picture;
            p.Offer = Offers;
            c.Packages.Add(p);
            c.SaveChanges();
            ViewBag.pk = "Package added successfully !";
            return View();
        }

        /* Services */
        [HttpGet]
        public ActionResult Services()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddServices(Services services, string Services, HttpPostedFileBase image)
        {
            string picture = Path.Combine(Server.MapPath("~/Images"), image.FileName);
            image.SaveAs(picture);
            services.Image = picture;
            services.Service = Services;
            c.Services.Add(services);
            c.SaveChanges();
            ViewBag.sv = "Servicess added successfully";
            return View();
        }
    }
}