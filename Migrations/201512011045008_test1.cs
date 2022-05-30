namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
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
        
        public override void Down()
        {
            //DropTable("dbo.CallforReports");
        }
    }
}
