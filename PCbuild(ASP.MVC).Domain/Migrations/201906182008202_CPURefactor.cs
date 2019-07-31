namespace PCbuilder_ASP.MVC_.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CPURefactor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CPUs", "AverageBench", c => c.Int(nullable: false));
            DropColumn("dbo.CPUs", "AverangeBench");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CPUs", "AverangeBench", c => c.Int(nullable: false));
            DropColumn("dbo.CPUs", "AverageBench");
        }
    }
}
