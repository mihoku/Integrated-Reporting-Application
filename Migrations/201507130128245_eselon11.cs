namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eselon11 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RefTPUs", "TPUEselon1ID");
            RenameColumn(table: "dbo.RefTPUs", name: "RefEselon1_ID", newName: "TPUEselon1ID");
            RenameIndex(table: "dbo.RefTPUs", name: "IX_RefEselon1_ID", newName: "IX_TPUEselon1ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.RefTPUs", name: "IX_TPUEselon1ID", newName: "IX_RefEselon1_ID");
            RenameColumn(table: "dbo.RefTPUs", name: "TPUEselon1ID", newName: "RefEselon1_ID");
            AddColumn("dbo.RefTPUs", "TPUEselon1ID", c => c.Int());
        }
    }
}
