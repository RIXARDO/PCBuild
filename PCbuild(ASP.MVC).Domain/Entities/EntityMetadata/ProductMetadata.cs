using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuild_ASP.MVC_.Domain.Entities.EntityMetadata
{
    public partial class ProductMetadata
    {
        public Guid ProductGuid { get; set; }
    }
}
