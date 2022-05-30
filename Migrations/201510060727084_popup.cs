namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class popup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefPopupTexts", "ModulID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefPopupTexts", "ModulID");
        }
    }
}
