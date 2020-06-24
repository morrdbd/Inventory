using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class DisposableDistributionVM
    {
        public int DisposableDistributionID { get; set; }

        public int? DocumentIssueNumber { get; set; }

        public string DocumentIssuedDate { get; set; }

        public int? OrderNumber { get; set; }

        public string OrderDate { get; set; }

        public int DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public string Details { get; set; }

        public string FilePath { get; set; }

        public List<DisposableDistributionItemVM> DistributionItems { get; set; }

    }
}