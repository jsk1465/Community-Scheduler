using System;

namespace CommunityScheduler.Interfaces
{
	public interface ITimeSlot : IEquatable<ITimeSlot>
	{
		DateTime Start    { get; set; }
		DateTime End      { get; set; }
		TimeSpan Duration { get; }
	}
}