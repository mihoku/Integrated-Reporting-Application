using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class TransSchedule
    {
        public TransSchedule()
        {
            this.RefTPU = new HashSet<RefTPU>();
            this.TransIkhtisarProgress = new HashSet<TransIkhtisarProgress>();
            this.TransNDPermintaan = new HashSet<TransNDPermintaan>();
        }
        
        [Key]
        public int ID { get; set; }
        [DisplayName("Judul Master Tema Pengawasan")]
        [StringLength(100)]
        public string Title { get; set; }
        public int Tahun { get; set; }
        public int Locked { get; set; }

        public virtual ICollection<RefTPU> RefTPU { get; set; }
        public virtual ICollection<TransIkhtisarProgress> TransIkhtisarProgress { get; set; }
        public virtual ICollection<TransNDPermintaan> TransNDPermintaan { get; set; }
    }
}