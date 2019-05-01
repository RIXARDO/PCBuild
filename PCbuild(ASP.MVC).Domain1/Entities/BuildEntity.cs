using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCbuild_ASP.MVC_.Domain.Entities
{
    public partial class BuildEntity
    {
        public int BuildEntityID { get; set; }

        public int CPUID { get; set; }
        public virtual CPU CPU { get; set; }

        public int GPUID { get; set; }
        public virtual GPU GPU { get; set; }

        public string UserID{ get;set; }

    }
}