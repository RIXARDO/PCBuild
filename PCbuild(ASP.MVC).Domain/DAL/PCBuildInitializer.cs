using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PCbuild_ASP.MVC_.Domain.Concrete;
using PCbuild_ASP.MVC_.Domain.Entities;

namespace PCbuild_ASP.MVC_.Domain.DAL
{
    internal class PCBuildInitializer: MigrateDatabaseToLatestVersion<EFDbContext,Migrations.Configuration>
    {
        
    }
}