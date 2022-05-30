namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class komentarlength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransFlashKegiatanKomentars", "KomenIsi", c => c.String(maxLength: 1000));
            AlterColumn("dbo.TransKegiatanKomentars", "KomenIsi", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransKegiatanKomentars", "KomenIsi", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransFlashKegiatanKomentars", "KomenIsi", c => c.String(maxLength: 100));
        }
    }
}
