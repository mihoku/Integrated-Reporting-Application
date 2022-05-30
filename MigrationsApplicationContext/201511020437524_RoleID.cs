namespace ira.MigrationsApplicationContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RoleID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RoleID");
        }
    }
}
