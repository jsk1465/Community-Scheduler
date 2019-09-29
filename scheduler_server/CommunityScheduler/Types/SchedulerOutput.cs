using System;
using System.Collections.Generic;

namespace CommunityScheduler.Types
{
	public class SchedulerOutput
	{
		public Dictionary<DayOfWeek, List<OutputTimeSlot>> Schedule { get; set; }
	}
}