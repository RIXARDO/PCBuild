using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCbuild_ASP.MVC_.Domain.Entities
{
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product
    { 
        public Guid ProductGuid { get; set; }
        public string Status { get; set; }

        public virtual Price Price { get; set; }
    }
}
