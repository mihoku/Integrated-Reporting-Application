namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zzz : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.RefTPUs", "TPUEselon1ID");
            AddForeignKey("dbo.RefTPUs", "TPUEselon1ID", "dbo.RefEselon1", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefTPUs", "TPUEselon1ID", "dbo.RefEselon1");
            DropIndex("dbo.RefTPUs", new[] { "TPUEselon1ID" });
        }
    }
}
