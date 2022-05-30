namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class periodeflashcancel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransFlashKegiatanProgresses", "Period", "dbo.RefPeriodes");
            DropIndex("dbo.TransFlashKegiatanProgresses", new[] { "Period" });
            AddColumn("dbo.TransFlashKegiatanProgresses", "Tanggal", c => c.DateTime(nullable: false));
            DropColumn("dbo.TransFlashKegiatanProgresses", "Period");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransFlashKegiatanProgresses", "Period", c => c.Int(nullable: false));
            DropColumn("dbo.TransFlashKegiatanProgresses", "Tanggal");
            CreateIndex("dbo.TransFlashKegiatanProgresses", "Period");
            AddForeignKey("dbo.TransFlashKegiatanProgresses", "Period", "dbo.RefPeriodes", "ID", cascadeDelete: true);
        }
    }
}
