using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class RefUniverseAudit
    {
        public RefUniverseAudit()
        {
            this.TransIkhtisarProgress = new HashSet<TransIkhtisarProgress>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Objek Pengawasan")]
        [StringLength(100)]
        public string Ket { get; set; }
        public bool Aktif { get; set; }
        [DisplayName("Unit Penanggung Jawab")]
        public int UnitID { get; set; }

        [ForeignKey("UnitID")]
        public RefUnitPJ RefUnitPJ { get; set; }

        public virtual ICollection<TransIkhtisarProgress> TransIkhtisarProgress { get; set; }
    }
}