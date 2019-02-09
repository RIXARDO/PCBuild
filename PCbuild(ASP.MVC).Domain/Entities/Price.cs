using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuild_ASP.MVC_.Domain.Entities
{
    public class Price
    {
        public int PriceID { get; set; }
        public enum Currency { UAH, EUR, USD, RUB };
        public string Vendor { get; set; }
        public Currency currency { get; set; }
        public float Amount { get; set; }

    }
}