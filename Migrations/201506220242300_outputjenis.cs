namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class outputjenis : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TransKegiatanOutputs", "RefKegiatanOutputJenis_ID", "dbo.RefKegiatanOutputJenis");
            DropIndex("dbo.TransKegiatanOutputs", new[] { "RefKegiatanOutputJenis_ID" });
            RenameColumn(table: "dbo.TransKegiatanOutputs", name: "RefKegiatanOutputJenis_ID", newName: "OutputJenisID");
            AlterColumn("dbo.TransKegiatanOutputs", "OutputJenisID", c => c.Int(nullable: false));
            CreateIndex("dbo.TransKegiatanOutputs", "OutputJenisID");
            AddForeignKey("dbo.TransKegiatanOutputs", "OutputJenisID", "dbo.RefKegiatanOutputJenis", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransKegiatanOutputs", "OutputJenisID", "dbo.RefKegiatanOutputJenis");
            DropIndex("dbo.TransKegiatanOutputs", new[] { "OutputJenisID" });
            AlterColumn("dbo.TransKegiatanOutputs", "OutputJenisID", c => c.Int());
            RenameColumn(table: "dbo.TransKegiatanOutputs", name: "OutputJenisID", newName: "RefKegiatanOutputJenis_ID");
            CreateIndex("dbo.TransKegiatanOutputs", "RefKegiatanOutputJenis_ID");
            AddForeignKey("dbo.TransKegiatanOutputs", "RefKegiatanOutputJenis_ID", "dbo.RefKegiatanOutputJenis", "ID");
        }
    }
}
