namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KegiatanFinalizeModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Finalize = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KegiatanFinalizeModels");
        }
    }
}
