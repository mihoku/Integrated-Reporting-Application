using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class TransKegiatanProgress
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Progress Kegiatan")]
        [StringLength(1000)]
        public string Detail { get; set; }
        //public int Tahun { get; set; }
        public int Period { get; set; }
        public int KegiatanID { get; set; }
        [DisplayName("Status Kegiatan")]
        public int KegStatusID { get; set; }
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

        [ForeignKey("KegStatusID")]
        public virtual RefKegiatanStatus RefKegiatanStatus { get; set; }

        [ForeignKey("Period")]
        public virtual RefPeriode RefPeriode { get; set; }
    }
}