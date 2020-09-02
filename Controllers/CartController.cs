using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelWebsite.Models;
using System.Net;
using System.Net.Mail;

namespace TravelWebsite.Controllers
{
    public class CartController : Controller
    {
        cs c = new cs();

        // GET: Cart/Create
        public ActionResult Create(int ID)
        {
            var x = c.Packages.Where(e => e.ID == ID);
            return View(x);
        }

        // POST: Cart/Create
        [HttpPost]
        public ActionResult Create(ChargeDTO chargeDTO, int ID)
        {
            double price = 0;
            StripeConfiguration.ApiKey = "sk_test_51HHZalIRpf7rAmeBgc0q7aD4yiOcIaPjGCZ60FvMO4Yje4RnstURkwhMYOILHmZJwYHTzhq02OdsQDs1oP3ERsIS00k3aejALI";
            var x = c.Packages.Where(e => e.ID == ID);
            foreach (var item in x)
            {
                price = item.Price;
            }
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
                Amount = (long)price,
                Currency = "usd",
                Description = "Charge for vtmn1212@gmail.com",
                Customer = customer.Id
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);

            var model = new ChargeViewModel();
            model.ChargeId = charge.Id;
            model.Name = customer.Name;
            model.Email = customer.Email;
            // Send mail for client
            MailMessage mail = new MailMessage();
            mail.From = new System.Net.Mail.MailAddress("demotravelguide@gmail.com");

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(mail.From.ToString(), "strongpassword");
            smtp.Host = "smtp.gmail.com";

            // Recipient address
            mail.To.Add(new MailAddress(customer.Email));

            // Formatted mail body;
            string st = "Booking Successfully. Thank you for choosing our service! Have a nice day";

            mail.Body = st;
            smtp.Send(mail);

            return View("OrderStatus", model);
        }

    }
}