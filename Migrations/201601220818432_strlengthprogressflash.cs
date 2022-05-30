namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class strlengthprogressflash : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransFlashKegiatanProgresses", "Detail", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransFlashKegiatanProgresses", "Detail", c => c.String(maxLength: 300));
        }
    }
}
