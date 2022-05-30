using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class RefPegawai
    {
        public RefPegawai()
        {
            this.RefTPU = new HashSet<RefTPU>();
            this.RefKegiatan = new HashSet<RefKegiatan>();
        }
        [Key]
    
        public int ID { get; set; }

        [Required]
        [DisplayName("Nama Pegawai")]
        public string PegName { get; set; }

        [Required]
        [DisplayName("NIP")]
        [StringLength(18)]
        public string PegNIP { get; set; }

        [DisplayName("Unit")]
        public int PegUnitID { get; set; }

        public bool Aktif { get; set; }

        [Required]
        [DisplayName("Email Kemenkeu")]
        public string PegEmailKemenkeu { get; set; }

        [ForeignKey("PegUnitID")]
        public virtual RefUnitPJ RefUnitPJ { get; set; }

        public virtual ICollection<RefTPU> RefTPU { get; set; }
        public virtual ICollection<RefKegiatan> RefKegiatan { get; set; }

    }
}