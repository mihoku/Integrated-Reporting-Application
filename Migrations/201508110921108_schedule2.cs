namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schedule2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefTPUs", "PKPTID", c => c.Int(nullable: false));
            CreateIndex("dbo.RefTPUs", "PKPTID");
            AddForeignKey("dbo.RefTPUs", "PKPTID", "dbo.TransSchedules", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefTPUs", "PKPTID", "dbo.TransSchedules");
            DropIndex("dbo.RefTPUs", new[] { "PKPTID" });
            DropColumn("dbo.RefTPUs", "PKPTID");
        }
    }
}
