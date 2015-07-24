using System;
using System.Collections.Generic;
using CongestionCharge.Business.Library;
using CongestionCharge.Business.Model;

namespace CongestionCharge.Business
{
    public interface ICongestionRateStore
    {
        List<ICongestionRate> GetRates(DateTime day, VehicleType vehicleType);
    }

    public class CongestionRateStore : ICongestionRateStore
    {
        private readonly List<ICongestionRate> _congestionRates;

        public CongestionRateStore(List<ICongestionRate> congestionRates)
        {
            _congestionRates = congestionRates;
        }

        public List<ICongestionRate> GetRates(DateTime date, VehicleType vehicleType)
        {
            if (_congestionRates == null || _congestionRates.Count == 0) return new List<ICongestionRate>();
            return CongestionRateFinder.GetCongestionRatesForDay(date, vehicleType, _congestionRates);
        }
    }
}
