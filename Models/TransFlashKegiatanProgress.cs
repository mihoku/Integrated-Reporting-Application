//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace ira.Models
//{
//    public class TransFlashReportContent
//    {
//        [Key]
//        public int ID { get; set; }
//        public string ReportContent { get; set; }
//        public int FlashReportID { get; set; }
//        public string SysUsername { get; set; }
//        public DateTime SysTglEntry { get; set; }
//        public string SysWorkstation { get; set; }

//        [ForeignKey("FlashReportID")]
//        public virtual TransFlashReport TransFlashReport { get; set; }
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
    public class TransFlashKegiatanProgress
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Progress Kegiatan")]
        [StringLength(1000)]
        public string Detail { get; set; }
        //public int Tahun { get; set; }
        public int Period { get; set; }
        public int Tahun { get; set; }
        //public DateTime Tanggal { get; set; }
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
        public virtual TransFlashKegiatan TransFlashKegiatan { get; set; }

        [ForeignKey("KegStatusID")]
        public virtual RefFlashKegiatanStatus RefFlashKegiatanStatus { get; set; }

        [ForeignKey("Period")]
        public virtual RefPeriode RefPeriode { get; set; }
    }
}