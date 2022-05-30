namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialDeath : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ConfigMails", "Host", c => c.String(nullable: false));
            AlterColumn("dbo.RefEselon1", "Ket", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.RefKegiatans", "KegName", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.RefPegawais", "PegName", c => c.String(nullable: false));
            AlterColumn("dbo.RefPegawais", "PegNIP", c => c.String(nullable: false, maxLength: 18));
            AlterColumn("dbo.RefPegawais", "PegEmailKemenkeu", c => c.String(nullable: false));
            AlterColumn("dbo.RefUnitPJs", "Detail", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.RefUnitPJs", "DetailShort", c => c.String(nullable: false, maxLength: 7));
            AlterColumn("dbo.RefUniverseAudits", "Ket", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.RefPeriodes", "Ket", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.RefFlashKegiatanStatus", "Ket", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.RefKegiatanOutputJenis", "Ket", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.RefKegiatanStatus", "Ket", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.RefQuarters", "QuarterDetails", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.RefTPUJenis", "JenisDetail", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.RefTPUStatus", "Ket", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.RefRoles", "Detail", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RefRoles", "Detail", c => c.String());
            AlterColumn("dbo.RefTPUStatus", "Ket", c => c.String(maxLength: 30));
            AlterColumn("dbo.RefTPUJenis", "JenisDetail", c => c.String(maxLength: 40));
            AlterColumn("dbo.RefQuarters", "QuarterDetails", c => c.String(maxLength: 20));
            AlterColumn("dbo.RefKegiatanStatus", "Ket", c => c.String(maxLength: 30));
            AlterColumn("dbo.RefKegiatanOutputJenis", "Ket", c => c.String(maxLength: 100));
            AlterColumn("dbo.RefFlashKegiatanStatus", "Ket", c => c.String(maxLength: 60));
            AlterColumn("dbo.RefPeriodes", "Ket", c => c.String(maxLength: 20));
            AlterColumn("dbo.RefUniverseAudits", "Ket", c => c.String(maxLength: 100));
            AlterColumn("dbo.RefUnitPJs", "DetailShort", c => c.String(maxLength: 7));
            AlterColumn("dbo.RefUnitPJs", "Detail", c => c.String(maxLength: 60));
            AlterColumn("dbo.RefPegawais", "PegEmailKemenkeu", c => c.String());
            AlterColumn("dbo.RefPegawais", "PegNIP", c => c.String(maxLength: 18));
            AlterColumn("dbo.RefPegawais", "PegName", c => c.String());
            AlterColumn("dbo.RefKegiatans", "KegName", c => c.String(maxLength: 200));
            AlterColumn("dbo.RefEselon1", "Ket", c => c.String(maxLength: 100));
            AlterColumn("dbo.ConfigMails", "Host", c => c.String());
        }
    }
}
