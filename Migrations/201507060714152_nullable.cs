namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransKegiatanSTs", "TglAwal", c => c.DateTime());
            AlterColumn("dbo.TransKegiatanSTs", "TglAkhir", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransKegiatanSTs", "TglAkhir", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TransKegiatanSTs", "TglAwal", c => c.DateTime(nullable: false));
        }
    }
}
