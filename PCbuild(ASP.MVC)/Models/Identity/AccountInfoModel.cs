using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PCbuild_ASP.MVC_.Models.Identity
{
    public class AccountInfoModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime Year { get; set; }
    }
}