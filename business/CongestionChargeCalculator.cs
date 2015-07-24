using System;
using System.Collections.Generic;
using CongestionCharge.Business.Model;
using CongestionCharge.Business.Library;

namespace CongestionCharge.Business
{

	public interface ICongestionChargeCalculator
    {
        List<ICongestionChargeInvoice> CalculateCost(DateTime start, DateTime end, VehicleType vehicleType);
    }

    public class CongestionChargeCalculator : ICongestionChargeCalculator
    {
        private readonly ICongestionRateStore _congestionRateStore;
        private readonly IInvoiceBuilder _invoiceBuilder;


        public CongestionChargeCalculator(ICongestionRateStore congestionRateStore, IInvoiceBuilder invoiceBuilder)
        {
            _congestionRateStore = congestionRateStore;
            _invoiceBuilder = invoiceBuilder;
        }

        public List<ICongestionChargeInvoice> CalculateCost(DateTime start, DateTime end, VehicleType vehicleType)
        {
	        var days = DateTimeSpliter.SplitIntoDays(start, end);
            var chargeSummaries = new List<IChargeSummary>();
	        foreach (DateRange timeSegment in days)
	        {
				var applicableChargeRates = _congestionRateStore.GetRates(timeSegment.Start, vehicleType);
                if(applicableChargeRates == null || applicableChargeRates.Count == 0) continue;

	            var dailyRateCalculations = RateCalculator.Calculate(timeSegment.Start, timeSegment.End, applicableChargeRates);

                if (dailyRateCalculations != null && dailyRateCalculations.Count > 0) chargeSummaries.AddRange(dailyRateCalculations);
	        }

            return _invoiceBuilder.BuildInvoice(chargeSummaries);
        }

    }
}
