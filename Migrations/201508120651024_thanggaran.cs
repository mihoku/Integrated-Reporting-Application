namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thanggaran : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RefTPUs", "TPUThAnggaran");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RefTPUs", "TPUThAnggaran", c => c.Int(nullable: false));
        }
    }
}
