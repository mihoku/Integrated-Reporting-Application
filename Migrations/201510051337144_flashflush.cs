namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flashflush : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransFlashReports", "FlashKegID", "dbo.RefKegiatans");
            DropForeignKey("dbo.TransFlashReportContents", "FlashReportID", "dbo.TransFlashReports");
            DropIndex("dbo.TransFlashReports", new[] { "FlashKegID" });
            DropIndex("dbo.TransFlashReportContents", new[] { "FlashReportID" });
            DropTable("dbo.TransFlashReports");
            DropTable("dbo.TransFlashReportContents");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TransFlashReportContents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReportContent = c.String(),
                        FlashReportID = c.Int(nullable: false),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(nullable: false),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TransFlashReports",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Tahun = c.Int(nullable: false),
                        Period = c.Int(nullable: false),
                        KegID = c.Int(nullable: false),
                        FlashID = c.Int(nullable: false),
                        FlashTh = c.Int(),
                        FlashPeriod = c.Int(),
                        FlashKegID = c.Int(),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(nullable: false),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.TransFlashReportContents", "FlashReportID");
            CreateIndex("dbo.TransFlashReports", "FlashKegID");
            AddForeignKey("dbo.TransFlashReportContents", "FlashReportID", "dbo.TransFlashReports", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TransFlashReports", "FlashKegID", "dbo.RefKegiatans", "ID");
        }
    }
}
