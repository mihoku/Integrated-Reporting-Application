namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notifclick : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransNotifClicks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TransNotifClicks");
        }
    }
}
