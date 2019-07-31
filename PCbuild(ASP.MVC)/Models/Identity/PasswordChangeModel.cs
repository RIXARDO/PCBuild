using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PCbuilder_ASP.MVC_.Models.Identity
{
    public class PasswordChangeModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword",ErrorMessage ="Password nor same")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}