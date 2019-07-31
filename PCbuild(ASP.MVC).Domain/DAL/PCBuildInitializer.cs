using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PCbuilder_ASP.MVC_.Domain.Concrete;
using PCbuilder_ASP.MVC_.Domain.Entities;
using PCbuilder_ASP.MVC_.Domain.Migrations;

namespace PCbuilder_ASP.MVC_.Domain.DAL
{
    internal class PCBuildInitializer : MigrateDatabaseToLatestVersion<EFDbContext, Configuration> //DropCreateDatabaseIfModelChanges<EFDbContext>//
    {
        
    }
}