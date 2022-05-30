namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class st1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransKegiatanSTs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NoST = c.String(),
                        JudulST = c.String(),
                        Tahun = c.Int(nullable: false),
                        TanggalST = c.DateTime(nullable: false),
                        KegiatanID = c.Int(nullable: false),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(nullable: false),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefKegiatans", t => t.KegiatanID, cascadeDelete: true)
                .Index(t => t.KegiatanID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransKegiatanSTs", "KegiatanID", "dbo.RefKegiatans");
            DropIndex("dbo.TransKegiatanSTs", new[] { "KegiatanID" });
            DropTable("dbo.TransKegiatanSTs");
        }
    }
}
