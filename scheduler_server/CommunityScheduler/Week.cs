using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CommunityScheduler.Types;
using Newtonsoft.Json;

namespace CommunityScheduler
{
	internal class Week
	{
		private Dictionary<DayOfWeek, List<SlotInfo>> _slotMap;
		public  List<SchedulerInput>                  Inputs { get; set; }
		public  IDictionary<User, UserInfo>           Users  { get; set; }

		public ImmutableList<DayOfWeek> Days { get; set; } = new[]
		{
			DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
			DayOfWeek.Thursday, DayOfWeek.Friday
		}.ToImmutableList ( );

		public DateTime StartTime { get; set; } = DateTime.Parse ( "09:00" );
		public DateTime EndTime   { get; set; } = DateTime.Parse ( "17:00" );
		public TimeSpan TimeUnit  { get; set; } = TimeSpan.FromMinutes ( 30 );

		[JsonIgnore]
		public List<SlotInfo> Slots { get; private set; }

		[JsonConstructor]
		private Week ( )
		{
			CreateSlots ( );
			UpdateSlots ( );
		}

		public Week ( IEnumerable<SchedulerInput> inputs,
					  IDictionary<User, UserInfo> users )
		{
			Inputs = inputs.ToList ( );
			Users  = users;

			CreateSlots ( );
			UpdateSlots ( );
		}

		private void CreateSlots ( )
		{
			var duration   = EndTime - StartTime;
			var slotsCount = (int) Math.Round ( duration.TotalSeconds / TimeUnit.TotalSeconds );
			Slots    = new List<SlotInfo> ( slotsCount );
			_slotMap = new Dictionary<DayOfWeek, List<SlotInfo>> ( );

			foreach ( var day in Days )
			{
				var start = StartTime;
				_slotMap[day] = new List<SlotInfo> ( );
				for ( var i = 0; i < slotsCount; i++ )
				{
					var slot = new SlotInfo ( day, start, start + TimeUnit );
					Slots.Add ( slot );
					_slotMap[day].Add ( slot );
					start += TimeUnit;
				}
			}
		}

		private void UpdateSlots ( )
		{
			foreach ( var input in Inputs )
			foreach ( var pair in input.TimeSlots )
			foreach ( var slot in pair.Value )
			foreach ( var minimalSlot in _slotMap[pair.Key]
				.Where ( minimalSlot => slot.Start <= minimalSlot.StartTime && minimalSlot.EndTime <= slot.End ) )
				minimalSlot.Available.Add ( Users[input.User] );
		}

		public double GetHeuristic ( )
		{
			var result = 0d;
			var counts = new Dictionary<User, int> ( );
			foreach ( var slot in Slots.Where ( x => x.Chosen != null ) )
			{
				if ( counts.ContainsKey ( slot.Chosen.User ) )
					counts[slot.Chosen.User]++;
				else counts[slot.Chosen.User] = 1;
				result += 1d / counts[slot.Chosen.User];
			}

			return result / Slots.Count;
		}
	}
}