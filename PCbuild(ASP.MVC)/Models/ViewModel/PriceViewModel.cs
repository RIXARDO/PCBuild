using PCbuild_ASP.MVC_.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuild_ASP.MVC_.Models.ViewModel
{
    public class PriceViewModel
    {
        public Guid PriceGuid { get; set; }
        public string Vendor { get; set; }
        public Currency Сurrency { get; set; }
        public double Amount { get; set; }
    }
}