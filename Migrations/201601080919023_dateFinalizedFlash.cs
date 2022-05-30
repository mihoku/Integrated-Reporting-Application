namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateFinalizedFlash : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransFlashKegiatans", "DateFinalized", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransFlashKegiatans", "DateFinalized");
        }
    }
}
