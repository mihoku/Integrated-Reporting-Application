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
    public class ConfigMail
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

        [Required]
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
}