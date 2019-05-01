using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PCbuild_ASP.MVC_.Domain.Entities
{
    [MetadataType(typeof(CPUMetadata))]
    public partial class CPU
    {
        /// <summary>
        /// Id
        /// </summary>
        [HiddenInput (DisplayValue =false)]
        public int CPUID { get; set; }

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

        public virtual ICollection<PriceCPU> PriceCPUs { get; set; }

        public int AverangeBench { get; set; }

        public virtual ICollection<BuildEntity> BuildEntities { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                CPU cpu = (CPU)obj;
                return (this.CPUID == cpu.CPUID);
            }
        }

        public override int GetHashCode()
        {
           return CPUID;
        }
    }
}