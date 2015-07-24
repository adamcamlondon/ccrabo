using System;
using CongestionCharge.Business.Model;

namespace CongestionCharge.ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            var application = new ApplicationTest();

            Console.WriteLine("Scenario: Car: 24/04/2008 11:32 - 24/04/2008 14:42");
            var firstScenario = application.CalculateInvoice(new DateTime(2008, 04, 24, 11, 32, 0),
                                                            new DateTime(2008, 04, 24, 14, 42, 0), VehicleType.Car);

            Console.WriteLine("Results:");
            foreach (var congestionChargeInvoice in firstScenario)
                Console.WriteLine(string.Format("{0}  {1}", congestionChargeInvoice.Description, congestionChargeInvoice.Value));

           Console.WriteLine("");
           Console.WriteLine("Scenario: Motorbike: 24/04/2008 17:00 - 24/04/2008 22:11");
           var secondScenario = application.CalculateInvoice(new DateTime(2008, 04, 24, 17, 00, 0),
                                                           new DateTime(2008, 04, 24, 22, 11, 0), VehicleType.Motorcycle);
           Console.WriteLine("Results:");
           foreach (var congestionChargeInvoice in secondScenario)
               Console.WriteLine(string.Format("{0}  {1}", congestionChargeInvoice.Description, congestionChargeInvoice.Value));


           Console.WriteLine("");
           Console.WriteLine("Scenario: Van: 25/04/2008 10:23 - 28/04/2008 09:02");
           var thirdScenario = application.CalculateInvoice(new DateTime(2008, 04, 25, 10, 23, 0),
                                                           new DateTime(2008, 04, 28, 09, 02, 0), VehicleType.Van);
           Console.WriteLine("Results:");
           foreach (var congestionChargeInvoice in thirdScenario)
               Console.WriteLine(string.Format("{0}  {1}", congestionChargeInvoice.Description, congestionChargeInvoice.Value));


            Console.ReadKey();
        }
    }
}
