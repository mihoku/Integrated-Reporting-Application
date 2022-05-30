namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test23 : DbMigration
    {
        public override void Up()
        {
            //DropTable("dbo.CallforReports");
        }
        
        public override void Down()
        {
            //CreateTable(
            //    "dbo.CallforReports",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            NomorND = c.String(),
            //            TanggalND = c.DateTime(nullable: false),
            //            PeriodeID = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
        }
    }
}
