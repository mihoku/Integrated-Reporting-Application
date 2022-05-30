using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ira.Models
{
    public class RefEselon1
    {
        public RefEselon1()
        {
            this.TPU = new HashSet<RefTPU>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Nama Eselon I")]
        [StringLength(100)]
        public string Ket { get; set; }
        public bool Aktif { get; set; }
        public ICollection<RefTPU> TPU { get; set; }
    }
}