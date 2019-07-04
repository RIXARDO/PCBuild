using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuild_ASP.MVC_.Services.DTO
{
    public partial class GPUdto
    {
        public Guid ProductGuid { get; set; }

        public string Status { get; set; }

        public string Manufacture { get; set; }

        public string Developer { get; set; }

        public string Name { get; set; }

        public string Architecture { get; set; }

        public int BoostClock { get; set; }

        public int FrameBuffer { get; set; }

        public int MemorySpeed { get; set; }

        public int AverageBench { get; set; }

        public virtual PriceDTO Price { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                GPUdto gpu = (GPUdto)obj;
                return (this.ProductGuid == gpu.ProductGuid);
            }
        }

        public override int GetHashCode()
        {
            return ProductGuid.GetHashCode();
        }
    }
}