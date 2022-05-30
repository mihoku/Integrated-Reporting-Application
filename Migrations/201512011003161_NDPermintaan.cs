namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NDPermintaan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransNDPermintaans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TanggalND = c.DateTime(nullable: false),
                        NomorND = c.String(maxLength: 100),
                        PKPTID = c.Int(nullable: false),
                        PeriodeID = c.Int(nullable: false),
                        SysUsername = c.String(maxLength: 100),
                        SysWorkstation = c.String(maxLength: 100),
                        SysTglEntry = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefPeriodes", t => t.PeriodeID, cascadeDelete: true)
                .ForeignKey("dbo.TransSchedules", t => t.PKPTID, cascadeDelete: true)
                .Index(t => t.PKPTID)
                .Index(t => t.PeriodeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransNDPermintaans", "PKPTID", "dbo.TransSchedules");
            DropForeignKey("dbo.TransNDPermintaans", "PeriodeID", "dbo.RefPeriodes");
            DropIndex("dbo.TransNDPermintaans", new[] { "PeriodeID" });
            DropIndex("dbo.TransNDPermintaans", new[] { "PKPTID" });
            DropTable("dbo.TransNDPermintaans");
        }
    }
}
