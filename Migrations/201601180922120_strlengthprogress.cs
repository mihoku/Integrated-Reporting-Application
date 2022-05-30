namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class strlengthprogress : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransKegiatanProgresses", "Detail", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransKegiatanProgresses", "Detail", c => c.String(maxLength: 300));
        }
    }
}
