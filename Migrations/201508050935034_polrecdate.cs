namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class polrecdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransKegiatanPolrecs", "SysTglEntry", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransKegiatanPolrecs", "SysTglEntry", c => c.DateTime(nullable: false));
        }
    }
}
