using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class RefKegiatan
    {
        public RefKegiatan()
        {
            //this.TransFlashReport = new HashSet<TransFlashReport>();
            this.TransKegiatanKomentar = new HashSet<TransKegiatanKomentar>();
            this.TransKegiatanProgress = new HashSet<TransKegiatanProgress>();
            this.TransKegiatanHS = new HashSet<TransKegiatanHS>();
            this.TransKegiatanOutput = new HashSet<TransKegiatanOutput>();
            this.TransKegiatanPolrec = new HashSet<TransKegiatanPolrec>();
            this.TransKegiatanST = new HashSet<TransKegiatanST>();
            this.TransHighlight = new HashSet<TransHighlight>();
        }
    
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Judul Kegiatan")]
        [StringLength(200)]
        public string KegName { get; set; }

        [DisplayName("Judul Tema Pengawasan")]
        public int KegiatanTPUID { get; set; }
        [DisplayName("Manajer Kegiatan")]
        public int KegMjrID { get; set; }
        //public int KegWaMjrID { get; set; }
        //public string KegOutput { get; set; }
        //public int KegStatusID { get; set; }
        [DisplayName("Periode Mulai")]
        public int PeriodeID { get; set; }
        [DisplayName("Target Penyelesaian")]
        public System.DateTime KegTarget { get; set; }
        //public string Output { get; set; }
        [DisplayName("Keterangan Tambahan")]
        [StringLength(300)]
        public string Keterangan { get; set; }
        public int Finalize { get; set; }
        [StringLength(100)]
        public string SysUsername { get; set; }
        public Nullable<System.DateTime> SysTglEntry { get; set; }
        [StringLength(100)]
        public string SysWorkstation { get; set; }
    
        //public virtual ICollection<TransFlashReport> TransFlashReport { get; set; }
        //[ForeignKey("KegStatusID")]
        //public virtual RefKegiatanStatus RefKegiatanStatus { get; set; }
        public virtual ICollection<TransKegiatanKomentar> TransKegiatanKomentar { get; set; }
        public virtual ICollection<TransKegiatanProgress> TransKegiatanProgress { get; set; }
        public virtual ICollection<TransKegiatanHS> TransKegiatanHS { get; set; }
        public virtual ICollection<TransKegiatanOutput> TransKegiatanOutput { get; set; }
        public virtual ICollection<TransKegiatanPolrec> TransKegiatanPolrec { get; set; }
        public virtual ICollection<TransKegiatanST> TransKegiatanST { get; set; }
        public virtual ICollection<TransHighlight> TransHighlight { get; set; }
        [ForeignKey("PeriodeID")]
        public virtual RefPeriode RefPeriode { get; set; }
        [ForeignKey("KegiatanTPUID")]
        public virtual RefTPU RefTPU { get; set; }
        [ForeignKey("KegMjrID")]
        public virtual RefPegawai RefPegawai { get; set; }
    }
}