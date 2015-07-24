using System;
using CongestionCharge.Business.Model;

namespace CongestionCharge.Business.Library
{
    public static class DateDayEnumParser
    {

        public static Day ParseDay(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return Day.Monday;
                case DayOfWeek.Tuesday:
                    return Day.Tuesday;
                case DayOfWeek.Wednesday:
                    return Day.Wednesday;
                case DayOfWeek.Thursday:
                    return Day.Thursday;
                case DayOfWeek.Friday:
                    return Day.Friday;
                case DayOfWeek.Saturday:
                    return Day.Saturday;
                case DayOfWeek.Sunday:
                    return Day.Sunday;
                default:
                    return Day.Weekend; 
            }
        }
    }
}
