namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class progressperiod : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.TransKegiatanProgresses", "Period");
            AddForeignKey("dbo.TransKegiatanProgresses", "Period", "dbo.RefPeriodes", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransKegiatanProgresses", "Period", "dbo.RefPeriodes");
            DropIndex("dbo.TransKegiatanProgresses", new[] { "Period" });
        }
    }
}
