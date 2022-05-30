namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class audituniverse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefUniverseAudits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ket = c.String(maxLength: 100),
                        Aktif = c.Boolean(nullable: false),
                        UnitID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefUnitPJs", t => t.UnitID, cascadeDelete: true)
                .Index(t => t.UnitID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefUniverseAudits", "UnitID", "dbo.RefUnitPJs");
            DropIndex("dbo.RefUniverseAudits", new[] { "UnitID" });
            DropTable("dbo.RefUniverseAudits");
        }
    }
}
