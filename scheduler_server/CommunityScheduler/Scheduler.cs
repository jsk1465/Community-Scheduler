using System;
using System.Collections.Generic;
using System.Linq;
using CommunityScheduler.Interfaces;
using CommunityScheduler.Types;

namespace CommunityScheduler
{
	public class Scheduler : IScheduler
	{
		public DateTime StartTime       { get; set; } = DateTime.Parse ( "09:00" );
		public DateTime EndTime         { get; set; } = DateTime.Parse ( "17:00" );
		public TimeSpan MinimumInterval { get; set; } = TimeSpan.FromMinutes ( 30 );

		public SchedulerOutput Run ( IEnumerable<SchedulerInput> inputs )
		{
			var list  = inputs.ToList ( );
			var users = CreateUserInfo ( list );
			var week  = new Week ( list, users );

			ChooseSlots ( week );

			return CreateOutput ( week );
		}

		private static void ChooseSlots ( Week week )
		{
			foreach ( var (slot, i) in week.Slots.Select ( ( x, i ) => ( x, i ) ) )
			{
				if ( slot.IsFilled )
					continue;

				if ( slot.Available.Count == 0 )
					continue;

				var user = slot.Available
							   .OrderByDescending ( x => x.Priority )
							   .ThenByDescending ( x => x.FutureDistance[i] )
							   .First ( );
				slot.Chosen = user;
				--user.Priority;

				if ( user.IslandLength[i] > 1 )
				{
					week.Slots[i + 1].Chosen = user;
					--user.Priority;
				}
			}
		}

		private SchedulerOutput CreateOutput ( Week week )
		{
			var result = new SchedulerOutput
			{
				Schedule = new Dictionary<DayOfWeek, List<OutputTimeSlot>> ( )
			};
			foreach ( var day in week.Days )
				result.Schedule[day] = new List<OutputTimeSlot> ( );
			foreach ( var slot in week.Slots.Where ( x => x.IsFilled ) )
				result.Schedule[slot.Day].Add ( new OutputTimeSlot
					{
						Start = slot.StartTime,
						End   = slot.EndTime,
						User  = slot.Chosen.User
					}
				);

			return result;
		}

		private Dictionary<User, UserInfo> CreateUserInfo ( IEnumerable<SchedulerInput> inputs )
		{
			var users = new Dictionary<User, UserInfo> ( );
			foreach ( var input in inputs )
			{
				var info = new UserInfo ( input.User )
				{
					Gender   = input.Gender,
					MaxHours = input.MaxHours
				};
				info.Init ( StartTime, EndTime, MinimumInterval, 5, input.TimeSlots );
				users[info.User] = info;
			}

			return users;
		}
	}
}