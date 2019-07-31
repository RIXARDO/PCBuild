using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuilder_ASP.MVC_.Models.ViewModel
{
    public class GPUViewModel
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

        public virtual PriceViewModel Price { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                GPUViewModel gpu = (GPUViewModel)obj;
                return (this.ProductGuid == gpu.ProductGuid);
            }
        }

        public override int GetHashCode()
        {
            return ProductGuid.GetHashCode();
        }
    }
}