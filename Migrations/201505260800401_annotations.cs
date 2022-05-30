namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class annotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RefPegawais", "PegNIP", c => c.String(maxLength: 18));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RefPegawais", "PegNIP", c => c.Double(nullable: false));
        }
    }
}
