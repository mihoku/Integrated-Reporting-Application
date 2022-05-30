//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ira.Models
//{
//    public class TransFlashReport
//    {
        
//        public TransFlashReport()
//        {
//            this.TransFlashReportContent = new HashSet<TransFlashReportContent>();
//        }

//        [Key]
//        public int ID { get; set; }
//        public int Tahun { get; set; }
//        public int Period { get; set; }
//        public int KegID { get; set; }

//        public int FlashID { get; set; }
//        public Nullable<int> FlashTh { get; set; }
//        public Nullable<int> FlashPeriod { get; set; }
//        public Nullable<int> FlashKegID { get; set; }

//        public string SysUsername { get; set; }
//        public DateTime SysTglEntry { get; set; }
//        public string SysWorkstation { get; set; }
    
//        public virtual ICollection<TransFlashReportContent> TransFlashReportContent { get; set; }

//        [ForeignKey("FlashKegID")]
//        public virtual RefKegiatan RefKegiatan { get; set; }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class TransFlashKegiatan
    {
        public TransFlashKegiatan()
        {
            //this.TransFlashReport = new HashSet<TransFlashReport>();
            this.TransFlashKegiatanOutput = new HashSet<TransFlashKegiatanOutput>();
            this.TransFlashKegiatanKomentar = new HashSet<TransFlashKegiatanKomentar>();
            this.TransFlashKegiatanProgress = new HashSet<TransFlashKegiatanProgress>();
            //this.TransKegiatanHS = new HashSet<TransKegiatanHS>();
            //this.TransKegiatanPolrec = new HashSet<TransKegiatanPolrec>();
            //this.TransKegiatanST = new HashSet<TransKegiatanST>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Judul Kegiatan")]
        [StringLength(200)]
        public string Judul { get; set; }

 //       public int KegiatanTPUID { get; set; }
        [DisplayName("Manajer Kegiatan")]
        public int ManajerID { get; set; }
        [DisplayName("Unit Penanggungjawab")]
        public int UnitID { get; set; }

        //public int KegWaMjrID { get; set; }
        //public string KegOutput { get; set; }
        //public int KegStatusID { get; set; }
        //[DisplayName("Periode Mulai")]
        //public int PeriodeID { get; set; }
        [DisplayName("Tanggal Kasus")]
        public System.DateTime TanggalKasus { get; set; }
        //public string Output { get; set; }
        //public string Keterangan { get; set; }
        public int Finalize { get; set; }
        public Nullable<System.DateTime> DateFinalized { get; set; }
        [StringLength(100)]
        public string SysUsername { get; set; }
        public Nullable<System.DateTime> SysTglEntry { get; set; }
        [StringLength(100)]
        public string SysWorkstation { get; set; }

        //public virtual ICollection<TransFlashReport> TransFlashReport { get; set; }
        //[ForeignKey("KegStatusID")]
        //public virtual RefKegiatanStatus RefKegiatanStatus { get; set; }
        public virtual ICollection<TransFlashKegiatanKomentar> TransFlashKegiatanKomentar { get; set; }
        public virtual ICollection<TransFlashKegiatanProgress> TransFlashKegiatanProgress { get; set; }
        public virtual ICollection<TransFlashKegiatanOutput> TransFlashKegiatanOutput { get; set; }
        //public virtual ICollection<TransKegiatanHS> TransKegiatanHS { get; set; }
        //public virtual ICollection<TransKegiatanPolrec> TransKegiatanPolrec { get; set; }
        //public virtual ICollection<TransKegiatanST> TransKegiatanST { get; set; }
        //[ForeignKey("PeriodeID")]
        //public virtual RefPeriode RefPeriode { get; set; }
        //[ForeignKey("KegiatanTPUID")]
        //public virtual RefTPU RefTPU { get; set; }
        [ForeignKey("ManajerID")]
        public virtual RefPegawai RefPegawai { get; set; }

        [ForeignKey("Finalize")]
        public virtual RefFlashKegiatanStatus RefFlashKegiatanStatus { get; set; }

        [ForeignKey("UnitID")]
        public virtual RefUnitPJ RefUnitPJ { get; set; }
    }
}