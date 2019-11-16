using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Attributes
{
    public class helper
    {
        public string[] monthName(string lang)
        {
            if(lang != "en")
            {
                return new List<string> { "حمل", "ثور", "جوزا", "سرطان", "اسد", "سمبله", "میزان", "عقرب", "قوس", "جدی", "دلو", "حود" }.ToArray();
            }
            return new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }.ToArray();
        }
    }
}