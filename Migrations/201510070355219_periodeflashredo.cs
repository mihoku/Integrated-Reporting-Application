namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class periodeflashredo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransFlashKegiatanProgresses", "Period", c => c.Int(nullable: false));
            AddColumn("dbo.TransFlashKegiatanProgresses", "Tahun", c => c.Int(nullable: false));
            CreateIndex("dbo.TransFlashKegiatanProgresses", "Period");
            AddForeignKey("dbo.TransFlashKegiatanProgresses", "Period", "dbo.RefPeriodes", "ID", cascadeDelete: true);
            DropColumn("dbo.TransFlashKegiatanProgresses", "Tanggal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransFlashKegiatanProgresses", "Tanggal", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.TransFlashKegiatanProgresses", "Period", "dbo.RefPeriodes");
            DropIndex("dbo.TransFlashKegiatanProgresses", new[] { "Period" });
            DropColumn("dbo.TransFlashKegiatanProgresses", "Tahun");
            DropColumn("dbo.TransFlashKegiatanProgresses", "Period");
        }
    }
}
