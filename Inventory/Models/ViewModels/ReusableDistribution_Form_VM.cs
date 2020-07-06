﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class ReusableDistribution_Form_VM
    {
        public int ReusableDistributionID { get; set; }

        public int? TicketNumber { get; set; }

        public string TicketIssuedDateForm { get; set; }

        public string Warehouse { get; set; }

        public int? RequestNumber { get; set; }

        public string RequestDateForm { get; set; }

        public int EmpID { get; set; }

        public string EmpName { get; set; }

        public string EmpFatherName { get; set; }

        public string EmpOccupation { get; set; }

        public int EmpDepartmentID { get; set; }

        public string EmpDepartmentName { get; set; }

        public string Details { get; set; }

        public ReusableDistributionItemVM[] DistributionItems { get; set; }
    }
}