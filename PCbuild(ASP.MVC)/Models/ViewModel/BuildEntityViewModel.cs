using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuilder_ASP.MVC_.Models.ViewModel
{
    public class BuildEntityViewModel
    {
        public Guid BuildEntityGuid { get; set; }

        public Guid CPUID { get; set; }
        public virtual CPUViewModel CPU { get; set; }
        public string CPUName { get; set; }

        public Guid GPUID { get; set; }
        public virtual GPUViewModel GPU { get; set; }
        public string GPUName { get; set; }

        public string UserID { get; set; }
    }
}