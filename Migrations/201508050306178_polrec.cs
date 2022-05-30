namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class polrec : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransKegiatanPolrecs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Uraian = c.String(),
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
            DropForeignKey("dbo.TransKegiatanPolrecs", "KegiatanID", "dbo.RefKegiatans");
            DropIndex("dbo.TransKegiatanPolrecs", new[] { "KegiatanID" });
            DropTable("dbo.TransKegiatanPolrecs");
        }
    }
}
