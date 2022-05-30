namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ndflash : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransNDPermintaanFlashes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TanggalND = c.DateTime(nullable: false),
                        NomorND = c.String(maxLength: 100),
                        Tahun = c.Int(nullable: false),
                        PeriodeID = c.Int(nullable: false),
                        Locked = c.Boolean(nullable: false),
                        SysUsername = c.String(maxLength: 100),
                        SysWorkstation = c.String(maxLength: 100),
                        SysTglEntry = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefPeriodes", t => t.PeriodeID, cascadeDelete: true)
                .Index(t => t.PeriodeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransNDPermintaanFlashes", "PeriodeID", "dbo.RefPeriodes");
            DropIndex("dbo.TransNDPermintaanFlashes", new[] { "PeriodeID" });
            DropTable("dbo.TransNDPermintaanFlashes");
        }
    }
}
