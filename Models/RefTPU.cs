using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class RefTPU
    {
        public RefTPU()
        {
            this.RefKegiatan = new HashSet<RefKegiatan>();
            this.TransTPUTujuan = new HashSet<TransTPUTujuan>();
        }

        [Key]
        public int ID { get; set; }

        [DisplayName("Nomor TPU/DPU")]
        public Nullable<int> No { get; set; }
        
        [DisplayName("Judul")]
        [Required(ErrorMessage = "Judul Tema Pengawasan harus diisi.")]
        [StringLength(300)]
        public string TPUName { get; set; }

        [DisplayName("Master Tema Pengawasan")]
        //[DataType(DataType.MultilineText)]
        //[DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Master Tema Pengawasan harus diisi.")]
        //public int TPUThAnggaran { get; set; }

        public int PKPTID { get; set; }

        //[DisplayName("Tujuan TPU")]
        //[DataType(DataType.MultilineText)]
        ////[DataType(DataType.MultilineText)]
        ////[DataType(DataType.MultilineText)]
        //[Required(ErrorMessage = "Tujuan TPU harus diisi.")]
        //public string TPUTujuan { get; set; }

        [DisplayName("Penanggung Jawab")]
        [Required(ErrorMessage = "PJ TPU harus diisi.")]
        public int TPUPJID { get; set; }
        //public int ? RefPegawaiID { get; set; }

        [DisplayName("Target")]
        [Required(ErrorMessage = "Target Penyelesaian Tema Pengawasan harus diisi.")]
        public int TPUQTargetID { get; set; }
        //public int ? RefQuarterID { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "Status Tema Pengawasan harus diisi.")]
        public int TPUStatusID { get; set; }
        //public int ? RefTPUStatusID { get; set; }

        [DisplayName("Unit Penanggung Jawab")]
        [Required(ErrorMessage = "Unit penanggung jawab Tema Pengawasan harus diisi.")]
        public int TPUUnitPJID { get; set; }
        //public int ? RefUnitPJID { get; set; }

        [DisplayName("Jenis")]
        [Required(ErrorMessage = "Jenis Tema Pengawasan harus diisi.")]
        public int TPUJenisID { get; set; }
        //public int ? RefTPUJenisID { get; set; }

        [DisplayName("Lokasi Utama")]
        [Required(ErrorMessage = "Eselon 1 harus diisi.")]
        public int TPUEselon1ID { get; set; }

        public int Finalize { get; set; }

        [StringLength(100)]
        public string SysUsername { get; set; }
        public Nullable<System.DateTime> SysTglEntry { get; set; }
        [StringLength(100)]
        public string SysWorkstation { get; set; }

        //[DisplayName("PJ TPU")]
        //[Required(ErrorMessage = "PJ TPU harus diisi.")]
        //public RefPegawai TPUPJID { get; set; }

        //[DisplayName("Wakil PJ TPU")]
        //[Required(ErrorMessage = "Wakil PJ TPU harus diisi.")]
        //public Nullable<int> TPUWPJID { get; set; }

        //[DisplayName("Target")]
        //[Required(ErrorMessage = "Target TPU harus diisi.")]
        //public RefQuarter TPUQTargetID { get; set; }

        //[DisplayName("Status TPU")]
        //[Required(ErrorMessage = "Status TPU harus diisi.")]
        //public RefTPUStatus TPUStatusID { get; set; }

        //[DisplayName("Unit Penanggung Jawab")]
        //[Required(ErrorMessage = "Unit penanggung jawab TPU harus diisi.")]
        //public RefUnitPJ TPUUnitPJID { get; set; }

        //[DisplayName("Jenis TPU")]
        //[Required(ErrorMessage = "Jenis TPU harus diisi.")]
        //public RefTPUJenis TPUJenisID { get; set; }

        //public virtual RefTPUJenis RefTPUJenis { get; set; }

        //public virtual RefPegawai RefPegawai { get; set; }

        //public virtual RefQuarter RefQuarter { get; set; }

        //public virtual RefTPUStatus RefTPUStatus { get; set; }

        //public virtual RefUnitPJ RefUnitPJ { get; set; }

        [ForeignKey("PKPTID")]
        public virtual TransSchedule TransSchedule { get; set; }

        [ForeignKey("TPUUnitPJID")]
        public virtual RefUnitPJ RefUnitPJ { get; set; }

        [ForeignKey("TPUJenisID")]
        public virtual RefTPUJenis RefTPUJenis { get; set; }

        [ForeignKey("TPUPJID")]
        public virtual RefPegawai RefPegawai { get; set; }

        [ForeignKey("TPUQTargetID")]
        public virtual RefQuarter RefQuarter { get; set; }

        [ForeignKey("TPUStatusID")]
        public virtual RefTPUStatus RefTPUStatus { get; set; }

        [ForeignKey("TPUEselon1ID")]
        public virtual RefEselon1 RefEselon1 { get; set; }

        public virtual ICollection<RefKegiatan> RefKegiatan { get; set; }

        public virtual ICollection<TransTPUTujuan> TransTPUTujuan { get; set; }
    }
}