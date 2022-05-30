namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deploy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransKegiatanHS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Hambatan = c.String(),
                        Solusi = c.String(),
                        KegiatanID = c.Int(nullable: false),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(nullable: false),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefKegiatans", t => t.KegiatanID, cascadeDelete: true)
                .Index(t => t.KegiatanID);
            
            CreateTable(
                "dbo.TransKegiatanOutputs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nomor = c.String(),
                        TanggalTerbit = c.DateTime(nullable: false),
                        Uraian = c.String(),
                        KegiatanID = c.Int(nullable: false),
                        OutputJenisID = c.Int(nullable: false),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(nullable: false),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefKegiatans", t => t.KegiatanID, cascadeDelete: true)
                .ForeignKey("dbo.RefKegiatanOutputJenis", t => t.OutputJenisID, cascadeDelete: true)
                .Index(t => t.KegiatanID)
                .Index(t => t.OutputJenisID);
            
            CreateTable(
                "dbo.RefKegiatanOutputJenis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ket = c.String(),
                        Aktif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TransKegiatanSTs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NoST = c.String(),
                        JudulST = c.String(),
                        Tahun = c.Int(nullable: false),
                        TanggalST = c.DateTime(),
                        KegiatanID = c.Int(nullable: false),
                        TglAwal = c.DateTime(),
                        TglAkhir = c.DateTime(),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(nullable: false),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefKegiatans", t => t.KegiatanID, cascadeDelete: true)
                .Index(t => t.KegiatanID);
            
            CreateTable(
                "dbo.TransTPUTujuans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TujuanTPU = c.String(),
                        TPUID = c.Int(nullable: false),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(nullable: false),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefTPUs", t => t.TPUID, cascadeDelete: true)
                .Index(t => t.TPUID);
            
            AddColumn("dbo.RefTPUJenis", "Aktif", c => c.Boolean(nullable: false));
            AddColumn("dbo.RefTPUs", "SysUsername", c => c.String());
            AddColumn("dbo.RefTPUs", "SysTglEntry", c => c.DateTime());
            AddColumn("dbo.RefTPUs", "SysWorkstation", c => c.String());
            AddColumn("dbo.RefKegiatans", "Keterangan", c => c.String());
            AddColumn("dbo.RefKegiatans", "SysUsername", c => c.String());
            AddColumn("dbo.RefKegiatans", "SysTglEntry", c => c.DateTime());
            AddColumn("dbo.RefKegiatans", "SysWorkstation", c => c.String());
            AddColumn("dbo.TransFlashReports", "SysUsername", c => c.String());
            AddColumn("dbo.TransFlashReports", "SysTglEntry", c => c.DateTime(nullable: false));
            AddColumn("dbo.TransFlashReports", "SysWorkstation", c => c.String());
            AddColumn("dbo.TransFlashReportContents", "SysUsername", c => c.String());
            AddColumn("dbo.TransFlashReportContents", "SysTglEntry", c => c.DateTime(nullable: false));
            AddColumn("dbo.TransFlashReportContents", "SysWorkstation", c => c.String());
            AddColumn("dbo.TransKegiatanKomentars", "SysUsername", c => c.String());
            AddColumn("dbo.TransKegiatanKomentars", "SysTglEntry", c => c.DateTime(nullable: false));
            AddColumn("dbo.TransKegiatanKomentars", "SysWorkstation", c => c.String());
            AddColumn("dbo.TransKegiatanProgresses", "SysUsername", c => c.String());
            AddColumn("dbo.TransKegiatanProgresses", "SysTglEntry", c => c.DateTime(nullable: false));
            AddColumn("dbo.TransKegiatanProgresses", "SysWorkstation", c => c.String());
            AddColumn("dbo.RefKegiatanStatus", "Aktif", c => c.Boolean(nullable: false));
            AddColumn("dbo.RefTPUStatus", "Aktif", c => c.Boolean(nullable: false));
            CreateIndex("dbo.RefKegiatans", "KegMjrID");
            AddForeignKey("dbo.RefKegiatans", "KegMjrID", "dbo.RefPegawais", "ID");
            DropColumn("dbo.RefTPUs", "TPUTujuan");
            DropColumn("dbo.RefKegiatans", "KegWaMjrID");
            DropColumn("dbo.RefKegiatans", "KegOutput");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RefKegiatans", "KegOutput", c => c.String());
            AddColumn("dbo.RefKegiatans", "KegWaMjrID", c => c.Int(nullable: false));
            AddColumn("dbo.RefTPUs", "TPUTujuan", c => c.String(nullable: false));
            DropForeignKey("dbo.TransTPUTujuans", "TPUID", "dbo.RefTPUs");
            DropForeignKey("dbo.TransKegiatanSTs", "KegiatanID", "dbo.RefKegiatans");
            DropForeignKey("dbo.TransKegiatanOutputs", "OutputJenisID", "dbo.RefKegiatanOutputJenis");
            DropForeignKey("dbo.TransKegiatanOutputs", "KegiatanID", "dbo.RefKegiatans");
            DropForeignKey("dbo.TransKegiatanHS", "KegiatanID", "dbo.RefKegiatans");
            DropForeignKey("dbo.RefKegiatans", "KegMjrID", "dbo.RefPegawais");
            DropIndex("dbo.TransTPUTujuans", new[] { "TPUID" });
            DropIndex("dbo.TransKegiatanSTs", new[] { "KegiatanID" });
            DropIndex("dbo.TransKegiatanOutputs", new[] { "OutputJenisID" });
            DropIndex("dbo.TransKegiatanOutputs", new[] { "KegiatanID" });
            DropIndex("dbo.TransKegiatanHS", new[] { "KegiatanID" });
            DropIndex("dbo.RefKegiatans", new[] { "KegMjrID" });
            DropColumn("dbo.RefTPUStatus", "Aktif");
            DropColumn("dbo.RefKegiatanStatus", "Aktif");
            DropColumn("dbo.TransKegiatanProgresses", "SysWorkstation");
            DropColumn("dbo.TransKegiatanProgresses", "SysTglEntry");
            DropColumn("dbo.TransKegiatanProgresses", "SysUsername");
            DropColumn("dbo.TransKegiatanKomentars", "SysWorkstation");
            DropColumn("dbo.TransKegiatanKomentars", "SysTglEntry");
            DropColumn("dbo.TransKegiatanKomentars", "SysUsername");
            DropColumn("dbo.TransFlashReportContents", "SysWorkstation");
            DropColumn("dbo.TransFlashReportContents", "SysTglEntry");
            DropColumn("dbo.TransFlashReportContents", "SysUsername");
            DropColumn("dbo.TransFlashReports", "SysWorkstation");
            DropColumn("dbo.TransFlashReports", "SysTglEntry");
            DropColumn("dbo.TransFlashReports", "SysUsername");
            DropColumn("dbo.RefKegiatans", "SysWorkstation");
            DropColumn("dbo.RefKegiatans", "SysTglEntry");
            DropColumn("dbo.RefKegiatans", "SysUsername");
            DropColumn("dbo.RefKegiatans", "Keterangan");
            DropColumn("dbo.RefTPUs", "SysWorkstation");
            DropColumn("dbo.RefTPUs", "SysTglEntry");
            DropColumn("dbo.RefTPUs", "SysUsername");
            DropColumn("dbo.RefTPUJenis", "Aktif");
            DropTable("dbo.TransTPUTujuans");
            DropTable("dbo.TransKegiatanSTs");
            DropTable("dbo.RefKegiatanOutputJenis");
            DropTable("dbo.TransKegiatanOutputs");
            DropTable("dbo.TransKegiatanHS");
        }
    }
}
