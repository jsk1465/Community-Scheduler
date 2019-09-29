using System;
using System.Collections.Generic;
using System.Linq;
using CommunityScheduler.Types;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CommunityScheduler.UnitTests
{
	public class Tests
	{
		[SetUp]
		public void Setup ( )
		{
			_random      = new Random ( 42 );
			_minimumSpan = TimeSpan.FromMinutes ( 30 );
		}

		[Test]
		public void SingleUserShouldGetAllSlots ( )
		{
			var scheduler = new Scheduler ( );
			var days = new[]
			{
				DayOfWeek.Monday,
				DayOfWeek.Tuesday,
				DayOfWeek.Wednesday,
				DayOfWeek.Thursday,
				DayOfWeek.Friday
			};
			var inputs = CreateRandomInputs ( 1, "u", days );
			var output = scheduler.Run ( inputs );

			Console.WriteLine ( JsonConvert.SerializeObject ( inputs, Formatting.Indented ) );

			Assert.IsNotNull ( output );
			Assert.IsNotNull ( output.Schedule );

			foreach ( var day in days )
			{
				Assert.IsTrue ( output.Schedule.TryGetValue ( day, out var slots ) );
				Assert.AreEqual ( slots.Aggregate ( TimeSpan.Zero, ( current, x ) => current + x.Duration ),
								  inputs[0].TimeSlots[day]
										   .Aggregate ( TimeSpan.Zero, ( current, x ) => current + x.Duration ) );
			}
		}

		[Test]
		public void WorksForMultipleUsers ( )
		{
			var scheduler = new Scheduler ( );
			var days = new[]
			{
				DayOfWeek.Monday,
				DayOfWeek.Tuesday,
				DayOfWeek.Wednesday,
				DayOfWeek.Thursday,
				DayOfWeek.Friday
			};
			var inputs = CreateRandomInputs ( 40, "u", days );
			var output = scheduler.Run ( inputs );

			Assert.IsNotNull ( output );
			Assert.IsNotNull ( output.Schedule );

			foreach ( var day in days ) Assert.IsTrue ( output.Schedule.TryGetValue ( day, out _ ) );
		}

#region Helpers

		private Random   _random;
		private TimeSpan _minimumSpan;

		private TimeSpan GetRandomDuration ( TimeSpan minDuration,
											 TimeSpan maxDuration )
		{
			var spanCount   = ( maxDuration - minDuration ) / _minimumSpan;
			var chosenCount = Math.Floor ( 2d + ( spanCount - 2d ) * _random.NextDouble ( ) );

			return _minimumSpan * chosenCount;
		}

		private TimeSlot GetRandomTimeSlot ( DateTime startTime,
											 DateTime endTime,
											 TimeSpan duration )
		{
			var spanCount               = ( endTime - startTime - duration ) / _minimumSpan;
			var chosenStartSpanMultiple = Math.Floor ( spanCount * _random.NextDouble ( ) );
			var chosenStart             = startTime + _minimumSpan * chosenStartSpanMultiple;

			return new TimeSlot {Start = chosenStart, End = chosenStart + duration};
		}

		private TimeSlot GetRandomTimeSlot ( DateTime startTime,
											 DateTime endTime,
											 TimeSpan minDuration,
											 TimeSpan maxDuration )
		{
			var duration = GetRandomDuration ( minDuration, maxDuration );
			return GetRandomTimeSlot ( startTime, endTime, duration );
		}

		private List<TimeSlot> GetRandomTimeSlots ( DateTime startTime,
													DateTime endTime,
													TimeSpan minDuration,
													TimeSpan maxDuration )
		{
			var result   = new List<TimeSlot> ( );
			var curStart = startTime;
			while ( curStart < endTime )
			{
				if ( _random.NextDouble ( ) < 0.5 )
				{
					curStart += _minimumSpan;
					continue;
				}

				var duration = GetRandomDuration ( minDuration, maxDuration );
				var slot = new TimeSlot
				{
					Start = curStart,
					End   = curStart + duration
				};
				if ( slot.End > endTime )
					slot.End = endTime;
				curStart = slot.End + _minimumSpan;
				result.Add ( slot );
			}

			return result;
		}

		private SchedulerInput CreateRandomInput ( string id,
												   IEnumerable<DayOfWeek> days,
												   TimeSpan maxHours,
												   DateTime startTime,
												   DateTime endTime,
												   TimeSpan minDuration,
												   TimeSpan maxDuration )
		{
			var input = new SchedulerInput
			{
				User      = new User {Id = id},
				Gender    = Gender.Male,
				MaxHours  = maxHours,
				TimeSlots = new Dictionary<DayOfWeek, List<TimeSlot>> ( )
			};

			foreach ( var day in days )
				input.TimeSlots[day] = GetRandomTimeSlots ( startTime, endTime, minDuration, maxDuration );

			return input;
		}

		private SchedulerInput CreateRandomInput ( string id,
												   params DayOfWeek[] days )
		{
			return CreateRandomInput ( id, days, TimeSpan.FromHours ( 5 ),
									   DateTime.Parse ( "09:00" ), DateTime.Parse ( "17:00" ),
									   TimeSpan.FromMinutes ( 30 ), TimeSpan.FromMinutes ( 120 ) );
		}

		private List<SchedulerInput> CreateRandomInputs ( int count,
														  string idPrefix,
														  params DayOfWeek[] days )
		{
			var inputs = new List<SchedulerInput> ( count );
			for ( var i = 0; i < count; i++ )
				inputs.Add ( CreateRandomInput ( $"{idPrefix}{i + 1}", days ) );

			return inputs;
		}

#endregion
	}
}