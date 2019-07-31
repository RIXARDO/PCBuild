using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuilder_ASP.MVC_.Services.DTO
{
    public enum Currency { UAH, EUR, USD, RUB };

    public partial class PriceDTO
    {
        public Guid PriceGuid { get; set; }    
        public string Vendor { get; set; }
        public Currency Сurrency { get; set; }
        public double Amount { get; set; }
    }
}