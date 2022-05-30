namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cancelprev : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RefKegiatans", "KegTarget", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RefKegiatans", "KegTarget", c => c.DateTime());
        }
    }
}
