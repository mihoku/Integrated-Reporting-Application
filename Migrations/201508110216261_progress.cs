namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class progress : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TransKegiatanProgresses", "Tahun");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TransKegiatanProgresses", "Tahun", c => c.Int(nullable: false));
        }
    }
}
