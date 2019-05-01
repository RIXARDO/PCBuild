using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace PCbuild_ASP.MVC_.Models.Identity
{
    public class ApplicationContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext():base("EFDbContext") {}

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}