using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ira.Models
{
    public class MailModel
    {
        //public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class ConfigMailView
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class ConfigMailAdvanced
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DisplayName("SMTP Host")]
        public string Host { get; set; }

        [DisplayName("SMTP Port")]
        public int Port { get; set; }

        [DisplayName("Is Body HTML")]
        public bool isBodyHtml { get; set; }

        [DisplayName("Use Default Credentials")]
        public bool useDefaultCredential { get; set; }

        [DisplayName("Enable SSL")]
        public bool enableSSL { get; set; }
    }

    public class PegawaiListViewModel
    {
        public int ID { get; set; }

        [DisplayName("Nama Pegawai")]
        public string PegName { get; set; }

        [DisplayName("NIP")]
        public string PegNIP { get; set; }

        [DisplayName("Unit")]
        public string RefUnitPJ { get; set; }

        [DisplayName("Email Kemenkeu")]
        public string PegEmailKemenkeu { get; set; }
    }

    public class TPUListViewModel
    {
        public int ID { get; set; }

        [DisplayName("Judul TPU")]
        public string TPUName { get; set; }

        [DisplayName("Jenis TPU")]
        public string TPUJenis { get; set; }

        [DisplayName("Unit")]
        public string UnitPJ { get; set; }

        [DisplayName("PIC")]
        public string TPUNamaPIC { get; set; }

        [DisplayName("Status TPU")]
        public string TPUStatus { get; set; }
    }

    //public class KegiatanFinalizeModel
    //{
    //    public int ID { get; set; }
    //    public int Finalize { get; set; }
    //}

    //public class KegiatanListViewModel
    //{
    //    public int ID { get; set; }
    //    public string KegName { get; set; }
    //    public int MyProperty { get; set; }
    //}

    public class TPUFinalizeModel
    {
        public int ID { get; set; }
        public int Finalize { get; set; }
    }

    //public class FlashKegiatanProgress {
    //    public string Detail { get; set; }
    //    public DateTime PeriodeID { get; set; }
    //    public int KegiatanID { get; set; }
    //    public int KegStatusID { get; set; }

    //}

    public class CallforReport
    {
        public int ID { get; set; }
        [DisplayName("Nomor Nota Dinas")]
        [StringLength(100)]
        public string NomorND { get; set; }
        [DisplayName("Tanggal Nota Dinas")]
        public DateTime TanggalND { get; set; }
        [DisplayName("Periode Pelaporan")]
        public int PeriodeID { get; set; }
    }

    public class AddKegiatan {
        [Key]
        public int ID { get; set; }

        [DisplayName("Judul Kegiatan")]
        [StringLength(200)]
        public string KegName { get; set; }

        [DisplayName("Judul Tema Pengawasan")]
        public int KegiatanTPUID { get; set; }

        [DisplayName("Manajer Kegiatan")]
        public int KegMjrID { get; set; }
        
        [DisplayName("Periode Mulai")]
        public int PeriodeID { get; set; }
        
        [DisplayName("Target Penyelesaian")]
        public System.DateTime KegTarget { get; set; }
        
        [DisplayName("Keterangan Tambahan")]
        [StringLength(300)]
        
        public string Keterangan { get; set; }
        
        public int Finalize { get; set; }
        
        [StringLength(100)]
        public string SysUsername { get; set; }
        
        public Nullable<System.DateTime> SysTglEntry { get; set; }
        
        [StringLength(100)]
        public string SysWorkstation { get; set; }

        public int ND { get; set; }
    }
}