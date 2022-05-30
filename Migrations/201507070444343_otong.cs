namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class otong : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RefKegiatans", "KegMjrID", "dbo.RefPegawais");
            DropForeignKey("dbo.RefTPUs", "TPUPJID", "dbo.RefPegawais");
            DropForeignKey("dbo.RefPegawais", "PegUnitID", "dbo.RefUnitPJs");
            DropForeignKey("dbo.RefTPUs", "TPUUnitPJID", "dbo.RefUnitPJs");
            DropForeignKey("dbo.RefKegiatans", "KegiatanTPUID", "dbo.RefTPUs");
            DropForeignKey("dbo.TransFlashReports", "FlashKegID", "dbo.RefKegiatans");
            DropForeignKey("dbo.TransFlashReportContents", "FlashReportID", "dbo.TransFlashReports");
            DropForeignKey("dbo.TransKegiatanHS", "KegiatanID", "dbo.RefKegiatans");
            DropForeignKey("dbo.TransKegiatanKomentars", "KomenKegID", "dbo.RefKegiatans");
            DropForeignKey("dbo.TransKegiatanOutputs", "KegiatanID", "dbo.RefKegiatans");
            DropForeignKey("dbo.TransKegiatanOutputs", "OutputJenisID", "dbo.RefKegiatanOutputJenis");
            DropForeignKey("dbo.TransKegiatanProgresses", "KegiatanID", "dbo.RefKegiatans");
            DropForeignKey("dbo.TransKegiatanProgresses", "KegStatusID", "dbo.RefKegiatanStatus");
            DropForeignKey("dbo.TransKegiatanSTs", "KegiatanID", "dbo.RefKegiatans");
            DropForeignKey("dbo.RefTPUs", "TPUQTargetID", "dbo.RefQuarters");
            DropForeignKey("dbo.RefTPUs", "TPUJenisID", "dbo.RefTPUJenis");
            DropForeignKey("dbo.RefTPUs", "TPUStatusID", "dbo.RefTPUStatus");
            DropForeignKey("dbo.TransTPUTujuans", "TPUID", "dbo.RefTPUs");
            DropIndex("dbo.RefTPUs", new[] { "TPUPJID" });
            DropIndex("dbo.RefTPUs", new[] { "TPUQTargetID" });
            DropIndex("dbo.RefTPUs", new[] { "TPUStatusID" });
            DropIndex("dbo.RefTPUs", new[] { "TPUUnitPJID" });
            DropIndex("dbo.RefTPUs", new[] { "TPUJenisID" });
            DropIndex("dbo.RefKegiatans", new[] { "KegiatanTPUID" });
            DropIndex("dbo.RefKegiatans", new[] { "KegMjrID" });
            DropIndex("dbo.RefPegawais", new[] { "PegUnitID" });
            DropIndex("dbo.TransFlashReports", new[] { "FlashKegID" });
            DropIndex("dbo.TransFlashReportContents", new[] { "FlashReportID" });
            DropIndex("dbo.TransKegiatanHS", new[] { "KegiatanID" });
            DropIndex("dbo.TransKegiatanKomentars", new[] { "KomenKegID" });
            DropIndex("dbo.TransKegiatanOutputs", new[] { "KegiatanID" });
            DropIndex("dbo.TransKegiatanOutputs", new[] { "OutputJenisID" });
            DropIndex("dbo.TransKegiatanProgresses", new[] { "KegiatanID" });
            DropIndex("dbo.TransKegiatanProgresses", new[] { "KegStatusID" });
            DropIndex("dbo.TransKegiatanSTs", new[] { "KegiatanID" });
            DropIndex("dbo.TransTPUTujuans", new[] { "TPUID" });
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropTable("dbo.RefTPUJenis");
            DropTable("dbo.RefTPUs");
            DropTable("dbo.RefKegiatans");
            DropTable("dbo.RefPegawais");
            DropTable("dbo.RefUnitPJs");
            DropTable("dbo.TransFlashReports");
            DropTable("dbo.TransFlashReportContents");
            DropTable("dbo.TransKegiatanHS");
            DropTable("dbo.TransKegiatanKomentars");
            DropTable("dbo.TransKegiatanOutputs");
            DropTable("dbo.RefKegiatanOutputJenis");
            DropTable("dbo.TransKegiatanProgresses");
            DropTable("dbo.RefKegiatanStatus");
            DropTable("dbo.TransKegiatanSTs");
            DropTable("dbo.RefQuarters");
            DropTable("dbo.RefTPUStatus");
            DropTable("dbo.TransTPUTujuans");
            DropTable("dbo.RefPopupTexts");
            DropTable("dbo.RefRoles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RefRoles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Detail = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RefPopupTexts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Airing = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RefTPUStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ket = c.String(),
                        Aktif = c.Boolean(nullable: false),
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
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RefKegiatanStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ket = c.String(),
                        Aktif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TransKegiatanProgresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Detail = c.String(),
                        Tahun = c.Int(nullable: false),
                        Period = c.Int(nullable: false),
                        KegiatanID = c.Int(nullable: false),
                        KegStatusID = c.Int(nullable: false),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(nullable: false),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TransKegiatanKomentars",
                c => new
                    {
                        KomenID = c.Int(nullable: false, identity: true),
                        KomenUserID = c.String(),
                        KomenIsi = c.String(),
                        KomenKegID = c.Int(),
                        KomenTgl = c.DateTime(),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(nullable: false),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.KomenID);
            
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
                .PrimaryKey(t => t.ID);
            
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
            
            CreateTable(
                "dbo.RefUnitPJs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Detail = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RefPegawais",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PegName = c.String(),
                        PegNIP = c.String(maxLength: 18),
                        PegUnitID = c.Int(nullable: false),
                        PegEmailKemenkeu = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RefKegiatans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KegName = c.String(),
                        KegiatanTPUID = c.Int(nullable: false),
                        KegMjrID = c.Int(nullable: false),
                        KegTarget = c.DateTime(nullable: false),
                        Keterangan = c.String(),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RefTPUs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        No = c.Int(nullable: false),
                        TPUName = c.String(nullable: false),
                        TPUThAnggaran = c.Int(nullable: false),
                        TPUPJID = c.Int(nullable: false),
                        TPUQTargetID = c.Int(nullable: false),
                        TPUStatusID = c.Int(nullable: false),
                        TPUUnitPJID = c.Int(nullable: false),
                        TPUJenisID = c.Int(nullable: false),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RefTPUJenis",
                c => new
                    {
                        JenisID = c.Int(nullable: false, identity: true),
                        JenisDetail = c.String(),
                        Aktif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.JenisID);
            
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            CreateIndex("dbo.TransTPUTujuans", "TPUID");
            CreateIndex("dbo.TransKegiatanSTs", "KegiatanID");
            CreateIndex("dbo.TransKegiatanProgresses", "KegStatusID");
            CreateIndex("dbo.TransKegiatanProgresses", "KegiatanID");
            CreateIndex("dbo.TransKegiatanOutputs", "OutputJenisID");
            CreateIndex("dbo.TransKegiatanOutputs", "KegiatanID");
            CreateIndex("dbo.TransKegiatanKomentars", "KomenKegID");
            CreateIndex("dbo.TransKegiatanHS", "KegiatanID");
            CreateIndex("dbo.TransFlashReportContents", "FlashReportID");
            CreateIndex("dbo.TransFlashReports", "FlashKegID");
            CreateIndex("dbo.RefPegawais", "PegUnitID");
            CreateIndex("dbo.RefKegiatans", "KegMjrID");
            CreateIndex("dbo.RefKegiatans", "KegiatanTPUID");
            CreateIndex("dbo.RefTPUs", "TPUJenisID");
            CreateIndex("dbo.RefTPUs", "TPUUnitPJID");
            CreateIndex("dbo.RefTPUs", "TPUStatusID");
            CreateIndex("dbo.RefTPUs", "TPUQTargetID");
            CreateIndex("dbo.RefTPUs", "TPUPJID");
            AddForeignKey("dbo.TransTPUTujuans", "TPUID", "dbo.RefTPUs", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RefTPUs", "TPUStatusID", "dbo.RefTPUStatus", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RefTPUs", "TPUJenisID", "dbo.RefTPUJenis", "JenisID", cascadeDelete: true);
            AddForeignKey("dbo.RefTPUs", "TPUQTargetID", "dbo.RefQuarters", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TransKegiatanSTs", "KegiatanID", "dbo.RefKegiatans", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TransKegiatanProgresses", "KegStatusID", "dbo.RefKegiatanStatus", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TransKegiatanProgresses", "KegiatanID", "dbo.RefKegiatans", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TransKegiatanOutputs", "OutputJenisID", "dbo.RefKegiatanOutputJenis", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TransKegiatanOutputs", "KegiatanID", "dbo.RefKegiatans", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TransKegiatanKomentars", "KomenKegID", "dbo.RefKegiatans", "ID");
            AddForeignKey("dbo.TransKegiatanHS", "KegiatanID", "dbo.RefKegiatans", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TransFlashReportContents", "FlashReportID", "dbo.TransFlashReports", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TransFlashReports", "FlashKegID", "dbo.RefKegiatans", "ID");
            AddForeignKey("dbo.RefKegiatans", "KegiatanTPUID", "dbo.RefTPUs", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RefTPUs", "TPUUnitPJID", "dbo.RefUnitPJs", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RefPegawais", "PegUnitID", "dbo.RefUnitPJs", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RefTPUs", "TPUPJID", "dbo.RefPegawais", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RefKegiatans", "KegMjrID", "dbo.RefPegawais", "ID", cascadeDelete: true);
        }
    }
}
