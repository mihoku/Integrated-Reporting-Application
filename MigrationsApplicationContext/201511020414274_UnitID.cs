namespace ira.MigrationsApplicationContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UnitID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UnitID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UnitID");
        }
    }
}
