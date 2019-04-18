using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PCbuild_ASP.MVC_.Models.Identity
{
    public class ModificationRoleModel
    {
        [Required]
        public string RoleName { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set;}
        public string Description { get; set; }
    }
}