using System;
using System.Collections.Generic;
using System.Globalization;
using CongestionCharge.Business.Library;
using CongestionCharge.Business.Model;
using System.Linq;

namespace CongestionCharge.Business
{
    public interface IInvoiceBuilder
    {
        List<ICongestionChargeInvoice> BuildInvoice(List<IChargeSummary> dailyCharges);
    }

    public class InvoiceBuilder : IInvoiceBuilder
    {
        private const string CurrencyAppend = "£";
       
        public List<ICongestionChargeInvoice> BuildInvoice(List<IChargeSummary> dailyCharges)
        {
            var invoices = new List<ICongestionChargeInvoice>();
            var groupedByDescription = dailyCharges.GroupBy(dc => dc.RateDescription);

            var total = 0m;
            foreach (var chargeRate in groupedByDescription)
            {
                var time = new TimeSpan();
                decimal cost = 0m;

                foreach (var day in chargeRate)
                {
                    time = time.Add(day.TimeSpent);
                    cost += day.Cost;
                }

                var totalRate = cost.TruncateDecimal(1);
                total += totalRate;

                invoices.Add(new CongestionChargeInvoice
                    {
                        Value = string.Format("{0}{1}", CurrencyAppend, totalRate.ToString("0.00")),
                        Description = string.Format("Charge for {0}h {1}m ({2}):", time.Hours, time.Minutes, chargeRate.Key)
                    });
            }

            invoices.Add(CalculateTotal(total));
            return invoices;
        }

        private ICongestionChargeInvoice CalculateTotal(decimal total)
        {
            return new CongestionChargeInvoice()
                {
                    Description = "Total Charge:",
                    Value = string.Format("{0}{1}", CurrencyAppend, total.ToString("0.00"))      
                };
        }
    }
}
