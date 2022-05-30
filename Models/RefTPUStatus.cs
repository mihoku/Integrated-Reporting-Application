using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ira.Models
{
    public class RefTPUStatus
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Status Tema Pengawasan")]
        [StringLength(30)]
        public string Ket { get; set; }
        public bool Aktif { get; set; }
        public ICollection<RefTPU> TPU { get; set; }
    }
}