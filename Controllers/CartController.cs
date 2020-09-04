using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelWebsite.Models;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace TravelWebsite.Controllers
{
    public class CartController : Controller
    {
        cs c = new cs();
        StringBuilder sb = new StringBuilder();
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
            StringBuilder sb = new StringBuilder("Thank you for using our services\n", 255);
            StripeConfiguration.ApiKey = "sk_test_51HHZalIRpf7rAmeBgc0q7aD4yiOcIaPjGCZ60FvMO4Yje4RnstURkwhMYOILHmZJwYHTzhq02OdsQDs1oP3ERsIS00k3aejALI";
            var x = c.Packages.Where(e => e.ID == ID);
            foreach (var item in x)
            {
                price = item.Price;
                sb.AppendFormat("NT Travel Guide xin cảm ơn quý khách đã sử dụng dịch vụ của chúng tôi. " +
                    "Quý khách đã thanh toán thành công. Xin vui lòng kiểm tra lại các thông tin bên dưới: \n" +
                    "Điểm đến: {0}\n" +
                    "Ngày xuất phát: {3}-{2}-{1}\n" +
                    "Chuyến đi trong vòng: {4}\n" +
                    "Mã vé: NT{5}\n\n" +
                    "Trân trọng,\nNT Travel Guide\n" +
                    "Nếu những thông tin trên có sai sót hoặc có thắc mắc về chuyến đi, xin phản hồi lại email này.\n" +
                    "\nHoặc gọi vào hotline: 028-38364748 để được hỗ trợ tốt nhất.\n" ,item.Destination, item.Depart.Year, item.Depart.Month, item.Depart.Day, item.Offer, item.ID);
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
            mail.Subject = "[MAIL XÁC NHẬN]";

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(mail.From.ToString(), "daylamatkhaumanhduocchua?");
            smtp.Host = "smtp.gmail.com";

            // Recipient address
            mail.To.Add(new MailAddress(customer.Email));

            // Formatted mail body;
            string st = "Xin chào anh/chị " + customer.Description + ", \n"+ sb;

            mail.Body = st;
            smtp.Send(mail);

            return View("OrderStatus", model);
        }

    }
}