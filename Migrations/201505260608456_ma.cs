namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ma : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RefPegawais", "PegUnitID", "dbo.RefUnitPJs");
            AddForeignKey("dbo.RefPegawais", "PegUnitID", "dbo.RefUnitPJs", "ID");
            
        }

        public override void Down()
        {
            //DropForeignKey("dbo.RefPegawais", "PegUnitID", "dbo.RefUnitPJs");
        }
    }
}
