namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class highlightv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransHighlights",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Tahun = c.Int(nullable: false),
                        Period = c.Int(nullable: false),
                        KegiatanID = c.Int(nullable: false),
                        SysUsername = c.String(),
                        SysTglEntry = c.DateTime(nullable: false),
                        SysWorkstation = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefKegiatans", t => t.KegiatanID, cascadeDelete: true)
                .ForeignKey("dbo.RefPeriodes", t => t.Period, cascadeDelete: true)
                .Index(t => t.Period)
                .Index(t => t.KegiatanID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransHighlights", "Period", "dbo.RefPeriodes");
            DropForeignKey("dbo.TransHighlights", "KegiatanID", "dbo.RefKegiatans");
            DropIndex("dbo.TransHighlights", new[] { "KegiatanID" });
            DropIndex("dbo.TransHighlights", new[] { "Period" });
            DropTable("dbo.TransHighlights");
        }
    }
}
