using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuild_ASP.MVC_.Domain.Entities
{
    public class PriceGPU: Price
    {
        public virtual GPU GPU { get; set; }
    }
}