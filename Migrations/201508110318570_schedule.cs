namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schedule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransSchedules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Tahun = c.Int(nullable: false),
                        Locked = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TransSchedules");
        }
    }
}
