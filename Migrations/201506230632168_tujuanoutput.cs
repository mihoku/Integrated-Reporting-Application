namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tujuanoutput : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RefTPUs", "TPUTujuan");
            DropColumn("dbo.RefKegiatans", "KegOutput");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RefKegiatans", "KegOutput", c => c.String());
            AddColumn("dbo.RefTPUs", "TPUTujuan", c => c.String(nullable: false));
        }
    }
}
