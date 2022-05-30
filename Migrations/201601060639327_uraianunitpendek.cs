namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uraianunitpendek : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefUnitPJs", "DetailShort", c => c.String(maxLength: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefUnitPJs", "DetailShort");
        }
    }
}
