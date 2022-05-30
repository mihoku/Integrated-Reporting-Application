namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class audittrail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefTPUs", "SysUsername", c => c.String());
            AddColumn("dbo.RefTPUs", "SysTglEntry", c => c.DateTime());
            AddColumn("dbo.RefTPUs", "SysWorkstation", c => c.String());
            AddColumn("dbo.RefKegiatans", "SysUsername", c => c.String());
            AddColumn("dbo.RefKegiatans", "SysTglEntry", c => c.DateTime());
            AddColumn("dbo.RefKegiatans", "SysWorkstation", c => c.String());
            AddColumn("dbo.TransFlashReports", "SysUsername", c => c.String());
            AddColumn("dbo.TransFlashReports", "SysTglEntry", c => c.DateTime(nullable: false));
            AddColumn("dbo.TransFlashReports", "SysWorkstation", c => c.String());
            AddColumn("dbo.TransFlashReportContents", "SysUsername", c => c.String());
            AddColumn("dbo.TransFlashReportContents", "SysTglEntry", c => c.DateTime(nullable: false));
            AddColumn("dbo.TransFlashReportContents", "SysWorkstation", c => c.String());
            AddColumn("dbo.TransKegiatanHS", "SysUsername", c => c.String());
            AddColumn("dbo.TransKegiatanHS", "SysTglEntry", c => c.DateTime(nullable: false));
            AddColumn("dbo.TransKegiatanHS", "SysWorkstation", c => c.String());
            AddColumn("dbo.TransKegiatanKomentars", "SysUsername", c => c.String());
            AddColumn("dbo.TransKegiatanKomentars", "SysTglEntry", c => c.DateTime(nullable: false));
            AddColumn("dbo.TransKegiatanKomentars", "SysWorkstation", c => c.String());
            AddColumn("dbo.TransKegiatanProgresses", "SysUsername", c => c.String());
            AddColumn("dbo.TransKegiatanProgresses", "SysTglEntry", c => c.DateTime(nullable: false));
            AddColumn("dbo.TransKegiatanProgresses", "SysWorkstation", c => c.String());
            AddColumn("dbo.TransTPUTujuans", "SysUsername", c => c.String());
            AddColumn("dbo.TransTPUTujuans", "SysTglEntry", c => c.DateTime(nullable: false));
            AddColumn("dbo.TransTPUTujuans", "SysWorkstation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransTPUTujuans", "SysWorkstation");
            DropColumn("dbo.TransTPUTujuans", "SysTglEntry");
            DropColumn("dbo.TransTPUTujuans", "SysUsername");
            DropColumn("dbo.TransKegiatanProgresses", "SysWorkstation");
            DropColumn("dbo.TransKegiatanProgresses", "SysTglEntry");
            DropColumn("dbo.TransKegiatanProgresses", "SysUsername");
            DropColumn("dbo.TransKegiatanKomentars", "SysWorkstation");
            DropColumn("dbo.TransKegiatanKomentars", "SysTglEntry");
            DropColumn("dbo.TransKegiatanKomentars", "SysUsername");
            DropColumn("dbo.TransKegiatanHS", "SysWorkstation");
            DropColumn("dbo.TransKegiatanHS", "SysTglEntry");
            DropColumn("dbo.TransKegiatanHS", "SysUsername");
            DropColumn("dbo.TransFlashReportContents", "SysWorkstation");
            DropColumn("dbo.TransFlashReportContents", "SysTglEntry");
            DropColumn("dbo.TransFlashReportContents", "SysUsername");
            DropColumn("dbo.TransFlashReports", "SysWorkstation");
            DropColumn("dbo.TransFlashReports", "SysTglEntry");
            DropColumn("dbo.TransFlashReports", "SysUsername");
            DropColumn("dbo.RefKegiatans", "SysWorkstation");
            DropColumn("dbo.RefKegiatans", "SysTglEntry");
            DropColumn("dbo.RefKegiatans", "SysUsername");
            DropColumn("dbo.RefTPUs", "SysWorkstation");
            DropColumn("dbo.RefTPUs", "SysTglEntry");
            DropColumn("dbo.RefTPUs", "SysUsername");
        }
    }
}
