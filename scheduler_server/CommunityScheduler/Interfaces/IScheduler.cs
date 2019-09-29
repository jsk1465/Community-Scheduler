using System.Collections.Generic;
using CommunityScheduler.Types;

namespace CommunityScheduler.Interfaces
{
	public interface IScheduler
	{
		SchedulerOutput Run ( IEnumerable<SchedulerInput> inputs );
	}
}