using DOMoRR.Attributes;
using DOMoRR.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOMoRR.Models.ViewModels
{
    public class ReturneeSyncVM
    {
        public int CaseId { get; set; }
        public IndividualVM[] Individuals { get; set; }
        public string Photo { get; set; }
        public string[] Referral { get; set; }
        public string[] Vulner { get; set; }
    }

    public class IndividualVM
    {
        [Required]
        public string Forename { get; set; }

        [Required]
        public string FatherName { get; set; }

        [Required]
        [InValues(Values = "M|F")]
        public string Gender { get; set; }

        [Required]
        public string MaritalStatus { get; set; }

        [Range(1, 100)]
        public int Age { get; set; }

        public string IdType { get; set; }
        public string IdNo { get; set; }
        public string EducationLevel { get; set; }

        [Required]
        public string Relation { get; set; }
    }
}