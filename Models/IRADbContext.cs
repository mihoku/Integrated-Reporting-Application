using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ira.Models
{
    public class IRADbContext : DbContext
    {
        public virtual DbSet<ConfigMail> ConfigMail { get; set; }
        //public virtual DbSet<ConfigMailAdvanced> ConfigMailAdvanced { get; set; }
        public virtual DbSet<RefTPUJenis> RefJenisTPU { get; set; }
        public virtual DbSet<RefEselon1> RefEselon1 { get; set; }
        public virtual DbSet<RefPegawai> RefPegawai { get; set; }
        public virtual DbSet<RefQuarter> RefQuarter { get; set; }
        public virtual DbSet<RefRole> RefRole { get; set; }
        public virtual DbSet<RefKegiatanStatus> RefStatusKegiatan { get; set; }
        public virtual DbSet<RefTPUStatus> RefStatusTPU { get; set; }
        public virtual DbSet<RefUnitPJ> RefUnitPJ { get; set; }
        //public virtual DbSet<TransFlashReport> TransFlashReport { get; set; }
        //public virtual DbSet<TransFlashReportContent> TransFlashReportContent { get; set; }
        public virtual DbSet<TransKegiatanKomentar> TransKomentar { get; set; }
        public virtual DbSet<TransKegiatanProgress> TransProgressKegiatan { get; set; }
        public virtual DbSet<TransKegiatanHS> TransHambatanSolusiKegiatan { get; set; }
        public virtual DbSet<RefKegiatan> RefKegiatan { get; set; }
        public virtual DbSet<RefTPU> RefTPU { get; set; }
        public virtual DbSet<RefPopupText> RefPopUpText { get; set; }
        public virtual DbSet<RefUniverseAudit> RefUniverseAudit { get; set; }
        public virtual DbSet<RefPeriode> RefPeriode { get; set; }
        public virtual DbSet<TransTPUTujuan> TransTPUTujuan { get; set; }
        public virtual DbSet<RefKegiatanOutputJenis> RefKegiatanOutputJenis { get; set; }
        public virtual DbSet<TransKegiatanOutput> TransKegiatanOutput { get; set; }
        public virtual DbSet<TransKegiatanPolrec> TransKegiatanPolrec { get; set; }
        public virtual DbSet<TransKegiatanST> TransKegiatanST { get; set; }
        public virtual DbSet<TransSchedule> TransSchedule { get; set; }
        public virtual DbSet<TransNotifikasi> TransNotifikasi { get; set; }
        public virtual DbSet<TransNotifClick> TransNotifClick { get; set; }
        public virtual DbSet<RefFlashKegiatanStatus> RefFlashStatusKegiatan { get; set; }
        public virtual DbSet<TransFlashKegiatan> TransFlashKegiatan { get; set; }
        public virtual DbSet<TransFlashKegiatanKomentar> TransFlashKomentar { get; set; }
        public virtual DbSet<TransFlashKegiatanProgress> TransFlashProgress { get; set; }
        public virtual DbSet<TransFlashNotifikasi> TransFlashNotifikasi { get; set; }
        public virtual DbSet<TransFlashNotifClick> TransFlashNotifClick { get; set; }
        public virtual DbSet<TransHighlight> TransHighlight { get; set; }
        public virtual DbSet<TransNDPermintaan> TransNDPermintaan { get; set; }
        public virtual DbSet<TransNDPermintaanFlash> TransNDPermintaanFlash { get; set; }
        public virtual DbSet<TransIkhtisarProgress> TransIkhtisarProgresses { get; set; }

        public System.Data.Entity.DbSet<ira.Models.TransFlashKegiatanOutput> TransFlashKegiatanOutputs { get; set; }
        //public virtual DbSet<ira.Models.AspNetRole> AspNetRole { get; set; }
        //public virtual DbSet<ira.Models.AspNetUser> AspNetUser { get; set; }
        //public virtual DbSet<ira.Models.AspNetUserClaim> AspNetUserClaim { get; set; }
        //public virtual DbSet<ira.Models.AspNetUserLogin> AspNetUserLogin { get; set; }
        
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //}
    }

}