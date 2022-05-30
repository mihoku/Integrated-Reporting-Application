namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lock2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransNDPermintaans", "Locked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransNDPermintaans", "Locked");
        }
    }
}
