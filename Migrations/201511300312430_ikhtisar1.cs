namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ikhtisar1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransIkhtisarProgresses", "RencanaKerja", c => c.String(maxLength: 1900));
            AddColumn("dbo.TransIkhtisarProgresses", "HasilPengawasan", c => c.String(maxLength: 1900));
            AddColumn("dbo.TransIkhtisarProgresses", "RencanaPengawasan", c => c.String(maxLength: 1900));
            AddColumn("dbo.TransIkhtisarProgresses", "UniverseID", c => c.Int(nullable: false));
            AddColumn("dbo.TransIkhtisarProgresses", "SysUsername", c => c.String(maxLength: 100));
            AddColumn("dbo.TransIkhtisarProgresses", "SysWorkstation", c => c.String(maxLength: 100));
            AddColumn("dbo.TransIkhtisarProgresses", "SysTglEntry", c => c.DateTime(nullable: false));
            CreateIndex("dbo.TransIkhtisarProgresses", "UniverseID");
            AddForeignKey("dbo.TransIkhtisarProgresses", "UniverseID", "dbo.RefUniverseAudits", "ID", cascadeDelete: true);
            DropColumn("dbo.TransIkhtisarProgresses", "Text");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransIkhtisarProgresses", "Text", c => c.String(maxLength: 1900));
            DropForeignKey("dbo.TransIkhtisarProgresses", "UniverseID", "dbo.RefUniverseAudits");
            DropIndex("dbo.TransIkhtisarProgresses", new[] { "UniverseID" });
            DropColumn("dbo.TransIkhtisarProgresses", "SysTglEntry");
            DropColumn("dbo.TransIkhtisarProgresses", "SysWorkstation");
            DropColumn("dbo.TransIkhtisarProgresses", "SysUsername");
            DropColumn("dbo.TransIkhtisarProgresses", "UniverseID");
            DropColumn("dbo.TransIkhtisarProgresses", "RencanaPengawasan");
            DropColumn("dbo.TransIkhtisarProgresses", "HasilPengawasan");
            DropColumn("dbo.TransIkhtisarProgresses", "RencanaKerja");
        }
    }
}
