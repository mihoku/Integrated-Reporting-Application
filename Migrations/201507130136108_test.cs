namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RefTPUs", "TPUEselon1ID", "dbo.RefEselon1");
            DropIndex("dbo.RefTPUs", new[] { "TPUEselon1ID" });
            AlterColumn("dbo.RefTPUs", "TPUEselon1ID", c => c.Int(nullable: false));
            CreateIndex("dbo.RefTPUs", "TPUEselon1ID");
            AddForeignKey("dbo.RefTPUs", "TPUEselon1ID", "dbo.RefEselon1", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefTPUs", "TPUEselon1ID", "dbo.RefEselon1");
            DropIndex("dbo.RefTPUs", new[] { "TPUEselon1ID" });
            AlterColumn("dbo.RefTPUs", "TPUEselon1ID", c => c.Int());
            CreateIndex("dbo.RefTPUs", "TPUEselon1ID");
            AddForeignKey("dbo.RefTPUs", "TPUEselon1ID", "dbo.RefEselon1", "ID");
        }
    }
}
