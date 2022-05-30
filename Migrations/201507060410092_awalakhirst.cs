namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class awalakhirst : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransKegiatanSTs", "TglAwal", c => c.DateTime(nullable: false));
            AddColumn("dbo.TransKegiatanSTs", "TglAkhir", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransKegiatanSTs", "TglAkhir");
            DropColumn("dbo.TransKegiatanSTs", "TglAwal");
        }
    }
}
