using System.Collections.Generic;
using CommunityScheduler.Types;
using Microsoft.AspNetCore.Mvc;

namespace CommunityScheduler.WebApi.Controllers
{
	[Route ( "api/[controller]" )]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		[HttpPost ( "{inputs}" )]
		[Route ( "/schedule" )]
		public ActionResult<SchedulerOutput> Schedule ( [FromBody] List<SchedulerInput> inputs )
		{
			var scheduler = new Scheduler ( );
			var output    = scheduler.Run ( inputs );

			return output;
		}
	}
}