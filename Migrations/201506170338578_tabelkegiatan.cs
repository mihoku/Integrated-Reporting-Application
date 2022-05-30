namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tabelkegiatan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefKegiatans", "Output", c => c.String());
            AddColumn("dbo.RefKegiatans", "Keterangan", c => c.String());
            DropColumn("dbo.RefKegiatans", "KegWaMjrID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RefKegiatans", "KegWaMjrID", c => c.Int(nullable: false));
            DropColumn("dbo.RefKegiatans", "Keterangan");
            DropColumn("dbo.RefKegiatans", "Output");
        }
    }
}
