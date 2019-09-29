using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CommunityScheduler.Types
{
	public class SchedulerInput
	{
		[JsonProperty ( "user" )]
		public User User { get; set; }

		[JsonProperty ( "gender" )]
		public Gender Gender { get; set; }

		[JsonProperty ( "maxhours" )]
		public TimeSpan MaxHours { get; set; }

		[JsonProperty ( "timeslots" )]
		public Dictionary<DayOfWeek, List<TimeSlot>> TimeSlots { get; set; }
	}
}