using Inventory.Attributes;
using Inventory.Models.ViewModels;
using MD.PersianDateTime;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models.Repositories
{
    public class ProductRepocitory
    {
        InventoryDBContext db = new InventoryDBContext();


        /// <summary>
        /// Variable use for line chart
        /// </summary>
        public int[] MobileCarRequestPMonth = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static string enThisYear = (DateTime.Now.Year + "-01-01");
        public static string drThisYear = PersianDateTime.Now.Year + "," + "-01-01";
        DateTime thisYear = Convert.ToDateTime(enThisYear);
        //End variable for line chart

        public HighCharts AllMobileCarRequestChart(string lang)
        {
            var label = Helper.monthName(lang);

            if (lang != "en")
            {
                thisYear = PersianDateTime.Parse(drThisYear).ToDateTime();
            }
            CountMobileCarRequestPMonth(lang);
            var allMobileCarRequestByMonth = new HighCharts
            {
                categories = label,
                series = new Series[]
                {
                    new Series
                    {
                        name = Labels.Mobile_Cars,
                        data = MobileCarRequestPMonth.ToArray()
                    }
                }
            };
            return allMobileCarRequestByMonth;
        }

        private void CountMobileCarRequestPMonth(string lang)
        {
            //Suggestion and OrDER Data
            var carRequests = db.MobileCarTickets.Where(c => c.InsertedDate >= thisYear && c.IsActive == true).ToList();
            List<chartData> CarRequestData = new List<chartData>();
            if (lang != "en")
            {
                CarRequestData = carRequests.Select(c => new { InsertedDate = new PersianDateTime(c.InsertedDate) })
                .GroupBy(c => c.InsertedDate.Month).Select(c => new chartData { Month = c.Key, RecPerMonth = c.Count() }).ToList();
            }
            else
            {
                CarRequestData = carRequests.Select(c => c.InsertedDate)
                .GroupBy(c => c.Month).Select(c => new chartData { Month = c.Key, RecPerMonth = c.Count() }).ToList();
            }
            if (CarRequestData.Count() > 0)
            {
                CarRequestData.ForEach(c => MobileCarRequestPMonth[c.Month - 1] = c.RecPerMonth);
            }
        }
    }
}