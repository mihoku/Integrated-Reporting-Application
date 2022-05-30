namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inputlimit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RefEselon1", "Ket", c => c.String(maxLength: 100));
            AlterColumn("dbo.RefTPUs", "TPUName", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.RefTPUs", "SysUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.RefTPUs", "SysWorkstation", c => c.String(maxLength: 100));
            AlterColumn("dbo.RefKegiatans", "KegName", c => c.String(maxLength: 200));
            AlterColumn("dbo.RefKegiatans", "Keterangan", c => c.String(maxLength: 300));
            AlterColumn("dbo.RefKegiatans", "SysUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.RefKegiatans", "SysWorkstation", c => c.String(maxLength: 100));
            AlterColumn("dbo.RefUnitPJs", "Detail", c => c.String(maxLength: 60));
            AlterColumn("dbo.RefPeriodes", "Ket", c => c.String(maxLength: 20));
            AlterColumn("dbo.TransFlashKegiatanProgresses", "Detail", c => c.String(maxLength: 300));
            AlterColumn("dbo.TransFlashKegiatanProgresses", "SysUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransFlashKegiatanProgresses", "SysWorkstation", c => c.String(maxLength: 100));
            AlterColumn("dbo.RefFlashKegiatanStatus", "Ket", c => c.String(maxLength: 60));
            AlterColumn("dbo.TransFlashKegiatans", "Judul", c => c.String(maxLength: 200));
            AlterColumn("dbo.TransFlashKegiatans", "SysUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransFlashKegiatans", "SysWorkstation", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransFlashKegiatanKomentars", "KomenUserID", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransFlashKegiatanKomentars", "KomenIsi", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransFlashKegiatanKomentars", "SysUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransFlashKegiatanKomentars", "SysWorkstation", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransHighlights", "SysUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransHighlights", "SysWorkstation", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanProgresses", "Detail", c => c.String(maxLength: 300));
            AlterColumn("dbo.TransKegiatanProgresses", "SysUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanProgresses", "SysWorkstation", c => c.String(maxLength: 100));
            AlterColumn("dbo.RefKegiatanStatus", "Ket", c => c.String(maxLength: 30));
            AlterColumn("dbo.TransKegiatanHS", "Hambatan", c => c.String(maxLength: 200));
            AlterColumn("dbo.TransKegiatanHS", "Solusi", c => c.String(maxLength: 200));
            AlterColumn("dbo.TransKegiatanHS", "SysUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanHS", "SysWorkstation", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanKomentars", "KomenUserID", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanKomentars", "KomenIsi", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanKomentars", "SysUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanKomentars", "SysWorkstation", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanOutputs", "Nomor", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanOutputs", "Judul", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanOutputs", "Uraian", c => c.String(maxLength: 300));
            AlterColumn("dbo.TransKegiatanOutputs", "SysUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanOutputs", "SysWorkstation", c => c.String(maxLength: 100));
            AlterColumn("dbo.RefKegiatanOutputJenis", "Ket", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanPolrecs", "Judul", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanPolrecs", "Uraian", c => c.String(maxLength: 300));
            AlterColumn("dbo.TransKegiatanPolrecs", "SysUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanPolrecs", "SysWorkstation", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanSTs", "NoST", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanSTs", "JudulST", c => c.String(maxLength: 500));
            AlterColumn("dbo.TransKegiatanSTs", "SysUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransKegiatanSTs", "SysWorkstation", c => c.String(maxLength: 100));
            AlterColumn("dbo.RefQuarters", "QuarterDetails", c => c.String(maxLength: 20));
            AlterColumn("dbo.RefTPUJenis", "JenisDetail", c => c.String(maxLength: 40));
            AlterColumn("dbo.RefTPUStatus", "Ket", c => c.String(maxLength: 30));
            AlterColumn("dbo.TransSchedules", "Title", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransTPUTujuans", "TujuanTPU", c => c.String(maxLength: 300));
            AlterColumn("dbo.TransTPUTujuans", "SysUsername", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransTPUTujuans", "SysWorkstation", c => c.String(maxLength: 100));
            AlterColumn("dbo.RefPopupTexts", "Message", c => c.String(maxLength: 300));
            AlterColumn("dbo.TransFlashNotifClicks", "UserName", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransFlashNotifikasis", "body", c => c.String(maxLength: 300));
            AlterColumn("dbo.TransFlashNotifikasis", "Controller", c => c.String(maxLength: 20));
            AlterColumn("dbo.TransFlashNotifikasis", "name", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransFlashNotifikasis", "Action", c => c.String(maxLength: 20));
            AlterColumn("dbo.TransNotifClicks", "UserName", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransNotifikasis", "body", c => c.String(maxLength: 300));
            AlterColumn("dbo.TransNotifikasis", "Controller", c => c.String(maxLength: 20));
            AlterColumn("dbo.TransNotifikasis", "name", c => c.String(maxLength: 100));
            AlterColumn("dbo.TransNotifikasis", "Action", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TransNotifikasis", "Action", c => c.String());
            AlterColumn("dbo.TransNotifikasis", "name", c => c.String());
            AlterColumn("dbo.TransNotifikasis", "Controller", c => c.String());
            AlterColumn("dbo.TransNotifikasis", "body", c => c.String());
            AlterColumn("dbo.TransNotifClicks", "UserName", c => c.String());
            AlterColumn("dbo.TransFlashNotifikasis", "Action", c => c.String());
            AlterColumn("dbo.TransFlashNotifikasis", "name", c => c.String());
            AlterColumn("dbo.TransFlashNotifikasis", "Controller", c => c.String());
            AlterColumn("dbo.TransFlashNotifikasis", "body", c => c.String());
            AlterColumn("dbo.TransFlashNotifClicks", "UserName", c => c.String());
            AlterColumn("dbo.RefPopupTexts", "Message", c => c.String());
            AlterColumn("dbo.TransTPUTujuans", "SysWorkstation", c => c.String());
            AlterColumn("dbo.TransTPUTujuans", "SysUsername", c => c.String());
            AlterColumn("dbo.TransTPUTujuans", "TujuanTPU", c => c.String());
            AlterColumn("dbo.TransSchedules", "Title", c => c.String());
            AlterColumn("dbo.RefTPUStatus", "Ket", c => c.String());
            AlterColumn("dbo.RefTPUJenis", "JenisDetail", c => c.String());
            AlterColumn("dbo.RefQuarters", "QuarterDetails", c => c.String());
            AlterColumn("dbo.TransKegiatanSTs", "SysWorkstation", c => c.String());
            AlterColumn("dbo.TransKegiatanSTs", "SysUsername", c => c.String());
            AlterColumn("dbo.TransKegiatanSTs", "JudulST", c => c.String());
            AlterColumn("dbo.TransKegiatanSTs", "NoST", c => c.String());
            AlterColumn("dbo.TransKegiatanPolrecs", "SysWorkstation", c => c.String());
            AlterColumn("dbo.TransKegiatanPolrecs", "SysUsername", c => c.String());
            AlterColumn("dbo.TransKegiatanPolrecs", "Uraian", c => c.String());
            AlterColumn("dbo.TransKegiatanPolrecs", "Judul", c => c.String());
            AlterColumn("dbo.RefKegiatanOutputJenis", "Ket", c => c.String());
            AlterColumn("dbo.TransKegiatanOutputs", "SysWorkstation", c => c.String());
            AlterColumn("dbo.TransKegiatanOutputs", "SysUsername", c => c.String());
            AlterColumn("dbo.TransKegiatanOutputs", "Uraian", c => c.String());
            AlterColumn("dbo.TransKegiatanOutputs", "Judul", c => c.String());
            AlterColumn("dbo.TransKegiatanOutputs", "Nomor", c => c.String());
            AlterColumn("dbo.TransKegiatanKomentars", "SysWorkstation", c => c.String());
            AlterColumn("dbo.TransKegiatanKomentars", "SysUsername", c => c.String());
            AlterColumn("dbo.TransKegiatanKomentars", "KomenIsi", c => c.String());
            AlterColumn("dbo.TransKegiatanKomentars", "KomenUserID", c => c.String());
            AlterColumn("dbo.TransKegiatanHS", "SysWorkstation", c => c.String());
            AlterColumn("dbo.TransKegiatanHS", "SysUsername", c => c.String());
            AlterColumn("dbo.TransKegiatanHS", "Solusi", c => c.String());
            AlterColumn("dbo.TransKegiatanHS", "Hambatan", c => c.String());
            AlterColumn("dbo.RefKegiatanStatus", "Ket", c => c.String());
            AlterColumn("dbo.TransKegiatanProgresses", "SysWorkstation", c => c.String());
            AlterColumn("dbo.TransKegiatanProgresses", "SysUsername", c => c.String());
            AlterColumn("dbo.TransKegiatanProgresses", "Detail", c => c.String());
            AlterColumn("dbo.TransHighlights", "SysWorkstation", c => c.String());
            AlterColumn("dbo.TransHighlights", "SysUsername", c => c.String());
            AlterColumn("dbo.TransFlashKegiatanKomentars", "SysWorkstation", c => c.String());
            AlterColumn("dbo.TransFlashKegiatanKomentars", "SysUsername", c => c.String());
            AlterColumn("dbo.TransFlashKegiatanKomentars", "KomenIsi", c => c.String());
            AlterColumn("dbo.TransFlashKegiatanKomentars", "KomenUserID", c => c.String());
            AlterColumn("dbo.TransFlashKegiatans", "SysWorkstation", c => c.String());
            AlterColumn("dbo.TransFlashKegiatans", "SysUsername", c => c.String());
            AlterColumn("dbo.TransFlashKegiatans", "Judul", c => c.String());
            AlterColumn("dbo.RefFlashKegiatanStatus", "Ket", c => c.String());
            AlterColumn("dbo.TransFlashKegiatanProgresses", "SysWorkstation", c => c.String());
            AlterColumn("dbo.TransFlashKegiatanProgresses", "SysUsername", c => c.String());
            AlterColumn("dbo.TransFlashKegiatanProgresses", "Detail", c => c.String());
            AlterColumn("dbo.RefPeriodes", "Ket", c => c.String());
            AlterColumn("dbo.RefUnitPJs", "Detail", c => c.String());
            AlterColumn("dbo.RefKegiatans", "SysWorkstation", c => c.String());
            AlterColumn("dbo.RefKegiatans", "SysUsername", c => c.String());
            AlterColumn("dbo.RefKegiatans", "Keterangan", c => c.String());
            AlterColumn("dbo.RefKegiatans", "KegName", c => c.String());
            AlterColumn("dbo.RefTPUs", "SysWorkstation", c => c.String());
            AlterColumn("dbo.RefTPUs", "SysUsername", c => c.String());
            AlterColumn("dbo.RefTPUs", "TPUName", c => c.String(nullable: false));
            AlterColumn("dbo.RefEselon1", "Ket", c => c.String());
        }
    }
}