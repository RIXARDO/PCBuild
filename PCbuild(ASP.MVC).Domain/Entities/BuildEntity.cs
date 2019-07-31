using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PCbuilder_ASP.MVC_.Domain.Entities
{
    [MetadataType(typeof(BuildEntityMetadata))]
    public partial class BuildEntity
    {
        public Guid BuildEntityGuid { get; set; }

        [ForeignKey("CPU")]
        public Guid CPUID { get; set; }
        public virtual CPU CPU { get; set; }

        [ForeignKey("GPU")]
        public Guid GPUID { get; set; }
        public virtual GPU GPU { get; set; }

        public string UserID{ get;set; }

    }
}