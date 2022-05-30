namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RefTPUs", "No", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RefTPUs", "No", c => c.Int(nullable: false));
        }
    }
}
