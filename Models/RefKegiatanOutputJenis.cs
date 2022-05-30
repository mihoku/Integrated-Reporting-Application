﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ira.Models
{
    public class RefKegiatanOutputJenis
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [DisplayName("Jenis Output")]
        [StringLength(100)]
        public string Ket { get; set; }
        public bool Aktif { get; set; }
        public ICollection<TransKegiatanOutput> TransKegiatanOutput { get; set; }
    }
}