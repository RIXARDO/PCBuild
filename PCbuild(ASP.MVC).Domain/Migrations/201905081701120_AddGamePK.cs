namespace PCbuild_ASP.MVC_.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGamePK : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.BuildEntities", new[] { "CPU_ProductGuid" });
            DropIndex("dbo.BuildEntities", new[] { "GPU_ProductGuid" });
            DropColumn("dbo.BuildEntities", "CPUID");
            DropColumn("dbo.BuildEntities", "GPUID");
            RenameColumn(table: "dbo.BuildEntities", name: "CPU_ProductGuid", newName: "CPUID");
            RenameColumn(table: "dbo.BuildEntities", name: "GPU_ProductGuid", newName: "GPUID");
            DropPrimaryKey("dbo.BuildEntities");
            DropPrimaryKey("dbo.Games");
            AddColumn("dbo.BuildEntities", "BuildEntityGuid", c => c.Guid(nullable: false, identity: true));
            AddColumn("dbo.Games", "GameGuid", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.BuildEntities", "CPUID", c => c.Guid(nullable: false));
            AlterColumn("dbo.BuildEntities", "GPUID", c => c.Guid(nullable: false));
            AlterColumn("dbo.BuildEntities", "CPUID", c => c.Guid(nullable: false));
            AlterColumn("dbo.BuildEntities", "GPUID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.BuildEntities", "BuildEntityGuid");
            AddPrimaryKey("dbo.Games", "GameGuid");
            CreateIndex("dbo.BuildEntities", "CPUID");
            CreateIndex("dbo.BuildEntities", "GPUID");
            DropColumn("dbo.BuildEntities", "BuildEntityID");
            DropColumn("dbo.Games", "GameID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "GameID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.BuildEntities", "BuildEntityID", c => c.Int(nullable: false, identity: true));
            DropIndex("dbo.BuildEntities", new[] { "GPUID" });
            DropIndex("dbo.BuildEntities", new[] { "CPUID" });
            DropPrimaryKey("dbo.Games");
            DropPrimaryKey("dbo.BuildEntities");
            AlterColumn("dbo.BuildEntities", "GPUID", c => c.Guid());
            AlterColumn("dbo.BuildEntities", "CPUID", c => c.Guid());
            AlterColumn("dbo.BuildEntities", "GPUID", c => c.Int(nullable: false));
            AlterColumn("dbo.BuildEntities", "CPUID", c => c.Int(nullable: false));
            DropColumn("dbo.Games", "GameGuid");
            DropColumn("dbo.BuildEntities", "BuildEntityGuid");
            AddPrimaryKey("dbo.Games", "GameID");
            AddPrimaryKey("dbo.BuildEntities", "BuildEntityID");
            RenameColumn(table: "dbo.BuildEntities", name: "GPUID", newName: "GPU_ProductGuid");
            RenameColumn(table: "dbo.BuildEntities", name: "CPUID", newName: "CPU_ProductGuid");
            AddColumn("dbo.BuildEntities", "GPUID", c => c.Int(nullable: false));
            AddColumn("dbo.BuildEntities", "CPUID", c => c.Int(nullable: false));
            CreateIndex("dbo.BuildEntities", "GPU_ProductGuid");
            CreateIndex("dbo.BuildEntities", "CPU_ProductGuid");
        }
    }
}
