using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ira.Models
{
    public class RefUnitPJ
    {
            public RefUnitPJ()
            {
                this.RefTPU = new HashSet<RefTPU>();
                this.RefPegawai = new HashSet<RefPegawai>();
                this.RefUniverseAudit = new HashSet<RefUniverseAudit>();
                this.TransFlashKegiatan = new HashSet<TransFlashKegiatan>();
                //RefUniverseAudit = new List<RefUniverseAudit>();
            }

            [Key]
            public int ID { get; set; }

            [Required]
            [DisplayName("Nama Unit")]
            [StringLength(60)]
            public string Detail { get; set; }

            [Required]
            [DisplayName("Uraian Pendek")]
            [StringLength(7)]
            public string DetailShort { get; set; }
            public bool Aktif { get; set; }
            public bool isPrimeMover { get; set; }
            public virtual ICollection<RefTPU> RefTPU { get; set; }
            public virtual ICollection<RefPegawai> RefPegawai { get; set; }
            public virtual ICollection<RefUniverseAudit> RefUniverseAudit { get; set; }
            public virtual ICollection<TransFlashKegiatan> TransFlashKegiatan { get; set; }
        }
    }
