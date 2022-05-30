namespace ira.MigrationsApplicationContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isRevoked : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "isRevoked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "isRevoked");
        }
    }
}
