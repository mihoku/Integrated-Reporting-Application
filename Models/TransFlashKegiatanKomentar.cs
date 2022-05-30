using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class TransFlashKegiatanKomentar
    {
        [Key]
        public int ID { get; set; }
        [StringLength(100)]
        public string KomenUserID { get; set; }
        [DisplayName("Komentar")]
        [StringLength(1000)]
        public string KomenIsi { get; set; }
        public int KegiatanID { get; set; }
        public DateTime KomenTgl { get; set; }
        [StringLength(100)]
        public string SysUsername { get; set; }
        public DateTime SysTglEntry { get; set; }
        [StringLength(100)]
        public string SysWorkstation { get; set; }

        [ForeignKey("KegiatanID")]
        public virtual TransFlashKegiatan TransFlashKegiatan { get; set; }
    }
}