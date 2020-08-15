using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class ConsumeVM
    {
        public int ConsumeID { get; set; }

        public int? DocumentIssueNumber { get; set; }

        public string DocumentIssuedDate { get; set; }

        public int? OrderNumber { get; set; }

        public string OrderDate { get; set; }

        public int EmployeeID { get; set; }

        public int DepartmentID { get; set; }

        public string EmpName { get; set; }

        public string EmpFatherName { get; set; }

        public string EmpOccupation { get; set; }

        public int EmpDepartmentID { get; set; }

        public string EmpDepartmentName { get; set; }

        public string Details { get; set; }

        public string FilePath { get; set; }

        public List<ConsumeItemVM> ConsumeItems { get; set; }

    }
}