using System;

namespace ContractAndProjectManager.Helpers
{
    public static class DateHelpers
    {
        public static string DateFormat(DateTime dateTime, bool noTime = false) => dateTime.ToString(noTime ? "dd.MM.yyyy" : "dd.MM.yyy hh:mm");
    }
}