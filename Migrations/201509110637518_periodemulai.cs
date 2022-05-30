namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class periodemulai : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefKegiatans", "PeriodeID", c => c.Int(nullable: false));
            CreateIndex("dbo.RefKegiatans", "PeriodeID");
            AddForeignKey("dbo.RefKegiatans", "PeriodeID", "dbo.RefPeriodes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefKegiatans", "PeriodeID", "dbo.RefPeriodes");
            DropIndex("dbo.RefKegiatans", new[] { "PeriodeID" });
            DropColumn("dbo.RefKegiatans", "PeriodeID");
        }
    }
}
