using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class TransHighlight
    {
        [Key]
        public int ID { get; set; }
        public int Tahun { get; set; }
        public int Period { get; set; }
        public int KegiatanID { get; set; }
        [StringLength(100)]
        public string SysUsername { get; set; }
        public DateTime SysTglEntry { get; set; }
        [StringLength(100)]
        public string SysWorkstation { get; set; }

        [ForeignKey("KegiatanID")]
        public virtual RefKegiatan RefKegiatan { get; set; }

        [ForeignKey("Period")]
        public virtual RefPeriode RefPeriode { get; set; }
    }
}