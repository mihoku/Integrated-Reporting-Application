namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class period : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RefPeriodes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ket = c.String(),
                        Aktif = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RefPeriodes");
        }
    }
}
