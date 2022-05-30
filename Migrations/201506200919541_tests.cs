namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefKegiatanOutputJenis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ket = c.String(),
                        Aktif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.TransKegiatanOutputs", "RefKegiatanOutputJenis_ID", c => c.Int());
            CreateIndex("dbo.TransKegiatanOutputs", "RefKegiatanOutputJenis_ID");
            AddForeignKey("dbo.TransKegiatanOutputs", "RefKegiatanOutputJenis_ID", "dbo.RefKegiatanOutputJenis", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransKegiatanOutputs", "RefKegiatanOutputJenis_ID", "dbo.RefKegiatanOutputJenis");
            DropIndex("dbo.TransKegiatanOutputs", new[] { "RefKegiatanOutputJenis_ID" });
            DropColumn("dbo.TransKegiatanOutputs", "RefKegiatanOutputJenis_ID");
            DropTable("dbo.RefKegiatanOutputJenis");
        }
    }
}
