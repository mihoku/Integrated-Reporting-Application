namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sekretariat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefUnitPJs", "isPrimeMover", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefUnitPJs", "isPrimeMover");
        }
    }
}
