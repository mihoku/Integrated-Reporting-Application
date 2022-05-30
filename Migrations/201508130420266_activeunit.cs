namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activeunit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefUnitPJs", "Aktif", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefUnitPJs", "Aktif");
        }
    }
}
