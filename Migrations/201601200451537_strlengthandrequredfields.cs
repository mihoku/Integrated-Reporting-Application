namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class strlengthandrequredfields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransFlashKegiatans", "Judul", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.TransFlashKegiatanOutputs", "Nomor", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.TransFlashKegiatanOutputs", "Judul", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.TransFlashKegiatanOutputs", "Uraian", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.TransKegiatanOutputs", "Nomor", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.TransKegiatanOutputs", "Judul", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.TransKegiatanOutputs", "Uraian", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.TransKegiatanHS", "Hambatan", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.TransKegiatanHS", "Solusi", c => c.String(maxLength: 500));
            AlterColumn("dbo.TransKegiatanPolrecs", "Judul", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.TransKegiatanPolrecs", "Uraian", c => c.String(nullable: false, maxLength: 1000));
            AlterColumn("dbo.TransKegiatanSTs", "NoST", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.TransKegiatanSTs", "JudulST", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransKegiatanSTs", "JudulST", c => c.String(maxLength: 500));
            AlterColumn("dbo.TransKegiatanSTs", "NoST", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanPolrecs", "Uraian", c => c.String(maxLength: 300));
            AlterColumn("dbo.TransKegiatanPolrecs", "Judul", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanHS", "Solusi", c => c.String(maxLength: 200));
            AlterColumn("dbo.TransKegiatanHS", "Hambatan", c => c.String(maxLength: 200));
            AlterColumn("dbo.TransKegiatanOutputs", "Uraian", c => c.String(maxLength: 600));
            AlterColumn("dbo.TransKegiatanOutputs", "Judul", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanOutputs", "Nomor", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransFlashKegiatanOutputs", "Uraian", c => c.String(maxLength: 300));
            AlterColumn("dbo.TransFlashKegiatanOutputs", "Judul", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransFlashKegiatanOutputs", "Nomor", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransFlashKegiatans", "Judul", c => c.String(maxLength: 200));
        }
    }
}
