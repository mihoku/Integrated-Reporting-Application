using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class RefPeriode
    {
        public RefPeriode()
        {
            this.TransKegiatanProgress = new HashSet<TransKegiatanProgress>();
            this.TransFlashProgress = new HashSet<TransFlashKegiatanProgress>();
            this.TransHighlight = new HashSet<TransHighlight>();
            this.TransIkhtisarProgress = new HashSet<TransIkhtisarProgress>();
            this.TransNDPermintaan = new HashSet<TransNDPermintaan>();
            this.TransNDPermintaanFlash = new HashSet<TransNDPermintaanFlash>();
        }
        
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Periode Pelaporan")]
        [StringLength(20)]
        public string Ket { get; set; }
        public bool Aktif { get; set; }

        public virtual ICollection<TransHighlight> TransHighlight { get; set; }
        public virtual ICollection<TransKegiatanProgress> TransKegiatanProgress{ get; set; }
        public virtual ICollection<TransFlashKegiatanProgress> TransFlashProgress { get; set; }
        public virtual ICollection<TransIkhtisarProgress> TransIkhtisarProgress { get; set; }
        public virtual ICollection<TransNDPermintaan> TransNDPermintaan { get; set; }
        public virtual ICollection<TransNDPermintaanFlash> TransNDPermintaanFlash { get; set; }
    }
}