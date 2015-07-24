using System;
using System.Collections.Generic;
using CongestionCharge.Business;
using CongestionCharge.Business.Model;
using Ninject.Modules;

namespace CongestionCharge.ConsoleDemo
{
    public class IoCBinding : NinjectModule
    {
        public override void Load()
        {
            Bind<ICongestionRateStore>().To<CongestionRateStore>().InSingletonScope().WithConstructorArgument("congestionRates", LoadRates());
            Bind<IInvoiceBuilder>().To<InvoiceBuilder>().InSingletonScope();
            Bind<ICongestionChargeCalculator>().To<CongestionChargeCalculator>().InSingletonScope();
        }

        private List<ICongestionRate> LoadRates()
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
