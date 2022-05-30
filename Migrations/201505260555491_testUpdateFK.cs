namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testUpdateFK : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.RefPegawais", "PegUnitID");
            AddForeignKey("dbo.RefPegawais", "PegUnitID", "dbo.RefUnitPJs", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefPegawais", "PegUnitID", "dbo.RefUnitPJs");
            DropIndex("dbo.RefPegawais", new[] { "PegUnitID" });
        }
    }
}
