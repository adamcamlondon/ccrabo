using System;
using System.Collections.Generic;
using System.Linq;
using CongestionCharge.Business;
using CongestionCharge.Business.Library;
using CongestionCharge.Business.Model;
using NUnit.Framework;

namespace CongestionChargeTests
{
    [TestFixture]
    public class RateCalculatorTest
    {

        [Test]
        public void Car_Weekday_Spanning_AM_PM()
        {
            var rates = GetFullRateExamples();
            var start = new DateTime(2008, 04, 24, 11, 32, 0);
            var end = new DateTime(2008, 04, 24, 14, 42, 0);

            var summaryResults = RateCalculator.Calculate(start, end, rates);

            Assert.AreEqual(2, summaryResults.Count);

            var am = summaryResults.First(sr => sr.RateDescription == "AM rate");
            Assert.AreEqual(0.9333m, decimal.Round(am.Cost,4));

            var pm = summaryResults.First(sr => sr.RateDescription == "PM rate");
            Assert.AreEqual(6.75m, pm.Cost);
        }

        [Test]
        public void MotorBike_Weekday_PM()
        {
            var rates = GetDiscountRateExamples();
            var start = new DateTime(2008, 04, 24, 17, 00, 0);
            var end = new DateTime(2008, 04, 24, 22, 11, 0);

            var summaryResults = RateCalculator.Calculate(start, end, rates);

            Assert.AreEqual(2, summaryResults.Count);

            var am = summaryResults.First(sr => sr.RateDescription == "AM rate");
            Assert.AreEqual(0m, am.Cost);

            var pm = summaryResults.First(sr => sr.RateDescription == "PM rate");
            Assert.AreEqual(2m, pm.Cost);
        }

        [Test]
        public void Van_Friday_AM_To_Monday_AM_End()
        {
            var rates = GetFullRateExamples();
            var startFriday = new DateTime(2008, 04, 25, 10, 23, 0);
            var endFriday = new DateTime(2008, 04, 26, 0, 0, 0);

            var startMonday = new DateTime(2008, 04, 28, 0, 0, 0);
            var endMonday = new DateTime(2008, 04, 28, 9, 02, 0);


            var summaryFridayResults = RateCalculator.Calculate(startFriday, endFriday, rates);
            var summaryMondayResults = RateCalculator.Calculate(startMonday, endMonday, rates);

            Assert.AreEqual(2, summaryFridayResults.Count);
            Assert.AreEqual(2, summaryMondayResults.Count);

            var amFriday = summaryFridayResults.First(sr => sr.RateDescription == "AM rate");
            var pmFriday = summaryFridayResults.First(sr => sr.RateDescription == "PM rate");
            var amMonday = summaryMondayResults.First(sr => sr.RateDescription == "AM rate");
            var pmMonday = summaryMondayResults.First(sr => sr.RateDescription == "PM rate");
 
            Assert.AreEqual(7.3m, amFriday.Cost + amMonday.Cost);
            Assert.AreEqual(17.5m, pmFriday.Cost + pmMonday.Cost);
        }



        private List<ICongestionRate> GetFullRateExamples()
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
                  
                };
        }


        private List<ICongestionRate> GetDiscountRateExamples()
        {
            return new List<ICongestionRate>
                {
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
