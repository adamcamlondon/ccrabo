using System;
using System.Collections.Generic;
using System.Linq;
using CongestionCharge.Business.Model;

namespace CongestionCharge.Business.Library
{

	public static class RateCalculator
	{

		public static List<ChargeSummary> Calculate(DateTime start, DateTime end, List<ICongestionRate> rates)
		{
            var calculatedRates = rates.Select(congestionRate => BuildSummary(start, end, congestionRate)).ToList();

		    return calculatedRates;
		}


        private static ChargeSummary BuildSummary(DateTime start, DateTime end, ICongestionRate rate)
        {
            decimal charge = 0m;
            var time = new TimeSpan(0,0,0,0);
            var intersectingTime = GetIntersectingTime(start, end, rate);

            if (intersectingTime != null)
            {
               time = intersectingTime.End - intersectingTime.Start;
                charge = CalculateRate(time, rate);
            }
          
            return new ChargeSummary
                {
                    Cost = charge,
                    TimeSpent = time,
                    RateDescription = rate.Description
                };
        }

        private static decimal CalculateRate(TimeSpan chargeableTime, ICongestionRate rate)
        {
            decimal charge = 0m;

            if (chargeableTime.Days > 0)
            {
                charge += chargeableTime.Days * (rate.Rate * 24);
            }

            if (chargeableTime.Hours > 0)
            {
                charge += chargeableTime.Hours*rate.Rate;
            }

            if (chargeableTime.Minutes > 0)
            {
                var convert = (decimal) chargeableTime.Minutes/60;

                charge += convert * rate.Rate;
            }

            return charge; 
        }

        private static DateRange GetIntersectingTime(DateTime start, DateTime end, ICongestionRate rate)
        {
            var timeSpent = new DateRange(start, end);
            var congestionRateRange = new DateRange(AddDatePartToCongestionTime(start, rate.Start), AddDatePartToCongestionTime(start, rate.End, true));
            return DateTimeIntersect.FindIntersect(timeSpent, congestionRateRange);
        }


        private static DateTime AddDatePartToCongestionTime(DateTime date, DateTime dateToAmmend, bool isEnd = false)
        {
             //If the end time is set to midnight Assume it's the next day
             if (isEnd && dateToAmmend.Hour == 0 && dateToAmmend.Minute == 0)
                return new DateTime(date.Year, date.Month, date.AddDays(1).Day, dateToAmmend.Hour, dateToAmmend.Minute, dateToAmmend.Second);

            return new DateTime(date.Year, date.Month, date.Day, dateToAmmend.Hour, dateToAmmend.Minute, dateToAmmend.Second);
        }

   
	}
}
