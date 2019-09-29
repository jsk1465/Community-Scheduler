using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CommunityScheduler
{
	internal class SlotInfo
	{
		public DayOfWeek      Day       { get; set; }
		public DateTime       StartTime { get; set; }
		public DateTime       EndTime   { get; set; }
		public UserInfo       Chosen    { get; set; }
		public List<UserInfo> Available { get; set; } = new List<UserInfo> ( );

		[JsonIgnore]
		public bool IsFilled => Chosen != null;

		public SlotInfo ( DayOfWeek day,
						  DateTime startTime,
						  DateTime endTime )
		{
			Day       = day;
			StartTime = startTime;
			EndTime   = endTime;
		}

		public override string ToString ( )
		{
			return $"{Chosen} / {Available.Count}";
		}
	}
}