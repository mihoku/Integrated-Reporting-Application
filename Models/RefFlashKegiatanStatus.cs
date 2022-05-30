using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ira.Models
{
    public class RefFlashKegiatanStatus
    {
        public RefFlashKegiatanStatus()
        {
            this.TransFlashKegiatanProgress = new HashSet<TransFlashKegiatanProgress>();
            this.TransFlashKegiatan = new HashSet<TransFlashKegiatan>();
        }
        
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Status Kegiatan")]
        [StringLength(60)]
        public string Ket { get; set; }
        public bool Aktif { get; set; }

        public virtual ICollection<TransFlashKegiatanProgress> TransFlashKegiatanProgress { get; set; }
        public virtual ICollection<TransFlashKegiatan> TransFlashKegiatan { get; set; }
    }
}