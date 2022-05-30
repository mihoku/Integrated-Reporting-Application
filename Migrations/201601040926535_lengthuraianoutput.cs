namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lengthuraianoutput : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TransKegiatanOutputs", "Uraian", c => c.String(maxLength: 600));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransKegiatanOutputs", "Uraian", c => c.String(maxLength: 300));
        }
    }
}
