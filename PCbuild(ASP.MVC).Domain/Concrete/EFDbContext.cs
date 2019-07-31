using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PCbuilder_ASP.MVC_.Domain.Entities;
using PCbuilder_ASP.MVC_.Domain.DAL;

namespace PCbuilder_ASP.MVC_.Domain.Concrete
{
    public class EFDbContext: DbContext
    {

        public EFDbContext():base("EFDbContext")
        {
            Database.SetInitializer(new PCBuildInitializer());
        }

        public EFDbContext(string ConectionString) : base(ConectionString)
        {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<EFDbContext>());
        }

        public DbSet<CPU> CPUs { get; set;}
        public DbSet<GPU> GPUs { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<BuildEntity> BuildEntities { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Price> Prices { get; set; }
    }
}