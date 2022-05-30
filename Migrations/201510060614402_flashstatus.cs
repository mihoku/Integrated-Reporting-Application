namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flashstatus : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TransFlashKegiatans", "Finalize");
            AddForeignKey("dbo.TransFlashKegiatans", "Finalize", "dbo.RefFlashKegiatanStatus", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransFlashKegiatans", "Finalize", "dbo.RefFlashKegiatanStatus");
            DropIndex("dbo.TransFlashKegiatans", new[] { "Finalize" });
        }
    }
}
