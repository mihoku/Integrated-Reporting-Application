namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RefPegawais", "PegUnitID", "dbo.RefUnitPJs");
            DropIndex("dbo.RefPegawais", new[] { "PegUnitID" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.RefPegawais", "PegUnitID");
            AddForeignKey("dbo.RefPegawais", "PegUnitID", "dbo.RefUnitPJs", "ID", cascadeDelete: true);
        }
    }
}
