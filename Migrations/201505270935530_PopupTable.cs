namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopupTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefPopupTexts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Airing = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RefPopupTexts");
        }
    }
}
