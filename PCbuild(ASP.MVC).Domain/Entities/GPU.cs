using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCbuilder_ASP.MVC_.Domain.Entities
{
    [Table("GPUs")]
    [MetadataType(typeof(GPUMetadata))]
    public partial class GPU: Product
    {

        public string Manufacture { get; set; }

        public string Developer { get; set; }

        public string Name { get; set; }

        public string Architecture { get; set; }

        public int BoostClock { get; set; }

        public int FrameBuffer { get; set; }

        public int MemorySpeed { get; set; }

        public int AverageBench { get; set; }

        public virtual ICollection<BuildEntity> BuildEntities { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                GPU gpu = (GPU)obj;
                return (this.ProductGuid == gpu.ProductGuid);
            }
        }

        public override int GetHashCode()
        {
            return ProductGuid.GetHashCode();
        }
    }
}