namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class periode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransIkhtisarProgresses", "PKPTID", c => c.Int(nullable: false));
            AddColumn("dbo.TransIkhtisarProgresses", "PeriodeID", c => c.Int(nullable: false));
            CreateIndex("dbo.TransIkhtisarProgresses", "PKPTID");
            CreateIndex("dbo.TransIkhtisarProgresses", "PeriodeID");
            AddForeignKey("dbo.TransIkhtisarProgresses", "PeriodeID", "dbo.RefPeriodes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TransIkhtisarProgresses", "PKPTID", "dbo.TransSchedules", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransIkhtisarProgresses", "PKPTID", "dbo.TransSchedules");
            DropForeignKey("dbo.TransIkhtisarProgresses", "PeriodeID", "dbo.RefPeriodes");
            DropIndex("dbo.TransIkhtisarProgresses", new[] { "PeriodeID" });
            DropIndex("dbo.TransIkhtisarProgresses", new[] { "PKPTID" });
            DropColumn("dbo.TransIkhtisarProgresses", "PeriodeID");
            DropColumn("dbo.TransIkhtisarProgresses", "PKPTID");
        }
    }
}
