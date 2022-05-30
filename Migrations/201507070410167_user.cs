namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RefKegiatans", "KegStatusID", "dbo.RefKegiatanStatus");
            DropForeignKey("dbo.RefKegiatans", "KegiatanTPUID", "dbo.RefTPUs");
            DropForeignKey("dbo.TransFlashReports", "FlashKegID", "dbo.RefKegiatans");
            DropForeignKey("dbo.TransFlashReportContents", "FlashReportID", "dbo.TransFlashReports");
            DropForeignKey("dbo.TransKegiatanKomentars", "KomenKegID", "dbo.RefKegiatans");
            DropForeignKey("dbo.TransKegiatanProgresses", "KegiatanID", "dbo.RefKegiatans");
            DropForeignKey("dbo.RefTPUs", "TPUPJID", "dbo.RefPegawais");
            DropForeignKey("dbo.RefTPUs", "TPUQTargetID", "dbo.RefQuarters");
            DropForeignKey("dbo.RefTPUs", "TPUJenisID", "dbo.RefTPUJenis");
            DropForeignKey("dbo.RefTPUs", "TPUStatusID", "dbo.RefTPUStatus");
            DropForeignKey("dbo.RefTPUs", "TPUUnitPJID", "dbo.RefUnitPJs");
            DropIndex("dbo.RefTPUs", new[] { "TPUPJID" });
            DropIndex("dbo.RefTPUs", new[] { "TPUQTargetID" });
            DropIndex("dbo.RefTPUs", new[] { "TPUStatusID" });
            DropIndex("dbo.RefTPUs", new[] { "TPUUnitPJID" });
            DropIndex("dbo.RefTPUs", new[] { "TPUJenisID" });
            DropIndex("dbo.RefKegiatans", new[] { "KegiatanTPUID" });
            DropIndex("dbo.RefKegiatans", new[] { "KegStatusID" });
            DropIndex("dbo.TransFlashReports", new[] { "FlashKegID" });
            DropIndex("dbo.TransFlashReportContents", new[] { "FlashReportID" });
            DropIndex("dbo.TransKegiatanKomentars", new[] { "KomenKegID" });
            DropIndex("dbo.TransKegiatanProgresses", new[] { "KegiatanID" });
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
            DropTable("dbo.RefKegiatanStatus");
            DropTable("dbo.TransFlashReports");
            DropTable("dbo.TransFlashReportContents");
            DropTable("dbo.TransKegiatanKomentars");
            DropTable("dbo.TransKegiatanProgresses");
            DropTable("dbo.RefPegawais");
            DropTable("dbo.RefQuarters");
            DropTable("dbo.RefTPUStatus");
            DropTable("dbo.RefUnitPJs");
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
                "dbo.RefUnitPJs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Detail = c.String(),
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
                "dbo.RefQuarters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuarterDetails = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                    })
                .PrimaryKey(t => t.KomenID);
            
            CreateTable(
                "dbo.TransFlashReportContents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReportContent = c.String(),
                        FlashReportID = c.Int(nullable: false),
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
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RefKegiatanStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ket = c.String(),
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
                        KegWaMjrID = c.Int(nullable: false),
                        KegOutput = c.String(),
                        KegStatusID = c.Int(nullable: false),
                        KegTarget = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RefTPUJenis",
                c => new
                    {
                        JenisID = c.Int(nullable: false, identity: true),
                        JenisDetail = c.String(),
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
            CreateIndex("dbo.TransKegiatanProgresses", "KegiatanID");
            CreateIndex("dbo.TransKegiatanKomentars", "KomenKegID");
            CreateIndex("dbo.TransFlashReportContents", "FlashReportID");
            CreateIndex("dbo.TransFlashReports", "FlashKegID");
            CreateIndex("dbo.RefKegiatans", "KegStatusID");
            CreateIndex("dbo.RefKegiatans", "KegiatanTPUID");
            CreateIndex("dbo.RefTPUs", "TPUJenisID");
            CreateIndex("dbo.RefTPUs", "TPUUnitPJID");
            CreateIndex("dbo.RefTPUs", "TPUStatusID");
            CreateIndex("dbo.RefTPUs", "TPUQTargetID");
            CreateIndex("dbo.RefTPUs", "TPUPJID");
            AddForeignKey("dbo.RefTPUs", "TPUUnitPJID", "dbo.RefUnitPJs", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RefTPUs", "TPUStatusID", "dbo.RefTPUStatus", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RefTPUs", "TPUJenisID", "dbo.RefTPUJenis", "JenisID", cascadeDelete: true);
            AddForeignKey("dbo.RefTPUs", "TPUQTargetID", "dbo.RefQuarters", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RefTPUs", "TPUPJID", "dbo.RefPegawais", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TransKegiatanProgresses", "KegiatanID", "dbo.RefKegiatans", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TransKegiatanKomentars", "KomenKegID", "dbo.RefKegiatans", "ID");
            AddForeignKey("dbo.TransFlashReportContents", "FlashReportID", "dbo.TransFlashReports", "ID", cascadeDelete: true);
            AddForeignKey("dbo.TransFlashReports", "FlashKegID", "dbo.RefKegiatans", "ID");
            AddForeignKey("dbo.RefKegiatans", "KegiatanTPUID", "dbo.RefTPUs", "ID", cascadeDelete: true);
            AddForeignKey("dbo.RefKegiatans", "KegStatusID", "dbo.RefKegiatanStatus", "ID", cascadeDelete: true);
        }
    }
}
