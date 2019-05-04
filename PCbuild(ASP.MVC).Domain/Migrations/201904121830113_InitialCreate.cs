namespace PCbuild_ASP.MVC_.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuildEntities",
                c => new
                    {
                        BuildEntityID = c.Int(nullable: false, identity: true),
                        CPUID = c.Int(nullable: false),
                        GPUID = c.Int(nullable: false),
                        UserID = c.String(),
                    })
                .PrimaryKey(t => t.BuildEntityID)
                .ForeignKey("dbo.CPUs", t => t.CPUID, cascadeDelete: true)
                .ForeignKey("dbo.GPUs", t => t.GPUID, cascadeDelete: true)
                .Index(t => t.CPUID)
                .Index(t => t.GPUID);
            
            CreateTable(
                "dbo.CPUs",
                c => new
                    {
                        CPUID = c.Int(nullable: false, identity: true),
                        Manufacture = c.String(),
                        ProcessorNumber = c.String(),
                        NumberOfCores = c.Int(nullable: false),
                        NumberOfThreads = c.Int(nullable: false),
                        PBF = c.Single(nullable: false),
                        Cache = c.String(),
                        TDP = c.String(),
                        AverangeBench = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CPUID);
            
            CreateTable(
                "dbo.PriceCPUs",
                c => new
                    {
                        PriceID = c.Int(nullable: false, identity: true),
                        Vendor = c.String(),
                        Сurrency = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        CPU_CPUID = c.Int(),
                    })
                .PrimaryKey(t => t.PriceID)
                .ForeignKey("dbo.CPUs", t => t.CPU_CPUID)
                .Index(t => t.CPU_CPUID);
            
            CreateTable(
                "dbo.GPUs",
                c => new
                    {
                        GPUID = c.Int(nullable: false, identity: true),
                        Manufacture = c.String(),
                        Name = c.String(),
                        Architecture = c.String(),
                        BoostClock = c.Int(nullable: false),
                        FrameBuffer = c.Int(nullable: false),
                        MemorySpeed = c.Int(nullable: false),
                        AverageBench = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GPUID);
            
            CreateTable(
                "dbo.PriceGPUs",
                c => new
                    {
                        PriceID = c.Int(nullable: false, identity: true),
                        Vendor = c.String(),
                        Сurrency = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        GPU_GPUID = c.Int(),
                    })
                .PrimaryKey(t => t.PriceID)
                .ForeignKey("dbo.GPUs", t => t.GPU_GPUID)
                .Index(t => t.GPU_GPUID);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        AverangeRequirements = c.Int(nullable: false),
                        ImageData32 = c.Binary(),
                        ImageMimeType32 = c.String(),
                        ImageData64 = c.Binary(),
                        ImageMimeType64 = c.String(),
                    })
                .PrimaryKey(t => t.GameID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceGPUs", "GPU_GPUID", "dbo.GPUs");
            DropForeignKey("dbo.BuildEntities", "GPUID", "dbo.GPUs");
            DropForeignKey("dbo.PriceCPUs", "CPU_CPUID", "dbo.CPUs");
            DropForeignKey("dbo.BuildEntities", "CPUID", "dbo.CPUs");
            DropIndex("dbo.PriceGPUs", new[] { "GPU_GPUID" });
            DropIndex("dbo.PriceCPUs", new[] { "CPU_CPUID" });
            DropIndex("dbo.BuildEntities", new[] { "GPUID" });
            DropIndex("dbo.BuildEntities", new[] { "CPUID" });
            DropTable("dbo.Games");
            DropTable("dbo.PriceGPUs");
            DropTable("dbo.GPUs");
            DropTable("dbo.PriceCPUs");
            DropTable("dbo.CPUs");
            DropTable("dbo.BuildEntities");
        }
    }
}
