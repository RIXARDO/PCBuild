using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PCbuilder_ASP.MVC_.Models.Identity
{
    public class ApplicationRole: IdentityRole
    {
        public ApplicationRole(){}

        public string Description { get; set; }
    }
}