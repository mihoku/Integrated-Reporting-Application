namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateetable150615 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RefKegiatans", "KegStatusID", "dbo.RefKegiatanStatus");
            DropIndex("dbo.RefKegiatans", new[] { "KegStatusID" });
            AddColumn("dbo.RefTPUs", "No", c => c.Int(nullable: false));
            AddColumn("dbo.TransKegiatanProgresses", "RefKegiatanStatus_ID", c => c.Int());
            CreateIndex("dbo.TransKegiatanProgresses", "RefKegiatanStatus_ID");
            AddForeignKey("dbo.TransKegiatanProgresses", "RefKegiatanStatus_ID", "dbo.RefKegiatanStatus", "ID");
            DropColumn("dbo.RefKegiatans", "KegStatusID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RefKegiatans", "KegStatusID", c => c.Int(nullable: false));
            DropForeignKey("dbo.TransKegiatanProgresses", "RefKegiatanStatus_ID", "dbo.RefKegiatanStatus");
            DropIndex("dbo.TransKegiatanProgresses", new[] { "RefKegiatanStatus_ID" });
            DropColumn("dbo.TransKegiatanProgresses", "RefKegiatanStatus_ID");
            DropColumn("dbo.RefTPUs", "No");
            CreateIndex("dbo.RefKegiatans", "KegStatusID");
            AddForeignKey("dbo.RefKegiatans", "KegStatusID", "dbo.RefKegiatanStatus", "ID", cascadeDelete: true);
        }
    }
}
