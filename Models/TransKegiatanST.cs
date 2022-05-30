using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class TransKegiatanST
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Nomor Surat Tugas")]
        [Required]
        [StringLength(100)]
        public string NoST { get; set; }
        [Required]
        [DisplayName("Judul")]
        [StringLength(500)]
        public string JudulST { get; set; }
        public int Tahun { get; set; }
        [DisplayName("Tanggal ST")]
        public Nullable<DateTime> TanggalST { get; set; }
        public int KegiatanID { get; set; }
        [DisplayName("Mulai dari")]
        public Nullable<DateTime> TglAwal { get; set; }
        [DisplayName("Sampai dengan")]
        public Nullable<DateTime> TglAkhir { get; set; }
        //public string Output { get; set; }
        //public string HambatanSolusi { get; set; }
        //public string Keterangan { get; set; }
        [StringLength(100)]
        public string SysUsername { get; set; }
        public DateTime SysTglEntry { get; set; }
        [StringLength(100)]
        public string SysWorkstation { get; set; }

        [ForeignKey("KegiatanID")]
        public virtual RefKegiatan RefKegiatan { get; set; }

    }
}