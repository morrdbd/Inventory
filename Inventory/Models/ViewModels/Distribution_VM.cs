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
    public class Distribution_VM
    {
        public int DistributionID { get; set; }

        public int? TicketNumber { get; set; }

        public string TicketIssuedDateForm { get; set; }

        [StringLength(500)]
        public string Warehouse { get; set; }

        public int? RequestNumber { get; set; }

        public string RequestDateForm { get; set; }

        public int EmployeeID { get; set; }

        public string EmpName { get; set; }

        public string EmpFatherName { get; set; }

        public string EmpOccupation { get; set; }

        public int EmpDepartmentID { get; set; }

        public string EmpDepartmentName { get; set; }

        public string Details { get; set; }

        public string FilePath { get; set; }

        public bool IsSecondHand { get; set; }

        public int? SecondHandPrice { get; set; }

        public List<DistributionItemVM> DistributionItems { get; set; }
    }
    

}