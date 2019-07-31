using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuilder_ASP.MVC_.Services.DTO
{
    public partial class CPUdto
    {
        public Guid ProductGuid { get; set;}

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
        
        public virtual PriceDTO Price { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                CPUdto cpu = (CPUdto)obj;
                return (this.ProductGuid == cpu.ProductGuid);
            }
        }

        public override int GetHashCode()
        {
           return ProductGuid.GetHashCode();
        }
    }
}