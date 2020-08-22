using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelWebsite.Models;

namespace TravelWebsite.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
        [HttpPost]
        public ActionResult Create(ChargeDTO chargeDTO)
        {
            StripeConfiguration.ApiKey = "sk_test_51HHZalIRpf7rAmeBgc0q7aD4yiOcIaPjGCZ60FvMO4Yje4RnstURkwhMYOILHmZJwYHTzhq02OdsQDs1oP3ERsIS00k3aejALI";

            var customerOptions = new CustomerCreateOptions
            {
                Description = chargeDTO.CardName,
                Source = chargeDTO.StripeToken,
                Email = chargeDTO.Email,
                Metadata = new Dictionary<String, String>()
                {
                    {"Phone Number", chargeDTO.Phone }
                }
            };
            var customerService = new CustomerService();
            Customer customer = customerService.Create(customerOptions);

            var options = new ChargeCreateOptions
            {
                Amount = 2000,
                Currency = "usd",
                Description = "Charge for vtmn1212@gmail.com",
                Customer = customer.Id
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);

            var model = new ChargeViewModel();
            model.ChargeId = charge.Id;

            return View("OrderStatus", model);
        }
    }
}