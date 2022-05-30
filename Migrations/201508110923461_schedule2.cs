namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schedule2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefTPUs", "PKPTID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefTPUs", "PKPTID");
        }
    }
}
