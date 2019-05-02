using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCbuild_ASP.MVC_.Domain.Entities
{
    [MetadataType(typeof(GPUMetadata))]
    [Table("GPUs")]
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
                return (this.ProductID == gpu.ProductID);
            }
        }

        public override int GetHashCode()
        {
            return ProductID.GetHashCode();
        }
    }
}