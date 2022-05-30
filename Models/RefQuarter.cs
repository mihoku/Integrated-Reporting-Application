using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ira.Models
{
    public class RefQuarter
    {
        public RefQuarter()
        {
            this.RefTPU = new HashSet<RefTPU>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Quarter")]
        [StringLength(20)]
        public string QuarterDetails { get; set; }

        public virtual ICollection<RefTPU> RefTPU { get; set; }
    }
}