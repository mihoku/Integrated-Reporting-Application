namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accept : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransIkhtisarProgresses", "Accepted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransIkhtisarProgresses", "Accepted");
        }
    }
}
