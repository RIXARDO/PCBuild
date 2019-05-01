using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuild_ASP.MVC_.Domain.Entities
{
    public class PriceCPU: Price
    {
        public virtual CPU CPU { get; set; }
    }
}