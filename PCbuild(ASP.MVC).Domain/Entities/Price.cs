using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PCbuilder_ASP.MVC_.Domain.Entities
{
    public enum Currency { UAH, EUR, USD, RUB };

    public partial class Price
    {
        [Key]
        public Guid PriceGuid { get; set; }    
        public string Vendor { get; set; }
        public Currency Сurrency { get; set; }
        public double Amount { get; set; }

        [Required]
        public virtual Product Product { get; set; }
    }
}