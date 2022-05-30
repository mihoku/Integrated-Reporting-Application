using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ira.Models
{
    public class RefPopupText
    {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Pop Up Message")]
        [StringLength(300)]
        public string Message { get; set; }

        [DisplayName("Tanggal Siar")]
        public DateTime Airing { get; set; }

        public int ModulID { get; set; }
    }
}