namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mailconfig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConfigMails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ConfigMailAdvanceds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Host = c.String(),
                        Port = c.Int(nullable: false),
                        isBodyHtml = c.Boolean(nullable: false),
                        useDefaultCredential = c.Boolean(nullable: false),
                        enableSSL = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ConfigMailAdvanceds");
            DropTable("dbo.ConfigMails");
        }
    }
}
