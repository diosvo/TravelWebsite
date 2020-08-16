using Antlr.Runtime.Misc;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelWebsite.Models;
using System.Web.Helpers;

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
            var hashedPw = Crypto.Hash(login.Password);
            var x = c.Logins.FirstOrDefault(a => a.Email.Equals(login.Email) && a.Password.Equals(hashedPw));
            if (x != null)
            {
                return View("AddPackages");
            }
            else
            {
                ViewBag.m = "Email or Password is incorrect.";
                return View();
            }
        }

        #region  /* Sign Up - Register (Reg) */
        [HttpGet]
        public ActionResult Reg()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Reg(Login login)
        {

            // Check if Email already register
            var registerdEmail = (from c in c.Logins
                                  where c.Email.Equals(login.Email)
                                  select c).SingleOrDefault();
            // Hash Password
            var hashedPw = Crypto.Hash(login.Password);
            login.Password = hashedPw;
            if (registerdEmail != null)
            {
                ViewBag.Error = "This email already registered !";
                return View();
            }
            else
            {
                c.Logins.Add(login);
                c.SaveChanges();
                return View("Index");
            }
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
            return RedirectToAction("PackagesList");
        }

        public ActionResult DeletePackage(int id)
        {
            var res = c.Packages.Where(x => x.ID == id).First();
            c.Packages.Remove(res);
            c.SaveChanges();

            var list = c.Packages.ToList();
            return View("PackagesList", list);
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
        public ActionResult Services(Services ser, string Service, HttpPostedFileBase image)
        {
            string picture = Path.Combine(Server.MapPath("~/Images"), image.FileName);
            image.SaveAs(picture);

            ser.Image = picture;
            ser.Service = Service;

            c.Services.Add(ser);
            c.SaveChanges();
            ViewBag.sv = "Services added successfully !";
            return RedirectToAction("ServicesList");
        }

        public ActionResult DeleteService(int id)
        {
            var res = c.Services.Where(x => x.ID == id).First();
            c.Services.Remove(res);
            c.SaveChanges();

            var list = c.Services.ToList();
            return View("ServicesList", list);
        }
        #endregion

        #region /* User Profile */
        [HttpGet]
        public ActionResult UserProfile()
        {
            var x = c.Logins.ToList();
            return View(x);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(Login login)
        {
            // Check if Email already register
            var registerdEmail = (from c in c.Logins
                                  where c.Email.Equals(login.Email)
                                  select c).SingleOrDefault();
            // Hash Password
            var hashedPw = Crypto.Hash(login.Password);
            login.Password = hashedPw;

            if (registerdEmail != null)
            {
                ViewBag.Error = "This email already registered !";
                return View();
            }
            else
            {
                Login obj = new Login();
                obj.Email = login.Email;
                obj.Password = login.Password;

                c.Logins.Add(obj);
                c.SaveChanges();
                ViewBag.Error = "User added successfully !";
                return RedirectToAction("UserProfile");
            }

        }

        public ActionResult DeleteUser(int id)
        {
            var res = c.Logins.Where(x => x.ID == id).First();
            c.Logins.Remove(res);
            c.SaveChanges();

            var list = c.Logins.ToList();
            return View("UserProfile", list);
        }
        #endregion
    }
}