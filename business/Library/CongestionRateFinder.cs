using System;
using System.Collections.Generic;
using System.Linq;
using CongestionCharge.Business.Model;

namespace CongestionCharge.Business.Library
{
    public static class CongestionRateFinder
    {

        public static List<ICongestionRate> GetCongestionRatesForDay(DateTime date, VehicleType type, List<ICongestionRate> congestionPeriods)
        {
            var day = DateDayEnumParser.ParseDay(date);
            var periods = congestionPeriods.Where(cr => cr.Vehicle.HasFlag(type) && cr.Day.HasFlag(day)).ToList();
            return FilterOutConflicting(periods, day, type);
        }

        private static List<ICongestionRate> FilterOutConflicting(List<ICongestionRate> periods, Day day, VehicleType type)
        {
            if (periods.Count == 1) return periods;
            var excludedList = new List<int>();

            for (int i = 0; i < periods.Count; i++)
            {
                if (excludedList.Contains(i)) continue;
                for (int j = 0; j < periods.Count; j++)
                {
                    if (j == i) continue;
                    var result = IsConflictOrOverLap(periods[i], periods[j], day, type);
                    if (result != -1)
                    {
                        excludedList.Add(result == 0 ? i : j);
                    }
                }
            }
            return excludedList.Count == 0 ? periods : periods.Where((t, i) => !excludedList.Contains(i)).ToList();
        }


        /// <summary>
        /// Returns -1 no conflict, Return 0 ignore first Return 1 ignore 2nd
        /// </summary>
        private static int IsConflictOrOverLap(ICongestionRate cp1, ICongestionRate cp2, Day day, VehicleType type)
        {
            var cr1DateRange = new DateRange(cp1.Start, cp1.End);
            var cr2DateRange = new DateRange(cp2.Start, cp2.End);

            var intersect = DateTimeIntersect.FindIntersect(cr1DateRange, cr2DateRange);

            if (intersect == null) return -1;

            return SelectTheMoreSpecific(cp1, cp2, day, type);
        }

        private static int SelectTheMoreSpecific(ICongestionRate cp1, ICongestionRate cp2, Day day, VehicleType type)
        {
            //Check Vehicle
            if (cp1.Vehicle == type) return 1;
            if (cp2.Vehicle == type) return 0;

            //Check Day of the week
            if (cp1.Day == day) return 1;
            if (cp2.Day == day) return 0;

            //OtherWise ignore the 2nd
            return 1;
        }
    }
}
