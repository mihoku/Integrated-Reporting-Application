namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plisisokta : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.RefPegawais", "PegUnitID", "dbo.RefUnitPJs", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefPegawais", "PegUnitID", "dbo.RefUnitPJs");
        }
    }
}
