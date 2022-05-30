using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class TransKegiatanPolrec
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [DisplayName("Judul Policy Recommendation")]
        [StringLength(100)]
        public string Judul { get; set; }
        [Required]
        [DisplayName("Uraian")]
        [StringLength(1000)]
        public string Uraian { get; set; }
        public int KegiatanID { get; set; }
        [StringLength(100)]
        public string SysUsername { get; set; }
        public Nullable<DateTime> SysTglEntry { get; set; }
        [StringLength(100)]
        public string SysWorkstation { get; set; }

        [ForeignKey("KegiatanID")]
        public virtual RefKegiatan RefKegiatan { get; set; }

    }
}