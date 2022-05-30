using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ira.Models
{
    public class RefRole
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [DisplayName("Role")]
        [StringLength(20)]
        public string Detail { get; set; }
    }
}