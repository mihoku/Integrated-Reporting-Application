namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class komentar : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransKegiatanKomentars", "KomenKegID", "dbo.RefKegiatans");
            DropIndex("dbo.TransKegiatanKomentars", new[] { "KomenKegID" });
            AlterColumn("dbo.TransKegiatanKomentars", "KomenKegID", c => c.Int(nullable: false));
            CreateIndex("dbo.TransKegiatanKomentars", "KomenKegID");
            AddForeignKey("dbo.TransKegiatanKomentars", "KomenKegID", "dbo.RefKegiatans", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransKegiatanKomentars", "KomenKegID", "dbo.RefKegiatans");
            DropIndex("dbo.TransKegiatanKomentars", new[] { "KomenKegID" });
            AlterColumn("dbo.TransKegiatanKomentars", "KomenKegID", c => c.Int());
            CreateIndex("dbo.TransKegiatanKomentars", "KomenKegID");
            AddForeignKey("dbo.TransKegiatanKomentars", "KomenKegID", "dbo.RefKegiatans", "ID");
        }
    }
}
