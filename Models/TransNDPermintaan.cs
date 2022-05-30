using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class TransNDPermintaan
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Tanggal Nota Dinas")]
        public DateTime TanggalND { get; set; }

        [DisplayName("Nomor Nota Dinas")]
        [StringLength(100)]
        public string NomorND { get; set; }

        //[AllowHtml]
        //[DisplayName("Rencana Pengawasan ke Depan")]
        //[StringLength(1900)]
        //public string RencanaPengawasan { get; set; }

        public int PKPTID { get; set; }

        public int PeriodeID { get; set; }

        public bool Locked { get; set; }

        [StringLength(100)]
        public string SysUsername { get; set; }
        [StringLength(100)]
        public string SysWorkstation { get; set; }
        public DateTime SysTglEntry { get; set; }

        [ForeignKey("PKPTID")]
        public virtual TransSchedule TransSchedule { get; set; }

        [ForeignKey("PeriodeID")]
        public virtual RefPeriode RefPeriode { get; set; }
    }
}