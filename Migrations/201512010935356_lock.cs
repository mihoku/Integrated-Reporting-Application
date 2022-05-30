namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _lock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransIkhtisarProgresses", "Locked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransIkhtisarProgresses", "Locked");
        }
    }
}
