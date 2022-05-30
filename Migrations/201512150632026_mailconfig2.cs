namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mailconfig2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConfigMails", "Host", c => c.String());
            AddColumn("dbo.ConfigMails", "Port", c => c.Int(nullable: false));
            AddColumn("dbo.ConfigMails", "isBodyHtml", c => c.Boolean(nullable: false));
            AddColumn("dbo.ConfigMails", "useDefaultCredential", c => c.Boolean(nullable: false));
            AddColumn("dbo.ConfigMails", "enableSSL", c => c.Boolean(nullable: false));
            DropTable("dbo.ConfigMailAdvanceds");
        }
        
        public override void Down()
        {
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
            
            DropColumn("dbo.ConfigMails", "enableSSL");
            DropColumn("dbo.ConfigMails", "useDefaultCredential");
            DropColumn("dbo.ConfigMails", "isBodyHtml");
            DropColumn("dbo.ConfigMails", "Port");
            DropColumn("dbo.ConfigMails", "Host");
        }
    }
}
