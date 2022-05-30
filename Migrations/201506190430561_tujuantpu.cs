namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tujuantpu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransTPUTujuans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TujuanTPU = c.String(),
                        TPUID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RefTPUs", t => t.TPUID, cascadeDelete: true)
                .Index(t => t.TPUID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransTPUTujuans", "TPUID", "dbo.RefTPUs");
            DropIndex("dbo.TransTPUTujuans", new[] { "TPUID" });
            DropTable("dbo.TransTPUTujuans");
        }
    }
}
