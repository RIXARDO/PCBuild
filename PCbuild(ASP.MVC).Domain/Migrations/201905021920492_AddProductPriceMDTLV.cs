namespace PCbuild_ASP.MVC_.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductPriceMDTLV : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PriceCPUs", "CPU_CPUID", "dbo.CPUs");
            DropForeignKey("dbo.PriceGPUs", "GPU_GPUID", "dbo.GPUs");
            DropForeignKey("dbo.BuildEntities", "CPUID", "dbo.CPUs");
            DropForeignKey("dbo.BuildEntities", "GPUID", "dbo.GPUs");
            DropIndex("dbo.BuildEntities", new[] { "CPUID" });
            DropIndex("dbo.BuildEntities", new[] { "GPUID" });
            DropIndex("dbo.PriceCPUs", new[] { "CPU_CPUID" });
            DropIndex("dbo.PriceGPUs", new[] { "GPU_GPUID" });
            DropPrimaryKey("dbo.CPUs");
            DropPrimaryKey("dbo.GPUs");
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductGuid = c.Guid(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ProductGuid);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        PriceGuid = c.Guid(nullable: false),
                        Vendor = c.String(),
                        Сurrency = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PriceGuid)
                .ForeignKey("dbo.Products", t => t.PriceGuid)
                .Index(t => t.PriceGuid);
            
            AddColumn("dbo.BuildEntities", "CPU_ProductGuid", c => c.Guid());
            AddColumn("dbo.BuildEntities", "GPU_ProductGuid", c => c.Guid());
            AddColumn("dbo.CPUs", "ProductGuid", c => c.Guid(nullable: false));
            AddColumn("dbo.GPUs", "ProductGuid", c => c.Guid(nullable: false));
            AddColumn("dbo.GPUs", "Developer", c => c.String());
            AddPrimaryKey("dbo.CPUs", "ProductGuid");
            AddPrimaryKey("dbo.GPUs", "ProductGuid");
            CreateIndex("dbo.BuildEntities", "CPU_ProductGuid");
            CreateIndex("dbo.BuildEntities", "GPU_ProductGuid");
            CreateIndex("dbo.CPUs", "ProductGuid");
            CreateIndex("dbo.GPUs", "ProductGuid");
            AddForeignKey("dbo.CPUs", "ProductGuid", "dbo.Products", "ProductGuid");
            AddForeignKey("dbo.GPUs", "ProductGuid", "dbo.Products", "ProductGuid");
            AddForeignKey("dbo.BuildEntities", "CPU_ProductGuid", "dbo.CPUs", "ProductGuid");
            AddForeignKey("dbo.BuildEntities", "GPU_ProductGuid", "dbo.GPUs", "ProductGuid");
            DropColumn("dbo.CPUs", "CPUID");
            DropColumn("dbo.GPUs", "GPUID");
            DropTable("dbo.PriceCPUs");
            DropTable("dbo.PriceGPUs");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.PriceID);
            
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
                .PrimaryKey(t => t.PriceID);
            
            AddColumn("dbo.GPUs", "GPUID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.CPUs", "CPUID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.BuildEntities", "GPU_ProductGuid", "dbo.GPUs");
            DropForeignKey("dbo.BuildEntities", "CPU_ProductGuid", "dbo.CPUs");
            DropForeignKey("dbo.GPUs", "ProductGuid", "dbo.Products");
            DropForeignKey("dbo.CPUs", "ProductGuid", "dbo.Products");
            DropForeignKey("dbo.Prices", "PriceGuid", "dbo.Products");
            DropIndex("dbo.GPUs", new[] { "ProductGuid" });
            DropIndex("dbo.CPUs", new[] { "ProductGuid" });
            DropIndex("dbo.Prices", new[] { "PriceGuid" });
            DropIndex("dbo.BuildEntities", new[] { "GPU_ProductGuid" });
            DropIndex("dbo.BuildEntities", new[] { "CPU_ProductGuid" });
            DropPrimaryKey("dbo.GPUs");
            DropPrimaryKey("dbo.CPUs");
            DropColumn("dbo.GPUs", "Developer");
            DropColumn("dbo.GPUs", "ProductGuid");
            DropColumn("dbo.CPUs", "ProductGuid");
            DropColumn("dbo.BuildEntities", "GPU_ProductGuid");
            DropColumn("dbo.BuildEntities", "CPU_ProductGuid");
            DropTable("dbo.Prices");
            DropTable("dbo.Products");
            AddPrimaryKey("dbo.GPUs", "GPUID");
            AddPrimaryKey("dbo.CPUs", "CPUID");
            CreateIndex("dbo.PriceGPUs", "GPU_GPUID");
            CreateIndex("dbo.PriceCPUs", "CPU_CPUID");
            CreateIndex("dbo.BuildEntities", "GPUID");
            CreateIndex("dbo.BuildEntities", "CPUID");
            AddForeignKey("dbo.BuildEntities", "GPUID", "dbo.GPUs", "GPUID", cascadeDelete: true);
            AddForeignKey("dbo.BuildEntities", "CPUID", "dbo.CPUs", "CPUID", cascadeDelete: true);
            AddForeignKey("dbo.PriceGPUs", "GPU_GPUID", "dbo.GPUs", "GPUID");
            AddForeignKey("dbo.PriceCPUs", "CPU_CPUID", "dbo.CPUs", "CPUID");
        }
    }
}
