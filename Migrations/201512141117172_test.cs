namespace ira.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.RefEselon1",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Ket = c.String(maxLength: 100),
            //            Aktif = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.RefTPUs",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            No = c.Int(nullable: false),
            //            TPUName = c.String(nullable: false, maxLength: 300),
            //            PKPTID = c.Int(nullable: false),
            //            TPUPJID = c.Int(nullable: false),
            //            TPUQTargetID = c.Int(nullable: false),
            //            TPUStatusID = c.Int(nullable: false),
            //            TPUUnitPJID = c.Int(nullable: false),
            //            TPUJenisID = c.Int(nullable: false),
            //            TPUEselon1ID = c.Int(nullable: false),
            //            Finalize = c.Int(nullable: false),
            //            SysUsername = c.String(maxLength: 100),
            //            SysTglEntry = c.DateTime(),
            //            SysWorkstation = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.RefEselon1", t => t.TPUEselon1ID, cascadeDelete: true)
            //    .ForeignKey("dbo.RefPegawais", t => t.TPUPJID, cascadeDelete: true)
            //    .ForeignKey("dbo.RefUnitPJs", t => t.TPUUnitPJID, cascadeDelete: true)
            //    .ForeignKey("dbo.TransSchedules", t => t.PKPTID, cascadeDelete: true)
            //    .ForeignKey("dbo.RefQuarters", t => t.TPUQTargetID, cascadeDelete: true)
            //    .ForeignKey("dbo.RefTPUJenis", t => t.TPUJenisID, cascadeDelete: true)
            //    .ForeignKey("dbo.RefTPUStatus", t => t.TPUStatusID, cascadeDelete: true)
            //    .Index(t => t.PKPTID)
            //    .Index(t => t.TPUPJID)
            //    .Index(t => t.TPUQTargetID)
            //    .Index(t => t.TPUStatusID)
            //    .Index(t => t.TPUUnitPJID)
            //    .Index(t => t.TPUJenisID)
            //    .Index(t => t.TPUEselon1ID);
            
            //CreateTable(
            //    "dbo.RefKegiatans",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            KegName = c.String(maxLength: 200),
            //            KegiatanTPUID = c.Int(nullable: false),
            //            KegMjrID = c.Int(nullable: false),
            //            PeriodeID = c.Int(nullable: false),
            //            KegTarget = c.DateTime(nullable: false),
            //            Keterangan = c.String(maxLength: 300),
            //            Finalize = c.Int(nullable: false),
            //            SysUsername = c.String(maxLength: 100),
            //            SysTglEntry = c.DateTime(),
            //            SysWorkstation = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.RefPegawais", t => t.KegMjrID, cascadeDelete: true)
            //    .ForeignKey("dbo.RefPeriodes", t => t.PeriodeID, cascadeDelete: true)
            //    .ForeignKey("dbo.RefTPUs", t => t.KegiatanTPUID, cascadeDelete: true)
            //    .Index(t => t.KegiatanTPUID)
            //    .Index(t => t.KegMjrID)
            //    .Index(t => t.PeriodeID);
            
            //CreateTable(
            //    "dbo.RefPegawais",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            PegName = c.String(),
            //            PegNIP = c.String(maxLength: 18),
            //            PegUnitID = c.Int(nullable: false),
            //            Aktif = c.Boolean(nullable: false),
            //            PegEmailKemenkeu = c.String(),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.RefUnitPJs", t => t.PegUnitID, cascadeDelete: true)
            //    .Index(t => t.PegUnitID);
            
            //CreateTable(
            //    "dbo.RefUnitPJs",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Detail = c.String(maxLength: 60),
            //            Aktif = c.Boolean(nullable: false),
            //            isPrimeMover = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.RefUniverseAudits",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Ket = c.String(maxLength: 100),
            //            Aktif = c.Boolean(nullable: false),
            //            UnitID = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.RefUnitPJs", t => t.UnitID, cascadeDelete: true)
            //    .Index(t => t.UnitID);
            
            //CreateTable(
            //    "dbo.TransIkhtisarProgresses",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            RencanaKerja = c.String(maxLength: 1900),
            //            HasilPengawasan = c.String(maxLength: 1900),
            //            RencanaPengawasan = c.String(maxLength: 1900),
            //            UniverseID = c.Int(nullable: false),
            //            PKPTID = c.Int(nullable: false),
            //            PeriodeID = c.Int(nullable: false),
            //            SysUsername = c.String(maxLength: 100),
            //            SysWorkstation = c.String(maxLength: 100),
            //            SysTglEntry = c.DateTime(nullable: false),
            //            Locked = c.Boolean(nullable: false),
            //            Accepted = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.RefPeriodes", t => t.PeriodeID, cascadeDelete: true)
            //    .ForeignKey("dbo.TransSchedules", t => t.PKPTID, cascadeDelete: true)
            //    .ForeignKey("dbo.RefUniverseAudits", t => t.UniverseID, cascadeDelete: true)
            //    .Index(t => t.UniverseID)
            //    .Index(t => t.PKPTID)
            //    .Index(t => t.PeriodeID);
            
            //CreateTable(
            //    "dbo.RefPeriodes",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Ket = c.String(maxLength: 20),
            //            Aktif = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.TransFlashKegiatanProgresses",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Detail = c.String(maxLength: 300),
            //            Period = c.Int(nullable: false),
            //            Tahun = c.Int(nullable: false),
            //            KegiatanID = c.Int(nullable: false),
            //            KegStatusID = c.Int(nullable: false),
            //            SysUsername = c.String(maxLength: 100),
            //            SysTglEntry = c.DateTime(nullable: false),
            //            SysWorkstation = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.TransFlashKegiatans", t => t.KegiatanID, cascadeDelete: true)
            //    .ForeignKey("dbo.RefFlashKegiatanStatus", t => t.KegStatusID, cascadeDelete: true)
            //    .ForeignKey("dbo.RefPeriodes", t => t.Period, cascadeDelete: true)
            //    .Index(t => t.Period)
            //    .Index(t => t.KegiatanID)
            //    .Index(t => t.KegStatusID);
            
            //CreateTable(
            //    "dbo.RefFlashKegiatanStatus",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Ket = c.String(maxLength: 60),
            //            Aktif = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.TransFlashKegiatans",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Judul = c.String(maxLength: 200),
            //            ManajerID = c.Int(nullable: false),
            //            UnitID = c.Int(nullable: false),
            //            TanggalKasus = c.DateTime(nullable: false),
            //            Finalize = c.Int(nullable: false),
            //            SysUsername = c.String(maxLength: 100),
            //            SysTglEntry = c.DateTime(),
            //            SysWorkstation = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.RefFlashKegiatanStatus", t => t.Finalize, cascadeDelete: true)
            //    .ForeignKey("dbo.RefPegawais", t => t.ManajerID, cascadeDelete: true)
            //    .ForeignKey("dbo.RefUnitPJs", t => t.UnitID, cascadeDelete: true)
            //    .Index(t => t.ManajerID)
            //    .Index(t => t.UnitID)
            //    .Index(t => t.Finalize);
            
            //CreateTable(
            //    "dbo.TransFlashKegiatanKomentars",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            KomenUserID = c.String(maxLength: 100),
            //            KomenIsi = c.String(maxLength: 1000),
            //            KegiatanID = c.Int(nullable: false),
            //            KomenTgl = c.DateTime(nullable: false),
            //            SysUsername = c.String(maxLength: 100),
            //            SysTglEntry = c.DateTime(nullable: false),
            //            SysWorkstation = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.TransFlashKegiatans", t => t.KegiatanID, cascadeDelete: true)
            //    .Index(t => t.KegiatanID);
            
            //CreateTable(
            //    "dbo.TransHighlights",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Tahun = c.Int(nullable: false),
            //            Period = c.Int(nullable: false),
            //            KegiatanID = c.Int(nullable: false),
            //            SysUsername = c.String(maxLength: 100),
            //            SysTglEntry = c.DateTime(nullable: false),
            //            SysWorkstation = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.RefKegiatans", t => t.KegiatanID, cascadeDelete: true)
            //    .ForeignKey("dbo.RefPeriodes", t => t.Period, cascadeDelete: true)
            //    .Index(t => t.Period)
            //    .Index(t => t.KegiatanID);
            
            //CreateTable(
            //    "dbo.TransKegiatanProgresses",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Detail = c.String(maxLength: 300),
            //            Period = c.Int(nullable: false),
            //            KegiatanID = c.Int(nullable: false),
            //            KegStatusID = c.Int(nullable: false),
            //            SysUsername = c.String(maxLength: 100),
            //            SysTglEntry = c.DateTime(nullable: false),
            //            SysWorkstation = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.RefKegiatans", t => t.KegiatanID, cascadeDelete: true)
            //    .ForeignKey("dbo.RefKegiatanStatus", t => t.KegStatusID, cascadeDelete: true)
            //    .ForeignKey("dbo.RefPeriodes", t => t.Period, cascadeDelete: true)
            //    .Index(t => t.Period)
            //    .Index(t => t.KegiatanID)
            //    .Index(t => t.KegStatusID);
            
            //CreateTable(
            //    "dbo.RefKegiatanStatus",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Ket = c.String(maxLength: 30),
            //            Aktif = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.TransNDPermintaans",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            TanggalND = c.DateTime(nullable: false),
            //            NomorND = c.String(maxLength: 100),
            //            PKPTID = c.Int(nullable: false),
            //            PeriodeID = c.Int(nullable: false),
            //            Locked = c.Boolean(nullable: false),
            //            SysUsername = c.String(maxLength: 100),
            //            SysWorkstation = c.String(maxLength: 100),
            //            SysTglEntry = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.RefPeriodes", t => t.PeriodeID, cascadeDelete: true)
            //    .ForeignKey("dbo.TransSchedules", t => t.PKPTID, cascadeDelete: true)
            //    .Index(t => t.PKPTID)
            //    .Index(t => t.PeriodeID);
            
            //CreateTable(
            //    "dbo.TransSchedules",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Title = c.String(maxLength: 100),
            //            Tahun = c.Int(nullable: false),
            //            Locked = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.TransKegiatanHS",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Hambatan = c.String(maxLength: 200),
            //            Solusi = c.String(maxLength: 200),
            //            KegiatanID = c.Int(nullable: false),
            //            SysUsername = c.String(maxLength: 100),
            //            SysTglEntry = c.DateTime(nullable: false),
            //            SysWorkstation = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.RefKegiatans", t => t.KegiatanID, cascadeDelete: true)
            //    .Index(t => t.KegiatanID);
            
            //CreateTable(
            //    "dbo.TransKegiatanKomentars",
            //    c => new
            //        {
            //            KomenID = c.Int(nullable: false, identity: true),
            //            KomenUserID = c.String(maxLength: 100),
            //            KomenIsi = c.String(maxLength: 1000),
            //            KomenKegID = c.Int(nullable: false),
            //            KomenTgl = c.DateTime(nullable: false),
            //            SysUsername = c.String(maxLength: 100),
            //            SysTglEntry = c.DateTime(nullable: false),
            //            SysWorkstation = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.KomenID)
            //    .ForeignKey("dbo.RefKegiatans", t => t.KomenKegID, cascadeDelete: true)
            //    .Index(t => t.KomenKegID);
            
            //CreateTable(
            //    "dbo.TransKegiatanOutputs",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Nomor = c.String(maxLength: 100),
            //            TanggalTerbit = c.DateTime(nullable: false),
            //            Judul = c.String(maxLength: 100),
            //            Uraian = c.String(maxLength: 300),
            //            KegiatanID = c.Int(nullable: false),
            //            OutputJenisID = c.Int(nullable: false),
            //            SysUsername = c.String(maxLength: 100),
            //            SysTglEntry = c.DateTime(nullable: false),
            //            SysWorkstation = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.RefKegiatans", t => t.KegiatanID, cascadeDelete: true)
            //    .ForeignKey("dbo.RefKegiatanOutputJenis", t => t.OutputJenisID, cascadeDelete: true)
            //    .Index(t => t.KegiatanID)
            //    .Index(t => t.OutputJenisID);
            
            //CreateTable(
            //    "dbo.RefKegiatanOutputJenis",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Ket = c.String(maxLength: 100),
            //            Aktif = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.TransKegiatanPolrecs",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Judul = c.String(maxLength: 100),
            //            Uraian = c.String(maxLength: 300),
            //            KegiatanID = c.Int(nullable: false),
            //            SysUsername = c.String(maxLength: 100),
            //            SysTglEntry = c.DateTime(),
            //            SysWorkstation = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.RefKegiatans", t => t.KegiatanID, cascadeDelete: true)
            //    .Index(t => t.KegiatanID);
            
            //CreateTable(
            //    "dbo.TransKegiatanSTs",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            NoST = c.String(maxLength: 100),
            //            JudulST = c.String(maxLength: 500),
            //            Tahun = c.Int(nullable: false),
            //            TanggalST = c.DateTime(),
            //            KegiatanID = c.Int(nullable: false),
            //            TglAwal = c.DateTime(),
            //            TglAkhir = c.DateTime(),
            //            SysUsername = c.String(maxLength: 100),
            //            SysTglEntry = c.DateTime(nullable: false),
            //            SysWorkstation = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.RefKegiatans", t => t.KegiatanID, cascadeDelete: true)
            //    .Index(t => t.KegiatanID);
            
            //CreateTable(
            //    "dbo.RefQuarters",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            QuarterDetails = c.String(maxLength: 20),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.RefTPUJenis",
            //    c => new
            //        {
            //            JenisID = c.Int(nullable: false, identity: true),
            //            JenisDetail = c.String(maxLength: 40),
            //            Aktif = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.JenisID);
            
            //CreateTable(
            //    "dbo.RefTPUStatus",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Ket = c.String(maxLength: 30),
            //            Aktif = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.TransTPUTujuans",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            TujuanTPU = c.String(maxLength: 300),
            //            TPUID = c.Int(nullable: false),
            //            SysUsername = c.String(maxLength: 100),
            //            SysTglEntry = c.DateTime(nullable: false),
            //            SysWorkstation = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.RefTPUs", t => t.TPUID, cascadeDelete: true)
            //    .Index(t => t.TPUID);
            
            //CreateTable(
            //    "dbo.RefPopupTexts",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Message = c.String(maxLength: 300),
            //            Airing = c.DateTime(nullable: false),
            //            ModulID = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.RefRoles",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Detail = c.String(),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.TransFlashNotifClicks",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            UserName = c.String(maxLength: 100),
            //            Date = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.TransFlashNotifikasis",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            body = c.String(maxLength: 300),
            //            Date = c.DateTime(nullable: false),
            //            Controller = c.String(maxLength: 20),
            //            name = c.String(maxLength: 100),
            //            Action = c.String(maxLength: 20),
            //            RouteID = c.Int(),
            //            RoleID = c.Int(nullable: false),
            //            UnitID = c.Int(nullable: false),
            //            NotifType = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.TransNotifClicks",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            UserName = c.String(maxLength: 100),
            //            Date = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.TransNotifikasis",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            body = c.String(maxLength: 300),
            //            Date = c.DateTime(nullable: false),
            //            Controller = c.String(maxLength: 20),
            //            name = c.String(maxLength: 100),
            //            Action = c.String(maxLength: 20),
            //            RouteID = c.Int(),
            //            RoleID = c.Int(nullable: false),
            //            UnitID = c.Int(nullable: false),
            //            NotifType = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.TransTPUTujuans", "TPUID", "dbo.RefTPUs");
            //DropForeignKey("dbo.RefTPUs", "TPUStatusID", "dbo.RefTPUStatus");
            //DropForeignKey("dbo.RefTPUs", "TPUJenisID", "dbo.RefTPUJenis");
            //DropForeignKey("dbo.RefTPUs", "TPUQTargetID", "dbo.RefQuarters");
            //DropForeignKey("dbo.TransKegiatanSTs", "KegiatanID", "dbo.RefKegiatans");
            //DropForeignKey("dbo.TransKegiatanPolrecs", "KegiatanID", "dbo.RefKegiatans");
            //DropForeignKey("dbo.TransKegiatanOutputs", "OutputJenisID", "dbo.RefKegiatanOutputJenis");
            //DropForeignKey("dbo.TransKegiatanOutputs", "KegiatanID", "dbo.RefKegiatans");
            //DropForeignKey("dbo.TransKegiatanKomentars", "KomenKegID", "dbo.RefKegiatans");
            //DropForeignKey("dbo.TransKegiatanHS", "KegiatanID", "dbo.RefKegiatans");
            //DropForeignKey("dbo.RefKegiatans", "KegiatanTPUID", "dbo.RefTPUs");
            //DropForeignKey("dbo.RefKegiatans", "PeriodeID", "dbo.RefPeriodes");
            //DropForeignKey("dbo.TransIkhtisarProgresses", "UniverseID", "dbo.RefUniverseAudits");
            //DropForeignKey("dbo.TransNDPermintaans", "PKPTID", "dbo.TransSchedules");
            //DropForeignKey("dbo.TransIkhtisarProgresses", "PKPTID", "dbo.TransSchedules");
            //DropForeignKey("dbo.RefTPUs", "PKPTID", "dbo.TransSchedules");
            //DropForeignKey("dbo.TransNDPermintaans", "PeriodeID", "dbo.RefPeriodes");
            //DropForeignKey("dbo.TransKegiatanProgresses", "Period", "dbo.RefPeriodes");
            //DropForeignKey("dbo.TransKegiatanProgresses", "KegStatusID", "dbo.RefKegiatanStatus");
            //DropForeignKey("dbo.TransKegiatanProgresses", "KegiatanID", "dbo.RefKegiatans");
            //DropForeignKey("dbo.TransIkhtisarProgresses", "PeriodeID", "dbo.RefPeriodes");
            //DropForeignKey("dbo.TransHighlights", "Period", "dbo.RefPeriodes");
            //DropForeignKey("dbo.TransHighlights", "KegiatanID", "dbo.RefKegiatans");
            //DropForeignKey("dbo.TransFlashKegiatanProgresses", "Period", "dbo.RefPeriodes");
            //DropForeignKey("dbo.TransFlashKegiatanProgresses", "KegStatusID", "dbo.RefFlashKegiatanStatus");
            //DropForeignKey("dbo.TransFlashKegiatanProgresses", "KegiatanID", "dbo.TransFlashKegiatans");
            //DropForeignKey("dbo.TransFlashKegiatanKomentars", "KegiatanID", "dbo.TransFlashKegiatans");
            //DropForeignKey("dbo.TransFlashKegiatans", "UnitID", "dbo.RefUnitPJs");
            //DropForeignKey("dbo.TransFlashKegiatans", "ManajerID", "dbo.RefPegawais");
            //DropForeignKey("dbo.TransFlashKegiatans", "Finalize", "dbo.RefFlashKegiatanStatus");
            //DropForeignKey("dbo.RefUniverseAudits", "UnitID", "dbo.RefUnitPJs");
            //DropForeignKey("dbo.RefTPUs", "TPUUnitPJID", "dbo.RefUnitPJs");
            //DropForeignKey("dbo.RefPegawais", "PegUnitID", "dbo.RefUnitPJs");
            //DropForeignKey("dbo.RefTPUs", "TPUPJID", "dbo.RefPegawais");
            //DropForeignKey("dbo.RefKegiatans", "KegMjrID", "dbo.RefPegawais");
            //DropForeignKey("dbo.RefTPUs", "TPUEselon1ID", "dbo.RefEselon1");
            //DropIndex("dbo.TransTPUTujuans", new[] { "TPUID" });
            //DropIndex("dbo.TransKegiatanSTs", new[] { "KegiatanID" });
            //DropIndex("dbo.TransKegiatanPolrecs", new[] { "KegiatanID" });
            //DropIndex("dbo.TransKegiatanOutputs", new[] { "OutputJenisID" });
            //DropIndex("dbo.TransKegiatanOutputs", new[] { "KegiatanID" });
            //DropIndex("dbo.TransKegiatanKomentars", new[] { "KomenKegID" });
            //DropIndex("dbo.TransKegiatanHS", new[] { "KegiatanID" });
            //DropIndex("dbo.TransNDPermintaans", new[] { "PeriodeID" });
            //DropIndex("dbo.TransNDPermintaans", new[] { "PKPTID" });
            //DropIndex("dbo.TransKegiatanProgresses", new[] { "KegStatusID" });
            //DropIndex("dbo.TransKegiatanProgresses", new[] { "KegiatanID" });
            //DropIndex("dbo.TransKegiatanProgresses", new[] { "Period" });
            //DropIndex("dbo.TransHighlights", new[] { "KegiatanID" });
            //DropIndex("dbo.TransHighlights", new[] { "Period" });
            //DropIndex("dbo.TransFlashKegiatanKomentars", new[] { "KegiatanID" });
            //DropIndex("dbo.TransFlashKegiatans", new[] { "Finalize" });
            //DropIndex("dbo.TransFlashKegiatans", new[] { "UnitID" });
            //DropIndex("dbo.TransFlashKegiatans", new[] { "ManajerID" });
            //DropIndex("dbo.TransFlashKegiatanProgresses", new[] { "KegStatusID" });
            //DropIndex("dbo.TransFlashKegiatanProgresses", new[] { "KegiatanID" });
            //DropIndex("dbo.TransFlashKegiatanProgresses", new[] { "Period" });
            //DropIndex("dbo.TransIkhtisarProgresses", new[] { "PeriodeID" });
            //DropIndex("dbo.TransIkhtisarProgresses", new[] { "PKPTID" });
            //DropIndex("dbo.TransIkhtisarProgresses", new[] { "UniverseID" });
            //DropIndex("dbo.RefUniverseAudits", new[] { "UnitID" });
            //DropIndex("dbo.RefPegawais", new[] { "PegUnitID" });
            //DropIndex("dbo.RefKegiatans", new[] { "PeriodeID" });
            //DropIndex("dbo.RefKegiatans", new[] { "KegMjrID" });
            //DropIndex("dbo.RefKegiatans", new[] { "KegiatanTPUID" });
            //DropIndex("dbo.RefTPUs", new[] { "TPUEselon1ID" });
            //DropIndex("dbo.RefTPUs", new[] { "TPUJenisID" });
            //DropIndex("dbo.RefTPUs", new[] { "TPUUnitPJID" });
            //DropIndex("dbo.RefTPUs", new[] { "TPUStatusID" });
            //DropIndex("dbo.RefTPUs", new[] { "TPUQTargetID" });
            //DropIndex("dbo.RefTPUs", new[] { "TPUPJID" });
            //DropIndex("dbo.RefTPUs", new[] { "PKPTID" });
            //DropTable("dbo.TransNotifikasis");
            //DropTable("dbo.TransNotifClicks");
            //DropTable("dbo.TransFlashNotifikasis");
            //DropTable("dbo.TransFlashNotifClicks");
            //DropTable("dbo.RefRoles");
            //DropTable("dbo.RefPopupTexts");
            //DropTable("dbo.TransTPUTujuans");
            //DropTable("dbo.RefTPUStatus");
            //DropTable("dbo.RefTPUJenis");
            //DropTable("dbo.RefQuarters");
            //DropTable("dbo.TransKegiatanSTs");
            //DropTable("dbo.TransKegiatanPolrecs");
            //DropTable("dbo.RefKegiatanOutputJenis");
            //DropTable("dbo.TransKegiatanOutputs");
            //DropTable("dbo.TransKegiatanKomentars");
            //DropTable("dbo.TransKegiatanHS");
            //DropTable("dbo.TransSchedules");
            //DropTable("dbo.TransNDPermintaans");
            //DropTable("dbo.RefKegiatanStatus");
            //DropTable("dbo.TransKegiatanProgresses");
            //DropTable("dbo.TransHighlights");
            //DropTable("dbo.TransFlashKegiatanKomentars");
            //DropTable("dbo.TransFlashKegiatans");
            //DropTable("dbo.RefFlashKegiatanStatus");
            //DropTable("dbo.TransFlashKegiatanProgresses");
            //DropTable("dbo.RefPeriodes");
            //DropTable("dbo.TransIkhtisarProgresses");
            //DropTable("dbo.RefUniverseAudits");
            //DropTable("dbo.RefUnitPJs");
            //DropTable("dbo.RefPegawais");
            //DropTable("dbo.RefKegiatans");
            //DropTable("dbo.RefTPUs");
            //DropTable("dbo.RefEselon1");
        }
    }
}
