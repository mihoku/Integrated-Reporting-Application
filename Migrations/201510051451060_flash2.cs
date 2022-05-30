namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flash2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefFlashKegiatanStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ket = c.String(),
                        Aktif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TransFlashKegiatanProgresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Detail = c.String(),
                        Period = c.Int(nullable: false),
                        KegiatanID = c.Int(nullable: false),
                        KegStatusID = c.Int(nullable: false),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(nullable: false),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefFlashKegiatanStatus", t => t.KegStatusID, cascadeDelete: true)
                .ForeignKey("dbo.RefPeriodes", t => t.Period, cascadeDelete: true)
                .ForeignKey("dbo.TransFlashKegiatans", t => t.KegiatanID, cascadeDelete: true)
                .Index(t => t.Period)
                .Index(t => t.KegiatanID)
                .Index(t => t.KegStatusID);
            
            CreateTable(
                "dbo.TransFlashKegiatans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Judul = c.String(),
                        ManajerID = c.Int(nullable: false),
                        UnitID = c.Int(nullable: false),
                        TanggalKasus = c.DateTime(nullable: false),
                        Finalize = c.Int(nullable: false),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefPegawais", t => t.ManajerID, cascadeDelete: true)
                .ForeignKey("dbo.RefUnitPJs", t => t.UnitID, cascadeDelete: true)
                .Index(t => t.ManajerID)
                .Index(t => t.UnitID);
            
            CreateTable(
                "dbo.TransFlashKegiatanKomentars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KomenUserID = c.String(),
                        KomenIsi = c.String(),
                        KegiatanID = c.Int(nullable: false),
                        KomenTgl = c.DateTime(nullable: false),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(nullable: false),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TransFlashKegiatans", t => t.KegiatanID, cascadeDelete: true)
                .Index(t => t.KegiatanID);
            
            CreateTable(
                "dbo.TransFlashNotifClicks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TransFlashNotifikasis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        body = c.String(),
                        Date = c.DateTime(nullable: false),
                        Controller = c.String(),
                        name = c.String(),
                        Action = c.String(),
                        RouteID = c.Int(),
                        RoleID = c.Int(nullable: false),
                        NotifType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransFlashKegiatanProgresses", "KegiatanID", "dbo.TransFlashKegiatans");
            DropForeignKey("dbo.TransFlashKegiatanKomentars", "KegiatanID", "dbo.TransFlashKegiatans");
            DropForeignKey("dbo.TransFlashKegiatans", "UnitID", "dbo.RefUnitPJs");
            DropForeignKey("dbo.TransFlashKegiatans", "ManajerID", "dbo.RefPegawais");
            DropForeignKey("dbo.TransFlashKegiatanProgresses", "Period", "dbo.RefPeriodes");
            DropForeignKey("dbo.TransFlashKegiatanProgresses", "KegStatusID", "dbo.RefFlashKegiatanStatus");
            DropIndex("dbo.TransFlashKegiatanKomentars", new[] { "KegiatanID" });
            DropIndex("dbo.TransFlashKegiatans", new[] { "UnitID" });
            DropIndex("dbo.TransFlashKegiatans", new[] { "ManajerID" });
            DropIndex("dbo.TransFlashKegiatanProgresses", new[] { "KegStatusID" });
            DropIndex("dbo.TransFlashKegiatanProgresses", new[] { "KegiatanID" });
            DropIndex("dbo.TransFlashKegiatanProgresses", new[] { "Period" });
            DropTable("dbo.TransFlashNotifikasis");
            DropTable("dbo.TransFlashNotifClicks");
            DropTable("dbo.TransFlashKegiatanKomentars");
            DropTable("dbo.TransFlashKegiatans");
            DropTable("dbo.TransFlashKegiatanProgresses");
            DropTable("dbo.RefFlashKegiatanStatus");
        }
    }
}
