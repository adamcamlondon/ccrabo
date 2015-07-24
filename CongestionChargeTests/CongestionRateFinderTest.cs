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
    public class CongestionRateFinderTest
    {
      
        [Test]
        public void Returns_Nothing_For_Weekend_Car()
        {
            var congestionPeriods = BuildRealExamples();
            var saturday = new DateTime(2015, 07, 19, 13, 30, 0);
            var foundPeriods = CongestionRateFinder.GetCongestionRatesForDay(saturday, VehicleType.Car, congestionPeriods);
            Assert.AreEqual(0, foundPeriods.Count);
        }

        [Test]
        public void Returns_Nothing_For_Weekend_Motorcycle()
        {
            var congestionPeriods = BuildRealExamples();
            var saturday = new DateTime(2015, 07, 19, 13, 30, 0);
            var foundPeriods = CongestionRateFinder.GetCongestionRatesForDay(saturday, VehicleType.Motorcycle, congestionPeriods);
            Assert.AreEqual(0, foundPeriods.Count);
        }

        [Test]
        public void Returns_Two_For_Weekday_Car()
        {
            var congestionPeriods = BuildRealExamples();
            var tuesday = new DateTime(2015, 07, 21, 13, 30, 0);
            var foundPeriods = CongestionRateFinder.GetCongestionRatesForDay(tuesday, VehicleType.Car, congestionPeriods);
            Assert.AreEqual(2, foundPeriods.Count);

            var morning = foundPeriods.First(p => p.Rate == 2m);
            Assert.IsNotNull(morning);

            var afternoon = foundPeriods.First(p => p.Rate == 2.5m);
            Assert.IsNotNull(afternoon);

        }

        [Test]
        public void Returns_Two_For_Weekday_Motorcycle()
        {
            var congestionPeriods = BuildRealExamples();
            var tuesday = new DateTime(2015, 07, 21, 13, 30, 0);
            var foundPeriods = CongestionRateFinder.GetCongestionRatesForDay(tuesday, VehicleType.Motorcycle, congestionPeriods);
            Assert.AreEqual(2, foundPeriods.Count);
            var motocyclePeriodAm = foundPeriods.First(rp => rp.Description == "AM rate");
            var motocyclePeriodPm = foundPeriods.First(rp => rp.Description == "PM rate");
            Assert.IsNotNull(motocyclePeriodAm);
            Assert.IsNotNull(motocyclePeriodPm);

            Assert.AreEqual(1m, motocyclePeriodAm.Rate);
            Assert.AreEqual(1m, motocyclePeriodPm.Rate);
        }


        [Test]
        public void Returns_Two_For_Weekday_Car_Conflicting_Times_Returns_Most_Specific()
        {
            var congestionPeriods = BuildRealExamples();

            //Add another conflicting entry specifically for wednesday
            congestionPeriods.Add(new CongestionRate("Special Wednesday PM rate", Day.Wednesday, VehicleType.FullRateVehicle, 0m,
                                         new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month,
                                                      DateTime.MinValue.Day, 12, 0, 0),
                                         new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month,
                                                      DateTime.MinValue.Day, 19, 0, 0)));


            var wednesday = new DateTime(2015, 07, 22, 13, 30, 0);
            var foundPeriods = CongestionRateFinder.GetCongestionRatesForDay(wednesday, VehicleType.Car, congestionPeriods);
            Assert.AreEqual(2, foundPeriods.Count);

            var morning = foundPeriods.First(p => p.Rate == 2m);
            Assert.IsNotNull(morning);

            var afternoon = foundPeriods.First(p => p.Rate == 0m);
            Assert.IsNotNull(afternoon);
            Assert.AreEqual("Special Wednesday PM rate", afternoon.Description);
        }


        [Test]
        public void Returns_Two_For_Weekday_Van_Conflicting_Vehicles_And_Days_Returns_Most_Specific_Vehicle()
        {
            var congestionPeriods = BuildRealExamples();

            //Add another conflicting entry specifically for wednesday
            congestionPeriods.Add(new CongestionRate("Special Wednesday PM rate", Day.Wednesday, VehicleType.FullRateVehicle, 0m,
                                         new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month,
                                                      DateTime.MinValue.Day, 12, 0, 0),
                                         new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month,
                                                      DateTime.MinValue.Day, 19, 0, 0)));

            congestionPeriods.Add(new CongestionRate("Special Wednesday PM Van rate", Day.Wednesday, VehicleType.Van, 10m,
                                        new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month,
                                                     DateTime.MinValue.Day, 12, 0, 0),
                                        new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month,
                                                     DateTime.MinValue.Day, 19, 0, 0)));



            var wednesday = new DateTime(2015, 07, 22, 13, 30, 0);
            var foundPeriods = CongestionRateFinder.GetCongestionRatesForDay(wednesday, VehicleType.Van, congestionPeriods);
            Assert.AreEqual(2, foundPeriods.Count);

            var morning = foundPeriods.First(p => p.Rate == 2m);
            Assert.IsNotNull(morning);

            var afternoon = foundPeriods.First(p => p.Rate == 10m);
            Assert.IsNotNull(afternoon);
            Assert.AreEqual("Special Wednesday PM Van rate", afternoon.Description);
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
