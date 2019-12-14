using Inventory.Attributes;
using Inventory.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOMoRR.Controllers
{
    public class DistributionTicketPC5Controller : BaseController
    {
        //[AccessControl("Add")]
        public ActionResult Add()
        {
            return View("Form");
        }
        //public FileResult AddTicket()
        //{
        //    //using (XLWorkbook wb = new XLWorkbook(Server.MapPath("~/WinterizationSupport/Winterization_Assessment_All_Country_Dec-2019.xlsx")))
        //    //{
        //    //    IXLWorksheet masterList = wb.Worksheet(@"Master list");
        //    //    IXLWorksheet beneficiaryList = wb.Worksheet(@"Beneficiary Distribution List");
        //    //    masterList.Style.Border.InsideBorder = XLBorderStyleValues.Dashed;
        //    //    var rowStart = 5;
        //    //    var benefListRowStart = 8;
        //    //    if (mapped.Count > 0)
        //    //    {

        //    //        beneficiaryList.Rows(9, 8 + mapped.Count).Height = 86;
        //    //        int counter = 0;

        //    //        foreach (var provinceData in byProvince)
        //    //        {
        //    //            for (int i = 1; i <= provinceData.Count(); i++)
        //    //            {
        //    //                counter += 1;
        //    //                //Insert recode in beneficiary list
        //    //                beneficiaryList.Cell(benefListRowStart + counter, 1).Value = provinceData.Key + "-" + i;
        //    //                beneficiaryList.Cell(benefListRowStart + counter, 2).Value = mapped[i].Name;
        //    //                beneficiaryList.Cell(benefListRowStart + counter, 3).Value = mapped[i].FatherName;
                            
        //    //                masterList.Cell(rowStart + counter, 55).Value = attachmentURL + mapped[i].FormUuID + "/" + mapped[i]._uuid + "/" + mapped[i].FaceImage;
        //    //                masterList.Cell(rowStart + counter, 56).Value = attachmentURL + mapped[i].FormUuID + "/" + mapped[i]._uuid + "/" + mapped[i].TazkiraImage;
        //    //                masterList.Cell(rowStart + counter, 57).Value = attachmentURL + mapped[i].FormUuID + "/" + mapped[i]._uuid + "/" + mapped[i].AlternateFaceImage;
        //    //                masterList.Cell(rowStart + counter, 58).Value = attachmentURL + mapped[i].FormUuID + "/" + mapped[i]._uuid + "/" + mapped[i].AlternateTazkiraImage;
        //    //            }
        //    //        }
        //    //        //Insert record to bene
        //    //    }

        //    //    using (MemoryStream stream = new MemoryStream())
        //    //    {
        //    //        wb.SaveAs(stream);
        //    //        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "HAP Winterization all country assessment List.xlsx");
        //    //    }
        //    //}
        //    //return Content("h");
        //}
    }
}