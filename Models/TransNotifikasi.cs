using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ira.Models
{
    public class TransNotifikasi
    {
        [Key]
        public int ID { get; set; }
        [StringLength(300)]
        public string body { get; set; }
        public DateTime Date { get; set; }
        [StringLength(20)]
        public string Controller { get; set; }
        [StringLength(100)]
        public string name { get; set; }
        [StringLength(20)]
        public string Action { get; set; }
        public int ? RouteID { get; set; }
        public int RoleID { get; set; }
        public int UnitID { get; set; }
        public int NotifType { get; set; }
    }
}