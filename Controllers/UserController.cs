using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelWebsite.Models;

namespace TravelWebsite.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        cs c = new cs();
        public ActionResult Index()
        {
            return View();
        }
        //Services
        public ActionResult Services()
        {
            var x = c.Services.ToList();
            return View(x);
        }
        //Packages
        public ActionResult Packages()
        {
            var x = c.Packages .ToList();
            return View(x);
        }
        [HttpPost]
        public ActionResult Packages(string Destination)
        {
            var res = c.Packages.Where(e => e.Destination == Destination);
            return View("Packages", res);
        }
    }
}   