namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hambatansolusitable : DbMigration
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
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefKegiatans", t => t.KegiatanID, cascadeDelete: true)
                .Index(t => t.KegiatanID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransKegiatanHS", "KegiatanID", "dbo.RefKegiatans");
            DropIndex("dbo.TransKegiatanHS", new[] { "KegiatanID" });
            DropTable("dbo.TransKegiatanHS");
        }
    }
}
