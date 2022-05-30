namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ikhtisar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransIkhtisarProgresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(maxLength: 1900),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TransIkhtisarProgresses");
        }
    }
}
