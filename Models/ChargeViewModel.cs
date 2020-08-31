using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelWebsite.Models
{
    public class ChargeViewModel
    {
        public string ChargeId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int TourID { get; set; }
    }
}