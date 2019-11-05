using DOMoRR.Models.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOMoRR.Models.ViewModels
{
    public class Home_SC
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public DateTime? _DateFrom { get; set; }
        public DateTime? _DateTo { get; set; }
        public string Lang { get; set; }
    }
    public class DashboardCharts
    {
        public HighCharts Requisition { get; set; }
      
    }

    public class HighCharts
    {
        public string[] categories { get; set; }
        public Series[] series { get; set; }
    }
    public class Series
    {
        public string name { get; set; }
        public int[] data { get; set; }
    }
    public class PieCharts
    {
        public PieSeries[] series { get; set; }
    }
    public class PieSeries
    {
        public string name { get; set; }
        public int y { get; set; }
    }
}