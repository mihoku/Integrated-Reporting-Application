namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class outputandbool : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransKegiatanOutputs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nomor = c.String(),
                        TanggalTerbit = c.DateTime(nullable: false),
                        Uraian = c.String(),
                        KegiatanID = c.Int(nullable: false),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(nullable: false),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefKegiatans", t => t.KegiatanID, cascadeDelete: true)
                .Index(t => t.KegiatanID);
            
            AddColumn("dbo.RefTPUJenis", "Aktif", c => c.Boolean(nullable: false));
            AddColumn("dbo.RefTPUStatus", "Aktif", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransKegiatanOutputs", "KegiatanID", "dbo.RefKegiatans");
            DropIndex("dbo.TransKegiatanOutputs", new[] { "KegiatanID" });
            DropColumn("dbo.RefTPUStatus", "Aktif");
            DropColumn("dbo.RefTPUJenis", "Aktif");
            DropTable("dbo.TransKegiatanOutputs");
        }
    }
}
