using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuilder_ASP.MVC_.Domain.Entities
{
    public class BuildEntityMetadata
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BuildEntityGuid { get; set; }

    }
}
