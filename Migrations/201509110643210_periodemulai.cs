namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class periodemulai : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefKegiatans", "PeriodeID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefKegiatans", "PeriodeID");
        }
    }
}
