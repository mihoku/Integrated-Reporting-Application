namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conversion : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransKegiatanSTs", "TanggalST", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransKegiatanSTs", "TanggalST", c => c.DateTime(nullable: false));
        }
    }
}
