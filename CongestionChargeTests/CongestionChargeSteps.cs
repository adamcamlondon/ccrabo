using System;
using System.Collections.Generic;
using System.Globalization;
using CongestionCharge.Business;
using CongestionCharge.Business.Library;
using CongestionCharge.Business.Model;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace CongestionChargeTests
{
    [Binding]
    public class CongestionChargeSteps
    {
        private List<ICongestionRate> _rates;
        private VehicleType _vehicleType;
        private DateTime _start;
        private DateTime _end;
        private List<ICongestionChargeInvoice> _results;


        [Given]
        public void Given_The_Congestion_Rates_Are(Table table)
        {
            _rates = new List<ICongestionRate>();

            foreach (var tableRow in table.Rows)
                _rates.Add(BuildFromRow(tableRow));
        }
        
        [Given]
        public void Given_I_am_driving_a_car()
        {
            _vehicleType = VehicleType.Car;
        }
        
        [Given]
        public void Given_I_am_driving_a_Motorbike()
        {
            _vehicleType = VehicleType.Motorcycle;
        }
        
        [Given]
        public void Given_I_am_driving_a_Van()
        {
            _vehicleType = VehicleType.Van;
        }
        
        [When]
        public void When_I_enter_the_congestion_charge_zone_at_P0_P1_P2(string p0, string p1, int p2)
        {
            _start = ParseToDate(p0, p1, p2);
        }
        
        [When]
        public void When_I_leave_the_congestion_charge_zone_at_P0_P1_P2(string p0, string p1, int p2)
        {
            _end = ParseToDate(p0, p1, p2);

            var congestionRateStore = new CongestionRateStore(_rates);
            var congestionCalculator = new CongestionChargeCalculator(congestionRateStore, new InvoiceBuilder());
            _results = congestionCalculator.CalculateCost(_start, _end, _vehicleType);
        }
        
        [Then]
        public void Then_I_should_get_the_following(Table table)
        {
            Assert.AreEqual(table.Rows.Count, _results.Count);

            for (int i = 0; i < _results.Count; i++)
            {
                Assert.AreEqual(table.Rows[i]["Description"], _results[i].Description);
                Assert.AreEqual(table.Rows[i]["Value"], _results[i].Value);
            }
        }



        #region Helper Methods


        private ICongestionRate BuildFromRow(TableRow row)
        {
            var description = row["Description"];
            var day = row["Day"].ToEnum<Day>();
            var vehicle = row["Vehicle"].ToEnum<VehicleType>();
            var startStr = row["Start"];
            var endstr = row["End"];

            var start = ParseToDate(startStr);
            var end = ParseToDate(endstr);
            var rate = decimal.Parse(row["Rate"]);
            return new CongestionRate(description, day, vehicle, rate, start, end);
        }

        private DateTime ParseToDate(string hourmin)
        {
            var item = hourmin.Split(':');
            if (item.Length != 2) return DateTime.MinValue;

            var hour = int.Parse(item[0]);
            var min = int.Parse(item[1]);
            return new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, hour, min, 0);
        }

        private DateTime ParseToDate(string date, string hourstr, int min)
        {
            var dateTemp = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var hour = int.Parse(hourstr);
            return new DateTime(dateTemp.Year, dateTemp.Month, dateTemp.Day, hour, min, 0);
        }
        #endregion
    }
}
