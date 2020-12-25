using System;

namespace ContractAndProjectManager.Models
{
    public class MonthModel
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public int Days { get; set; }
        public int DaysStartOverall { get; set; }
        public int DaysEndOverall { get; set; }

        public string Key => Date.Month.ToString() + Date.Year;
    }
}