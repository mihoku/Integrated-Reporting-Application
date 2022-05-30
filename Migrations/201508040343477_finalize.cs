namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefKegiatans", "Finalize", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefKegiatans", "Finalize");
        }
    }
}
