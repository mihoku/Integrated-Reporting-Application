namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class manajerkegiatan : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.RefKegiatans", "KegMjrID");
            AddForeignKey("dbo.RefKegiatans", "KegMjrID", "dbo.RefPegawais", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefKegiatans", "KegMjrID", "dbo.RefPegawais");
            DropIndex("dbo.RefKegiatans", new[] { "KegMjrID" });
        }
    }
}
