using Inventory.HelperCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.ViewModels
{
    public class FileUpload
    {
        public int RecordID { get; set; }

        [AllowFileSize(ErrorMessage = "سایز فایل ضمیمه باید کمتر از 100 ام بی باشد")]
        public HttpPostedFileWrapper FileContent { get; set; }
    }
}