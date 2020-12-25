using System;
using System.Globalization;
using System.Linq;

namespace ContractAndProjectManager.Helpers
{
    public static class DateHelpers
    {
        public static string DateFormat(DateTime dateTime, bool noTime = false) => dateTime.ToString(noTime ? "dd.MM.yyyy" : "dd.MM.yyy hh:mm");
        public static string GetNameFromMonthNumber(int num, bool complete)
        {
            var name = (new DateTime(2010, num, 1)
                .ToString(complete ? "MMMM" : "MMM", new CultureInfo("ru")) + (complete ? "" : "."));
            return name.First().ToString().ToUpper() + name.Substring(1);
        }
    }
    
}