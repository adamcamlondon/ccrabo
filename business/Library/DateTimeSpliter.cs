using System;
using System.Collections.Generic;
using CongestionCharge.Business.Model;

namespace CongestionCharge.Business.Library
{
    public static class DateTimeSpliter
    {
        public static List<DateRange> SplitIntoDays(DateTime start, DateTime end)
        {
            var days = new List<DateRange>();
            DateTime currentDate = start;
            while (currentDate.Date != end.Date)
            {
                var nextDay = currentDate.AddDays(1);
                var currentDateEnd = new DateTime(nextDay.Year, nextDay.Month, nextDay.Day);
                days.Add(new DateRange(currentDate, currentDateEnd));
                currentDate = currentDateEnd;
            }
            days.Add(new DateRange(currentDate, end));
            return days;
        }
    }
}
