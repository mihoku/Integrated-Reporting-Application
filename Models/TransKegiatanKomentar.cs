using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class TransKegiatanKomentar
    {
        [Key]
        public int KomenID { get; set; }
        [StringLength(100)]
        public string KomenUserID { get; set; }
        [DisplayName("Komentar")]
        [StringLength(1000)]
        public string KomenIsi { get; set; }
        public int KomenKegID { get; set; }
        public DateTime KomenTgl { get; set; }
        [StringLength(100)]
        public string SysUsername { get; set; }
        public DateTime SysTglEntry { get; set; }
        [StringLength(100)]
        public string SysWorkstation { get; set; }

        [ForeignKey("KomenKegID")]
        public virtual RefKegiatan RefKegiatan { get; set; }
    }
}