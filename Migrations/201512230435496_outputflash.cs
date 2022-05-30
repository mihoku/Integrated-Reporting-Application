namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class outputflash : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransFlashKegiatanOutputs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nomor = c.String(maxLength: 100),
                        TanggalTerbit = c.DateTime(nullable: false),
                        Judul = c.String(maxLength: 100),
                        Uraian = c.String(maxLength: 300),
                        KegiatanID = c.Int(nullable: false),
                        OutputJenisID = c.Int(nullable: false),
                        SysUsername = c.String(maxLength: 100),
                        SysTglEntry = c.DateTime(nullable: false),
                        SysWorkstation = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefKegiatanOutputJenis", t => t.OutputJenisID, cascadeDelete: true)
                .ForeignKey("dbo.TransFlashKegiatans", t => t.KegiatanID, cascadeDelete: true)
                .Index(t => t.KegiatanID)
                .Index(t => t.OutputJenisID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransFlashKegiatanOutputs", "KegiatanID", "dbo.TransFlashKegiatans");
            DropForeignKey("dbo.TransFlashKegiatanOutputs", "OutputJenisID", "dbo.RefKegiatanOutputJenis");
            DropIndex("dbo.TransFlashKegiatanOutputs", new[] { "OutputJenisID" });
            DropIndex("dbo.TransFlashKegiatanOutputs", new[] { "KegiatanID" });
            DropTable("dbo.TransFlashKegiatanOutputs");
        }
    }
}
