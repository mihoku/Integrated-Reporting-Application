namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFK : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefTPUJenis",
                c => new
                    {
                        JenisID = c.Int(nullable: false, identity: true),
                        JenisDetail = c.String(),
                    })
                .PrimaryKey(t => t.JenisID);
            
            CreateTable(
                "dbo.RefTPUs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TPUName = c.String(nullable: false),
                        TPUThAnggaran = c.Int(nullable: false),
                        TPUTujuan = c.String(nullable: false),
                        TPUPJID = c.Int(nullable: false),
                        TPUQTargetID = c.Int(nullable: false),
                        TPUStatusID = c.Int(nullable: false),
                        TPUUnitPJID = c.Int(nullable: false),
                        TPUJenisID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefPegawais", t => t.TPUPJID, cascadeDelete: true)
                .ForeignKey("dbo.RefUnitPJs", t => t.TPUUnitPJID, cascadeDelete: true)
                .ForeignKey("dbo.RefQuarters", t => t.TPUQTargetID, cascadeDelete: true)
                .ForeignKey("dbo.RefTPUJenis", t => t.TPUJenisID, cascadeDelete: true)
                .ForeignKey("dbo.RefTPUStatus", t => t.TPUStatusID, cascadeDelete: true)
                .Index(t => t.TPUPJID)
                .Index(t => t.TPUQTargetID)
                .Index(t => t.TPUStatusID)
                .Index(t => t.TPUUnitPJID)
                .Index(t => t.TPUJenisID);
            
            CreateTable(
                "dbo.RefKegiatans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KegName = c.String(),
                        KegiatanTPUID = c.Int(nullable: false),
                        KegMjrID = c.Int(nullable: false),
                        KegWaMjrID = c.Int(nullable: false),
                        KegOutput = c.String(),
                        KegStatusID = c.Int(nullable: false),
                        KegTarget = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefKegiatanStatus", t => t.KegStatusID, cascadeDelete: true)
                .ForeignKey("dbo.RefTPUs", t => t.KegiatanTPUID, cascadeDelete: true)
                .Index(t => t.KegiatanTPUID)
                .Index(t => t.KegStatusID);
            
            CreateTable(
                "dbo.RefKegiatanStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ket = c.String(),
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefKegiatans", t => t.FlashKegID)
                .Index(t => t.FlashKegID);
            
            CreateTable(
                "dbo.TransFlashReportContents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReportContent = c.String(),
                        FlashReportID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TransFlashReports", t => t.FlashReportID, cascadeDelete: true)
                .Index(t => t.FlashReportID);
            
            CreateTable(
                "dbo.TransKegiatanKomentars",
                c => new
                    {
                        KomenID = c.Int(nullable: false, identity: true),
                        KomenUserID = c.String(),
                        KomenIsi = c.String(),
                        KomenKegID = c.Int(),
                        KomenTgl = c.DateTime(),
                    })
                .PrimaryKey(t => t.KomenID)
                .ForeignKey("dbo.RefKegiatans", t => t.KomenKegID)
                .Index(t => t.KomenKegID);
            
            CreateTable(
                "dbo.TransKegiatanProgresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Detail = c.String(),
                        Tahun = c.Int(nullable: false),
                        Period = c.Int(nullable: false),
                        KegiatanID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefKegiatans", t => t.KegiatanID, cascadeDelete: true)
                .Index(t => t.KegiatanID);
            
            CreateTable(
                "dbo.RefPegawais",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PegName = c.String(),
                        PegNIP = c.Double(nullable: false),
                        PegUnitID = c.Int(nullable: false),
                        PegEmailKemenkeu = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefUnitPJs", t => t.PegUnitID, cascadeDelete: true)
                .Index(t => t.PegUnitID);
            
            CreateTable(
                "dbo.RefUnitPJs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Detail = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RefQuarters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuarterDetails = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RefTPUStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ket = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RefRoles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Detail = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefTPUs", "TPUStatusID", "dbo.RefTPUStatus");
            DropForeignKey("dbo.RefTPUs", "TPUJenisID", "dbo.RefTPUJenis");
            DropForeignKey("dbo.RefTPUs", "TPUQTargetID", "dbo.RefQuarters");
            DropForeignKey("dbo.RefTPUs", "TPUUnitPJID", "dbo.RefUnitPJs");
            DropForeignKey("dbo.RefPegawais", "PegUnitID", "dbo.RefUnitPJs");
            DropForeignKey("dbo.RefTPUs", "TPUPJID", "dbo.RefPegawais");
            DropForeignKey("dbo.TransKegiatanProgresses", "KegiatanID", "dbo.RefKegiatans");
            DropForeignKey("dbo.TransKegiatanKomentars", "KomenKegID", "dbo.RefKegiatans");
            DropForeignKey("dbo.TransFlashReportContents", "FlashReportID", "dbo.TransFlashReports");
            DropForeignKey("dbo.TransFlashReports", "FlashKegID", "dbo.RefKegiatans");
            DropForeignKey("dbo.RefKegiatans", "KegiatanTPUID", "dbo.RefTPUs");
            DropForeignKey("dbo.RefKegiatans", "KegStatusID", "dbo.RefKegiatanStatus");
            DropIndex("dbo.RefPegawais", new[] { "PegUnitID" });
            DropIndex("dbo.TransKegiatanProgresses", new[] { "KegiatanID" });
            DropIndex("dbo.TransKegiatanKomentars", new[] { "KomenKegID" });
            DropIndex("dbo.TransFlashReportContents", new[] { "FlashReportID" });
            DropIndex("dbo.TransFlashReports", new[] { "FlashKegID" });
            DropIndex("dbo.RefKegiatans", new[] { "KegStatusID" });
            DropIndex("dbo.RefKegiatans", new[] { "KegiatanTPUID" });
            DropIndex("dbo.RefTPUs", new[] { "TPUJenisID" });
            DropIndex("dbo.RefTPUs", new[] { "TPUUnitPJID" });
            DropIndex("dbo.RefTPUs", new[] { "TPUStatusID" });
            DropIndex("dbo.RefTPUs", new[] { "TPUQTargetID" });
            DropIndex("dbo.RefTPUs", new[] { "TPUPJID" });
            DropTable("dbo.RefRoles");
            DropTable("dbo.RefTPUStatus");
            DropTable("dbo.RefQuarters");
            DropTable("dbo.RefUnitPJs");
            DropTable("dbo.RefPegawais");
            DropTable("dbo.TransKegiatanProgresses");
            DropTable("dbo.TransKegiatanKomentars");
            DropTable("dbo.TransFlashReportContents");
            DropTable("dbo.TransFlashReports");
            DropTable("dbo.RefKegiatanStatus");
            DropTable("dbo.RefKegiatans");
            DropTable("dbo.RefTPUs");
            DropTable("dbo.RefTPUJenis");
        }
    }
}
