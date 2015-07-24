using System;

namespace CongestionCharge.Business.Model
{
    public interface IChargeSummary
    {
         string RateDescription { get; set; }
         TimeSpan TimeSpent { get; set; }
         decimal Cost { get; set; }
    }

    public class ChargeSummary : IChargeSummary
    {
        public string RateDescription { get; set; }
        public TimeSpan TimeSpent { get; set; }
        public decimal Cost { get; set; }
    }
}
