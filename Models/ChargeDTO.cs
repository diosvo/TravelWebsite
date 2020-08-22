using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelWebsite.Models
{
    public class ChargeDTO
    {
        public string CardName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string StripeToken { get; set; }
    }
}