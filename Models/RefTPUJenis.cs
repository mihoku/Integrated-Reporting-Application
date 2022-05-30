using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ira.Models
{
    public class RefTPUJenis
    {
        public RefTPUJenis()
        {
            this.RefTPU = new HashSet<RefTPU>();
        }
    
        [Key]
        public int JenisID { get; set; }

        [Required]
        [DisplayName("Jenis TPU")]
        [StringLength(40)]
        public string JenisDetail { get; set; }
        public bool Aktif { get; set; }
    
        public virtual ICollection<RefTPU> RefTPU { get; set; }
    }
}