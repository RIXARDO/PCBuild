using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuilder_ASP.MVC_.Services.DTO
{
    public partial class ProductDTO
    {
        public Guid ProductGuid { get; set; }
        public string Status { get; set; }

        public virtual PriceDTO Price { get; set; }
    }
}
