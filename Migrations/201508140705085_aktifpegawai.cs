namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aktifpegawai : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefPegawais", "Aktif", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefPegawais", "Aktif");
        }
    }
}
