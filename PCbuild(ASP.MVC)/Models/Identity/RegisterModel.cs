using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PCbuild_ASP.MVC_.Models.Identity
{
    public class RegisterModel
    {
        [Required]
        [StringLength(16,MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Year { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password",ErrorMessage ="Password not same")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}