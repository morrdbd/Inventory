using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOMoRR.Models.Tables
{
    public class SigninLog
    {
        [Key]
        public int SigninId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserIP { get; set; }
        public DateTime SigninTime { get; set; }
    }
}