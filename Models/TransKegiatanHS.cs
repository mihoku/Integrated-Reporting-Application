using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class TransKegiatanHS
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(500)]
        public string Hambatan { get; set; }
        [StringLength(500)]
        public string Solusi { get; set; }
        public int KegiatanID { get; set; }
        [StringLength(100)]
        public string SysUsername { get; set; }
        public DateTime SysTglEntry { get; set; }
        [StringLength(100)]
        public string SysWorkstation { get; set; }

        [ForeignKey("KegiatanID")]
        public virtual RefKegiatan RefKegiatan { get; set; }

    }
}