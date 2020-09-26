using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class AssignSharedCarEmailBody_VM
    {
        public string SecondPassVisitingPlace { get; set; }
        public string SecondPassVisitingDateTime { get; set; }
        public string CarType { get; set; }
        public string DriverName { get; set; }
        public string NumberPlate { get; set; }
        public string FirstPassDepartment { get; set; }
        public string FirstPassVisitingPlace { get; set; }
        public string FirstPassVisitingDateTime { get; set; }
    }
}