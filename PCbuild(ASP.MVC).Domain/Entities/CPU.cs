using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCbuild_ASP.MVC_.Domain.Entities
{
    [MetadataType(typeof(CPUMetadata))]
    [Table("CPUs")]
    public partial class CPU:Product
    {

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
                return (this.ProductID == cpu.ProductID);
            }
        }

        public override int GetHashCode()
        {
           return ProductID.GetHashCode();
        }
    }
}