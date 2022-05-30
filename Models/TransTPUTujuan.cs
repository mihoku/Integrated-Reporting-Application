using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class TransTPUTujuan
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Sasaran TPU")]
        [StringLength(300)]
        public string TujuanTPU { get; set; }
        public int TPUID { get; set; }
        [StringLength(100)]
        public string SysUsername { get; set; }
        public DateTime SysTglEntry { get; set; }
        [StringLength(100)]
        public string SysWorkstation { get; set; }

        [ForeignKey("TPUID")]
        public virtual RefTPU RefTPU { get; set; }

    }
}