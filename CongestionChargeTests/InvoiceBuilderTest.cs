using System;
using System.Collections.Generic;
using System.Linq;
using CongestionCharge.Business;
using CongestionCharge.Business.Model;
using NUnit.Framework;

namespace CongestionChargeTests
{
    [TestFixture]
    public class InvoiceBuilderTest
    {
        [Test]
        public void Build_Invoice_With_Single_Day_Values()
        {
            var dailySummaries = new List<IChargeSummary>
                {
                    new ChargeSummary
                        {
                            RateDescription = "AM rate",
                            Cost = 0.93333333334m,
                            TimeSpent = new TimeSpan(0, 28, 0)
                        },
                    new ChargeSummary
                        {
                            RateDescription = "PM rate",
                            Cost = 6.75m,
                            TimeSpent = new TimeSpan(2, 42, 0)
                        }
                };

            var invoiceBuilder = new InvoiceBuilder();
            var invoices = invoiceBuilder.BuildInvoice(dailySummaries);

            Assert.AreEqual(3, invoices.Count);

            var am = invoices.First(i => i.Description.Contains("AM rate"));
            var pm = invoices.First(i => i.Description.Contains("PM rate"));
            var total = invoices.First(i => i.Description.Contains("Total"));


            Assert.AreEqual("£0.90", am.Value);
            Assert.AreEqual("Charge for 0h 28m (AM rate):", am.Description);

            Assert.AreEqual("£6.70", pm.Value);
            Assert.AreEqual("Charge for 2h 42m (PM rate):", pm.Description);

            Assert.AreEqual("£7.60", total.Value);
            Assert.AreEqual("Total Charge:", total.Description);
        }


        [Test]
        public void Build_Invoice_With_PM_Rate_And_Empty_AM()
        {
            var dailySummaries = new List<IChargeSummary>
                {
                    new ChargeSummary
                        {
                            RateDescription = "AM rate",
                            Cost = 0.0m,
                            TimeSpent = new TimeSpan(0, 0, 0)
                        },
                    new ChargeSummary
                        {
                            RateDescription = "PM rate",
                            Cost = 2m,
                            TimeSpent = new TimeSpan(2, 0, 0)
                        }
                };


            var invoiceBuilder = new InvoiceBuilder();
            var invoices = invoiceBuilder.BuildInvoice(dailySummaries);

            Assert.AreEqual(3, invoices.Count);

            var am = invoices.First(i => i.Description.Contains("AM rate"));
            var pm = invoices.First(i => i.Description.Contains("PM rate"));
            var total = invoices.First(i => i.Description.Contains("Total"));


            Assert.AreEqual("£0.00", am.Value);
            Assert.AreEqual("Charge for 0h 0m (AM rate):", am.Description);

            Assert.AreEqual("£2.00", pm.Value);
            Assert.AreEqual("Charge for 2h 0m (PM rate):", pm.Description);

            Assert.AreEqual("£2.00", total.Value);
            Assert.AreEqual("Total Charge:", total.Description);
        }

        [Test]
        public void Build_Invoice_With_Multiple_Days_AM_and_PM()
        {
            var dailySummaries = new List<IChargeSummary>
                {
                    new ChargeSummary
                        {
                            RateDescription = "AM rate",
                            Cost = 3.233333333333333333333334m,
                            TimeSpent = new TimeSpan(1, 37, 0)
                        },
                    new ChargeSummary
                        {
                            RateDescription = "PM rate",
                            Cost = 17.5m,
                            TimeSpent = new TimeSpan(7, 0, 0)
                        },
                     new ChargeSummary
                        {
                            RateDescription = "AM rate",
                            Cost = 4.066666666666666666666666m,
                            TimeSpent = new TimeSpan(2, 02, 0)
                        },
                    new ChargeSummary
                        {
                            RateDescription = "PM rate",
                            Cost = 0m,
                            TimeSpent = new TimeSpan(0, 0, 0)
                        }
                };


            var invoiceBuilder = new InvoiceBuilder();
            var invoices = invoiceBuilder.BuildInvoice(dailySummaries);

            Assert.AreEqual(3, invoices.Count);

            var am = invoices.First(i => i.Description.Contains("AM rate"));
            var pm = invoices.First(i => i.Description.Contains("PM rate"));
            var total = invoices.First(i => i.Description.Contains("Total"));


            Assert.AreEqual("£7.30", am.Value);
            Assert.AreEqual("Charge for 3h 39m (AM rate):", am.Description);

            Assert.AreEqual("£17.50", pm.Value);
            Assert.AreEqual("Charge for 7h 0m (PM rate):", pm.Description);

            Assert.AreEqual("£24.80", total.Value);
            Assert.AreEqual("Total Charge:", total.Description);
        }
    }
}
