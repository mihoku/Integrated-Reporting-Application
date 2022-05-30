namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eselon1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RefTPUs", "TPUEselon1ID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RefTPUs", "TPUEselon1ID");
        }
    }
}
