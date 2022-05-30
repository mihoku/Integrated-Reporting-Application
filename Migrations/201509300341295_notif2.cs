namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notif2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransNotifikasis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        body = c.String(),
                        Date = c.DateTime(nullable: false),
                        Controller = c.String(),
                        Action = c.String(),
                        RouteID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TransNotifikasis");
        }
    }
}
