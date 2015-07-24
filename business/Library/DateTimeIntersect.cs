using System;
using CongestionCharge.Business.Model;

namespace CongestionCharge.Business.Library
{
	public static class DateTimeIntersect
	{
       

		public static DateRange FindIntersect(DateRange t1, DateRange t2)
		{

			var iRange = new DateRange
				             {
					             Start = t1.Start < t2.Start ? t2.Start : t1.Start,
					             End = t1.End < t2.End ? t1.End : t2.End
				             };

			return iRange.Start >= iRange.End ? null : iRange;
		}
	}
}
