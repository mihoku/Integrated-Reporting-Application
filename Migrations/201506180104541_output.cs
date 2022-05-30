namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class output : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RefKegiatans", "Output");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RefKegiatans", "Output", c => c.String());
        }
    }
}
