using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class TransFlashKegiatanOutput
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [DisplayName("Nomor")]
        [StringLength(100)]
        public string Nomor { get; set; }
        [DisplayName("Tanggal Terbit")]
        public DateTime TanggalTerbit { get; set; }
        [Required]
        [DisplayName("Judul Output")]
        [StringLength(500)]
        public string Judul { get; set; }
        [Required]
        [DisplayName("Uraian")]
        [StringLength(1000)]
        public string Uraian { get; set; }
        public int KegiatanID { get; set; }
        [DisplayName("Jenis Output")]
        public int OutputJenisID { get; set; }
        //public HttpPostedFileBase attachment { get; set; }
        [StringLength(100)]
        public string SysUsername { get; set; }
        public DateTime SysTglEntry { get; set; }
        [StringLength(100)]
        public string SysWorkstation { get; set; }

        [ForeignKey("KegiatanID")]
        public virtual TransFlashKegiatan TransFlashKegiatan { get; set; }

        [ForeignKey("OutputJenisID")]
        public virtual RefKegiatanOutputJenis RefKegiatanOutputJenis { get; set; }
    }
}