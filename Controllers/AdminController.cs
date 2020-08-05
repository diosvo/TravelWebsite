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

        #region  /* Sign Up - Registration (Reg) */
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
        #endregion

        #region /* Packages */
        [HttpGet]
        public ActionResult PackagesList()
        {
            var x = c.Packages.ToList();
            return View(x);
        }

        [HttpGet]
        public ActionResult AddPackages()
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
        #endregion

        #region /* Services */
        [HttpGet]
        public ActionResult ServicesList()
        {
            var x = c.Services.ToList();
            return View(x);
        }

        [HttpGet]
        public ActionResult Services()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Services(Services ser, string service, HttpPostedFileBase image)
        {
            string picture = Path.Combine(Server.MapPath("~/Images"), image.FileName);
            image.SaveAs(picture);
            var pkg = c.Services.Where(e => e.Service == service);
            ser.Image = picture;
            ser.Service = service;
            c.Services.Add(ser);
            c.SaveChanges();
            ViewBag.sv = "Services added successfully !";
            return View();
        }
        #endregion

        #region /* User Profile */
        [HttpGet]
        public ActionResult UserProfile()
        {
            var x = c.Logins.ToList();
            return View(x);
        }
        #endregion
    }
}