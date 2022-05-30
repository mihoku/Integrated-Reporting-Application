namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetable15061501 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransKegiatanProgresses", "RefKegiatanStatus_ID", "dbo.RefKegiatanStatus");
            DropIndex("dbo.TransKegiatanProgresses", new[] { "RefKegiatanStatus_ID" });
            RenameColumn(table: "dbo.TransKegiatanProgresses", name: "RefKegiatanStatus_ID", newName: "KegStatusID");
            AlterColumn("dbo.TransKegiatanProgresses", "KegStatusID", c => c.Int(nullable: false));
            CreateIndex("dbo.TransKegiatanProgresses", "KegStatusID");
            AddForeignKey("dbo.TransKegiatanProgresses", "KegStatusID", "dbo.RefKegiatanStatus", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransKegiatanProgresses", "KegStatusID", "dbo.RefKegiatanStatus");
            DropIndex("dbo.TransKegiatanProgresses", new[] { "KegStatusID" });
            AlterColumn("dbo.TransKegiatanProgresses", "KegStatusID", c => c.Int());
            RenameColumn(table: "dbo.TransKegiatanProgresses", name: "KegStatusID", newName: "RefKegiatanStatus_ID");
            CreateIndex("dbo.TransKegiatanProgresses", "RefKegiatanStatus_ID");
            AddForeignKey("dbo.TransKegiatanProgresses", "RefKegiatanStatus_ID", "dbo.RefKegiatanStatus", "ID");
        }
    }
}
