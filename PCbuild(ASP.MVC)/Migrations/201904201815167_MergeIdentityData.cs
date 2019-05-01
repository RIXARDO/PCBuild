namespace PCbuild_ASP.MVC_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MergeIdentityData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Year", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Year", c => c.Int(nullable: false));
        }
    }
}
