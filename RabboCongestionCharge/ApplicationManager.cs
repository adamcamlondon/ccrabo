using System;
using System.Collections.Generic;
using CongestionCharge.Business;
using CongestionCharge.Business.Model;
using Ninject;

namespace CongestionCharge.ConsoleDemo
{
    public class ApplicationTest
    {
        private  static IKernel _ioCKernel;
		private static IoCBinding _binding;
        private readonly ICongestionChargeCalculator _congestionChargeCalculator;

        public ApplicationTest()
        {
            CreateIoCBindings();
            _congestionChargeCalculator = _ioCKernel.Get<ICongestionChargeCalculator>();
        }

        private static void CreateIoCBindings()
		{
			_binding = new IoCBinding();
			_ioCKernel = new StandardKernel(_binding);
		}

        public List<ICongestionChargeInvoice> CalculateInvoice(DateTime start, DateTime end, VehicleType vehicleType)
        {
            return _congestionChargeCalculator.CalculateCost(start, end, vehicleType);
        }
    }
}
