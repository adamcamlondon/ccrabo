using System;
using System.Collections.Generic;
using CongestionCharge.Business;
using CongestionCharge.Business.Model;
using Moq;
using NUnit.Framework;

namespace CongestionChargeTests
{
    [TestFixture]
    public class CongestionChargeCalculatorTest
    {

        [Test]
        public void Parse_Single_Day_RateFinder_InvoiceBuilder_Called()
        {

           var invoiceBuilder = new Mock<IInvoiceBuilder>();
           var congestionRateStore = new Mock<ICongestionRateStore>();

            var startTime = new DateTime(2008, 04, 24, 11, 32, 0);
            var endTime = new DateTime(2008, 04, 24, 14, 42, 0);


            congestionRateStore.Setup(crs => crs.GetRates(It.IsAny<DateTime>(), VehicleType.Car)).Returns(new List<ICongestionRate>
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
                  
                                                                                                                });

            invoiceBuilder.Setup(ib => ib.BuildInvoice(It.IsAny<List<IChargeSummary>>())).Returns(new List<ICongestionChargeInvoice>());

            var congestionCalculator = new CongestionChargeCalculator(congestionRateStore.Object, invoiceBuilder.Object);
            congestionCalculator.CalculateCost(startTime, endTime, VehicleType.Car);


            congestionRateStore.Verify(crs => crs.GetRates(It.IsAny<DateTime>(), VehicleType.Car), Times.Exactly(1));
            invoiceBuilder.Verify(ib => ib.BuildInvoice(It.IsNotNull<List<IChargeSummary>>()), Times.Exactly(1));
        }


        [Test]
        public void Parse_Multiple_Day_RateFinder_InvoiceBuilder_Called()
        {

            var invoiceBuilder = new Mock<IInvoiceBuilder>();
            var congestionRateStore = new Mock<ICongestionRateStore>();

            var startTime = new DateTime(2008, 04, 23, 11, 32, 0);
            var endTime = new DateTime(2008, 04, 24, 14, 42, 0);


            congestionRateStore.Setup(crs => crs.GetRates(It.IsAny<DateTime>(), VehicleType.Car)).Returns(new List<ICongestionRate>
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
                  
                                                                                                                });

            invoiceBuilder.Setup(ib => ib.BuildInvoice(It.IsAny<List<IChargeSummary>>())).Returns(new List<ICongestionChargeInvoice>());

            var congestionCalculator = new CongestionChargeCalculator(congestionRateStore.Object, invoiceBuilder.Object);
            congestionCalculator.CalculateCost(startTime, endTime, VehicleType.Car);


            congestionRateStore.Verify(crs => crs.GetRates(It.IsAny<DateTime>(), VehicleType.Car), Times.Exactly(2));
            invoiceBuilder.Verify(ib => ib.BuildInvoice(It.IsNotNull<List<IChargeSummary>>()), Times.Exactly(1));
        }
    }
}
