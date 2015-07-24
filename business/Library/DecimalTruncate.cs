using System;

namespace CongestionCharge.Business.Library
{
    public static class DecimalTruncate
    {
        public static decimal TruncateDecimal(this decimal value, int decimalPlaces)
        {
            decimal integralValue = Math.Truncate(value);
            decimal fraction = value - integralValue;
            decimal factor = (decimal)Math.Pow(10, decimalPlaces);
            decimal truncatedFraction = Math.Truncate(fraction * factor) / factor;
            decimal result = integralValue + truncatedFraction;
            return result;
        }
    }
}
