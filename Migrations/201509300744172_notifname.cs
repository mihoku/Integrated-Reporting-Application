namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notifname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransNotifikasis", "name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransNotifikasis", "name");
        }
    }
}
