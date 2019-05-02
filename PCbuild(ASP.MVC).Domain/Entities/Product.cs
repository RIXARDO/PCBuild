using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCbuild_ASP.MVC_.Domain.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; }

        public virtual Price Price { get; set; }
        public string Status { get; set; }
    }
}
