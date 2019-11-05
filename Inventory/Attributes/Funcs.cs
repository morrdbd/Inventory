using MD.PersianDateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOMoRR.Attributes
{
    public static class Funcs
    {
        public static string ToFaDigits(int number)
        {
            string source = number.ToString();
            var Fa = "۰۱۲۳۴۵۶۷۸۹".ToCharArray();
            var En = "0123456789".ToCharArray();

            for (int i = 0; i < En.Count(); i++)
            {
                source = source.Replace(En[i], Fa[i]);
            }
            return source;
        }

        public static string DateFrom(string Lang)
        {
            if (Lang == "en")
            {
                return new DateTime(DateTime.Now.Year, 1, 1).ToString("yyyy-MM-dd");
            }
            else
            {
                var pDate = new PersianDateTime(DateTime.Now);
                var pDateFrom = new PersianDateTime(pDate.Year, 1, 1);
                pDateFrom.EnglishNumber = true;
                return pDateFrom.ToString("yyyy-MM-dd");
            }
        }

        public static string DateTo(string Lang)
        {
            if (Lang == "en")
            {
                return new DateTime(DateTime.Now.Year, 12, 31).ToString("yyyy-MM-dd");
            }
            else
            {
                var pDate = new PersianDateTime(DateTime.Now);
                var pDateTo = new PersianDateTime(pDate.Year, 12, pDate.IsLeapYear ? 30 : 29);
                pDateTo.EnglishNumber = true;
                return pDateTo.ToString("yyyy-MM-dd");
            }
        }

        public static DateTime? ToDate(this string Date, string Lang)
        {
            if (!string.IsNullOrEmpty(Date))
            {
                if (Lang == "en")
                {
                    return DateTime.Parse(Date);
                }
                else
                {
                    return PersianDateTime.Parse(Date).ToDateTime();
                }
            }
            return null;
        }

        public static string ToDateString(this DateTime? Date, string Lang)
        {
            if (Date != null)
            {
                if (Lang == "en")
                {
                    return Convert.ToDateTime(Date).ToString("yyyy-MM-dd");
                }
                else
                {
                    var pDate = new PersianDateTime(Date);
                    pDate.EnglishNumber = true;
                    return pDate.ToString("yyyy-MM-dd");
                }
            }
            return string.Empty;
        }
    }
}