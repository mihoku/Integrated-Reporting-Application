namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eselon1s : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefEselon1",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ket = c.String(),
                        Aktif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.RefTPUs", "RefEselon1_ID", c => c.Int());
            CreateIndex("dbo.RefTPUs", "RefEselon1_ID");
            AddForeignKey("dbo.RefTPUs", "RefEselon1_ID", "dbo.RefEselon1", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RefTPUs", "RefEselon1_ID", "dbo.RefEselon1");
            DropIndex("dbo.RefTPUs", new[] { "RefEselon1_ID" });
            DropColumn("dbo.RefTPUs", "RefEselon1_ID");
            DropTable("dbo.RefEselon1");
        }
    }
}
