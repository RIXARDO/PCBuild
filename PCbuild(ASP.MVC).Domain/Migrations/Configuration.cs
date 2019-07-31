namespace PCbuild_ASP.MVC_.Domain.Migrations
{
    using PCbuild_ASP.MVC_.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PCbuild_ASP.MVC_.Domain.Concrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "PCbuild_ASP.MVC_.Domain.Concrete.EFDbContext";
        }

        protected override void Seed(Concrete.EFDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            //var cpus = new List<CPU>
            //    {
            //    new CPU{ProductGuid=Guid.NewGuid(), Manufacture = "Intel", ProcessorNumber="i9-9900k", Cache="16mb SmartCache", NumberOfCores=8, NumberOfThreads =16, PBF=3.60f , TDP="95W", AverageBench=120 },
            //    new CPU {ProductGuid=Guid.NewGuid(), Manufacture = "Intel", ProcessorNumber="i7-8700k", Cache="12mb SmartCache", NumberOfCores=6, NumberOfThreads =12, PBF=3.70f , TDP="95W", AverageBench=108 },
            //    new CPU{ProductGuid=Guid.NewGuid(), Manufacture = "AMD", ProcessorNumber="Ryzen 7 2700X", Cache="16mb", NumberOfCores=8, NumberOfThreads =16, PBF=3.70f , TDP="105W", AverageBench=100 }
            //};

            //cpus.ForEach(x => context.CPUs.Add(x));
            //context.SaveChanges();

            //var gpus = new List<GPU>
            //{
            //    new GPU{ProductGuid=Guid.NewGuid(), Manufacture="Gigabyte", Developer="Nvidia", Name="RTX 2080Ti", Architecture="Turning", BoostClock=1545, FrameBuffer=11, MemorySpeed=14, AverageBench=194 },
            //    new GPU{ProductGuid=Guid.NewGuid(), Manufacture="MSI", Developer="Nvidia", Name="RTX 2080", Architecture="Turning", BoostClock=1710, FrameBuffer=8, MemorySpeed=14, AverageBench=149 },
            //    new GPU{ProductGuid=Guid.NewGuid(), Manufacture="Asus", Developer="Nvidia", Name="RTX 2070", Architecture="Turning", BoostClock=1620, FrameBuffer=8, MemorySpeed=14, AverageBench=130 },
            //    new GPU{ProductGuid=Guid.NewGuid(), Manufacture="MSI", Developer="AMD", Name="RX Vega", BoostClock=1564, FrameBuffer=8, AverageBench=119}
            //};
            //gpus.ForEach(x => context.GPUs.Add(x));
            //context.SaveChanges();


            //var games = new List<Game>
            //{
            //    new Game{Name="GTAV", AverangeRequirements=50},
            //    new Game{Name = "FFXV", AverangeRequirements = 70}
            //};
            //games.ForEach(x => context.Games.Add(x));
            //context.SaveChanges();

            base.Seed(context);
        }
    }
}
