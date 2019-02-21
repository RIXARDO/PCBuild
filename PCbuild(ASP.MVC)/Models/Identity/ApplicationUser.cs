﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PCbuild_ASP.MVC_.Models.Identity
{
    public class ApplicationUser: IdentityUser
    {
        public int Year { get; set; }
        public ApplicationUser()
        {
        }
    }
}