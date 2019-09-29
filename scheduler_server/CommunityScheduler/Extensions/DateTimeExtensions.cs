using System;

namespace CommunityScheduler.Extensions
{
	internal static class DateTimeExtensions
	{
		public static double CountIntervals ( this DateTime start, DateTime end, TimeSpan interval )
		{
			var duration = end - start;
			return duration.TotalSeconds / interval.TotalSeconds;
		}

		public static int CountIntegralIntervals ( this DateTime start, DateTime end, TimeSpan interval )
		{
			var intervals = CountIntervals ( start, end, interval );
			return (int) Math.Floor ( intervals );
		}
	}
}