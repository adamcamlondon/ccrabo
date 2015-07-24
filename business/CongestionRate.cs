using System;
using CongestionCharge.Business.Model;

namespace CongestionCharge.Business
{
    public interface ICongestionRate
    {
         string Description { get; }
         Day Day {get;}
         VehicleType Vehicle { get;}
         decimal Rate { get;}
         DateTime Start { get;}
         DateTime End {get;}
    }

    public class CongestionRate : ICongestionRate
    {
        public string Description { get; private set; }
        public Day Day { get; private set; }
        public VehicleType Vehicle { get; private set; }
        public decimal Rate { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public CongestionRate(string description, Day day, VehicleType vehicle, decimal rate, DateTime start, DateTime end)
        {
            Description = description;
            Day = day;
            Vehicle = vehicle;
            Rate = rate;
            Start = start;
            End = end;
        }
    }
}
