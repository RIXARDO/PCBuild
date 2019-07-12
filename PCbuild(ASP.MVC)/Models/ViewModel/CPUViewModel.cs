using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuild_ASP.MVC_.Models.ViewModel
{
    public class CPUViewModel
    {
        public Guid ProductGuid { get; set; }

        public string Status { get; set; }

        public string Manufacture { get; set; }

        public string ProcessorNumber { get; set; }

        public int NumberOfCores { get; set; }

        public int NumberOfThreads { get; set; }

        /// <summary>
        /// Processor Base Frequency(GHz)
        /// </summary>
        public float PBF { get; set; }

        public string Cache { get; set; }

        public string TDP { get; set; }

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
                CPUViewModel cpu = (CPUViewModel)obj;
                return (this.ProductGuid == cpu.ProductGuid);
            }
        }

        public override int GetHashCode()
        {
            return ProductGuid.GetHashCode();
        }
    }
}