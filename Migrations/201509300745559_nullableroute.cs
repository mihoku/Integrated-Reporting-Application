namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableroute : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransNotifikasis", "RouteID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransNotifikasis", "RouteID", c => c.Int(nullable: false));
        }
    }
}
