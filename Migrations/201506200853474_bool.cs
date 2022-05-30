namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _bool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefKegiatanStatus", "Aktif", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefKegiatanStatus", "Aktif");
        }
    }
}
