namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalizetpu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefTPUs", "Finalize", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefTPUs", "Finalize");
        }
    }
}
