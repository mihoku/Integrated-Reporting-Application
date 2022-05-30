namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notiftype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransNotifikasis", "NotifType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransNotifikasis", "NotifType");
        }
    }
}
