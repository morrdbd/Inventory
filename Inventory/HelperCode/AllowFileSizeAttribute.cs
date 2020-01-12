using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOMoRR.HelperCode
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AllowFileSizeAttribute:ValidationAttribute
    {
        public int FileSize { get; set; } = 100 * 1024 * 1024;
        public override bool IsValid(object value)
        {
            // Initialization  
            HttpPostedFileBase file = value as HttpPostedFileBase;
            bool isValid = true;

            // Settings.  
            int allowedFileSize = this.FileSize;

            // Verification.  
            if (file != null)
            {
                // Initialization.  
                var fileSize = file.ContentLength;

                // Settings.  
                isValid = fileSize <= allowedFileSize;
            }

            // Info  
            return isValid;
        }
    }
}