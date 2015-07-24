using System;

namespace CongestionCharge.Business.Library
{
    public static class EnumHelper
    {
        public static T ToEnum<T>(this string stringValue)
        {
            const bool ignoreCase = false;
            return ToEnum<T>(stringValue, ignoreCase);
        }

        public static T ToEnum<T>(this string stringValue, bool ignoreCase)
        {
            return typeof(T).IsEnum ? (T)Enum.Parse(typeof(T), stringValue, ignoreCase) : default(T);
        }
    }
}
