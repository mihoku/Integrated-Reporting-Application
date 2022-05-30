namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class komentglnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransKegiatanKomentars", "KomenTgl", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransKegiatanKomentars", "KomenTgl", c => c.DateTime());
        }
    }
}
