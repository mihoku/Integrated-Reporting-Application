using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ira.Models
{
    public class RefKegiatanStatus
    {
        public RefKegiatanStatus()
        {
            this.TransKegiatanProgress = new HashSet<TransKegiatanProgress>();
        }
        
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Status Kegiatan")]
        [StringLength(30)]
        public string Ket { get; set; }
        public bool Aktif { get; set; }

        public virtual ICollection<TransKegiatanProgress> TransKegiatanProgress { get; set; }
    }
}