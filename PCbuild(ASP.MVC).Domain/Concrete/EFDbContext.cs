using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PCbuild_ASP.MVC_.Domain.Entities;
using PCbuild_ASP.MVC_.Domain.DAL;

namespace PCbuild_ASP.MVC_.Domain.Concrete
{
    public class EFDbContext: DbContext
    {

        public EFDbContext():base("EFDbContext")
        {
            Database.SetInitializer(new PCBuildInitializer());
        }

        public DbSet<CPU> CPUs { get; set;}
        public DbSet<GPU> GPUs { get; set; }
        public DbSet<Game> Games { get; set; }
    }
}