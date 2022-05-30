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
    public class TransIkhtisarProgress
    {
        [Key]
        public int ID { get; set; }

        [AllowHtml]
        [DisplayName("Rencana Kerja/ Program-program Prioritas Objek Pengawasan")]
        [StringLength(1900)]
        public string RencanaKerja { get; set; }

        [AllowHtml]
        [DisplayName("Hasil Pengawasan")]
        [StringLength(1900)]
        public string HasilPengawasan { get; set; }

        [AllowHtml]
        [DisplayName("Rencana Pengawasan ke Depan")]
        [StringLength(1900)]
        public string RencanaPengawasan { get; set; }

        public int UniverseID { get; set; }

        public int PKPTID { get; set; }

        public int PeriodeID { get; set; }

        [StringLength(100)]
        public string SysUsername { get; set; }
        [StringLength(100)]
        public string SysWorkstation { get; set; }
        public DateTime SysTglEntry { get; set; }

        public bool Locked { get; set; }

        public bool Accepted { get; set; }

        [ForeignKey("UniverseID")]
        public virtual RefUniverseAudit RefUniverseAudit { get; set; }

        [ForeignKey("PKPTID")]
        public virtual TransSchedule TransSchedule { get; set; }

        [ForeignKey("PeriodeID")]
        public virtual RefPeriode RefPeriode { get; set; }
    }
}