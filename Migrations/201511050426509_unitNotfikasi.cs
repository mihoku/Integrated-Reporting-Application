namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unitNotfikasi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransFlashNotifikasis", "UnitID", c => c.Int(nullable: false));
            AddColumn("dbo.TransNotifikasis", "UnitID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransNotifikasis", "UnitID");
            DropColumn("dbo.TransFlashNotifikasis", "UnitID");
        }
    }
}
