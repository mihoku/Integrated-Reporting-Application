namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schedule3 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.RefTPUs", "PKPTID");
            AddForeignKey("dbo.RefTPUs", "PKPTID", "dbo.TransSchedules", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefTPUs", "PKPTID", "dbo.TransSchedules");
            DropIndex("dbo.RefTPUs", new[] { "PKPTID" });
        }
    }
}
