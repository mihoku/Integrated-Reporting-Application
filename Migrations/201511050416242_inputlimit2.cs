namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inputlimit2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RefFlashKegiatanStatus", "Ket", c => c.String(maxLength: 60));
            AlterColumn("dbo.TransKegiatanSTs", "JudulST", c => c.String(maxLength: 500));
            AlterColumn("dbo.RefTPUJenis", "JenisDetail", c => c.String(maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RefTPUJenis", "JenisDetail", c => c.String(maxLength: 20));
            AlterColumn("dbo.TransKegiatanSTs", "JudulST", c => c.String(maxLength: 300));
            AlterColumn("dbo.RefFlashKegiatanStatus", "Ket", c => c.String(maxLength: 30));
        }
    }
}
