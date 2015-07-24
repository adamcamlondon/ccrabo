using System;
using System.Collections.Generic;
using CongestionCharge.Business;
using CongestionCharge.Business.Model;
using NUnit.Framework;

namespace CongestionChargeTests
{
    [TestFixture]
    public class CongestionRateStoreTest
    {

        [Test]
        public void Returns_Correct_Filtered_Rates()
        {

            var congestioNRateStore = new CongestionRateStore(BuildRealExamples());
            var monday = new DateTime(2015, 07, 20, 10, 0, 0, 0);
            var rates = congestioNRateStore.GetRates(monday, VehicleType.Car);
            Assert.AreEqual(2, rates.Count);

            var amRate = rates.Find(r => r.Description == "AM rate");
            var pmRate = rates.Find(r => r.Description == "PM rate");

            Assert.AreEqual(2m, amRate.Rate);
            Assert.AreEqual(VehicleType.FullRateVehicle, amRate.Vehicle);

            Assert.AreEqual(2.5m, pmRate.Rate);
            Assert.AreEqual(VehicleType.FullRateVehicle, pmRate.Vehicle);
        }



        private List<ICongestionRate> BuildRealExamples()
        {
            return new List<ICongestionRate>
                {
                    new CongestionRate("AM rate",Day.Weekday, VehicleType.FullRateVehicle, 2m,
                                         new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month,
                                                      DateTime.MinValue.Day, 07, 0, 0),
                                         new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month,
                                                      DateTime.MinValue.Day, 12, 0, 0)),

                    new CongestionRate("PM rate", Day.Weekday, VehicleType.FullRateVehicle, 2.5m,
                                         new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month,
                                                      DateTime.MinValue.Day, 12, 0, 0),
                                         new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month,
                                                      DateTime.MinValue.Day, 19, 0, 0)),

                    new CongestionRate("AM rate", Day.Weekday,
                                         VehicleType.DicountRateVehicle, 1m,
                                         new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month,
                                                      DateTime.MinValue.Day, 07, 0, 0),
                                         new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month,
                                                      DateTime.MinValue.Day, 12, 0, 0)),
                     new CongestionRate("PM rate", Day.Weekday,
                                         VehicleType.DicountRateVehicle, 1m,
                                         new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month,
                                                      DateTime.MinValue.Day, 12, 0, 0),
                                         new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month,
                                                      DateTime.MinValue.Day, 19, 0, 0))
                };
        }
    }
}
