using Inventory.Models.Procedures;
using Inventory.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class ExpiredVM
    {
        public int ExpiredID { get; set; }

        public int? DocumentIssuedNo { get; set; }

        public string DocumentIssuedDateForm { get; set; }

        public int EmpID { get; set; }

        public string EmpName { get; set; }

        public string EmpFatherName { get; set; }

        public string EmpOccupation { get; set; }

        public int EmpDepartmentID { get; set; }

        public string EmpDepartmentName { get; set; }

        public string Details { get; set; }

        public List<ExpiredItemVM> ExpiredItems { get; set; }

        public ExpiredVM()
        {
            this.ExpiredItems = new List<ExpiredItemVM>();
        }
    }
    

}