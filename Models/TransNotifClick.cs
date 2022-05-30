using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ira.Models
{
    public class TransNotifClick
    {
        public int ID { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        public DateTime Date { get; set; }
    }
}