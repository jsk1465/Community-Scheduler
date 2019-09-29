using System;
using System.Collections.Generic;
using System.Linq;
using CommunityScheduler.Extensions;
using CommunityScheduler.Types;

namespace CommunityScheduler
{
	internal class UserInfo : IEquatable<UserInfo>
	{
		public User      User           { get; set; }
		public List<int> Set            { get; set; }
		public double    Priority       { get; set; }
		public List<int> FutureDistance { get; set; }
		public List<int> IslandLength   { get; set; }
		public Gender    Gender         { get; set; }
		public TimeSpan  MaxHours       { get; set; }

		public UserInfo ( User user )
		{
			User = user;
		}

		public bool Equals ( UserInfo other )
		{
			if ( other is null ) return false;
			if ( ReferenceEquals ( this, other ) ) return true;
			return User.Equals ( other.User );
		}

		public static implicit operator User ( UserInfo info )
		{
			return info.User;
		}

		public void Init ( DateTime start,
						   DateTime end,
						   TimeSpan minSpan,
						   int numDays,
						   IDictionary<DayOfWeek, List<TimeSlot>> slotMap )
		{
			BuildSet ( start, end, minSpan, numDays, slotMap );

			BuildFutureDistance ( );
			BuildIslands ( );
		}

		private void BuildSet ( DateTime start,
								DateTime end,
								TimeSpan minSpan,
								int numDays,
								IDictionary<DayOfWeek, List<TimeSlot>> slotMap )
		{
			var slotsCount = start.CountIntegralIntervals ( end, minSpan );
			Set = Enumerable.Repeat ( 0, slotsCount * numDays ).ToList ( );

			foreach ( var pair in slotMap )
			{
				var skip = ( (int) pair.Key - (int) DayOfWeek.Monday ) * slotsCount;
				foreach ( var slot in pair.Value.OrderBy ( x => x.Start ) )
				{
					var size       = slot.Start.CountIntegralIntervals ( slot.End, minSpan );
					var startIndex = skip + start.CountIntegralIntervals ( slot.Start, minSpan );
					for ( var i = startIndex; i < startIndex + size; i++ )
						Set[i] = 1;
				}
			}
		}

		private void BuildFutureDistance ( )
		{
			FutureDistance = Enumerable.Repeat ( 0, Set.Count ).ToList ( );
			var curDist = Set.Count;
			for ( var i = Set.Count - 1; i >= 0; i-- )
			{
				FutureDistance[i] = curDist;
				if ( Set[i] == 1 )
					curDist = 0;
				else
					++curDist;
			}
		}

		private void BuildIslands ( )
		{
			IslandLength = Enumerable.Repeat ( 0, Set.Count ).ToList ( );
			var curDist = 0;
			for ( var i = Set.Count - 1; i >= 0; i-- )
				if ( Set[i] == 0 )
					curDist = 0;
				else
					IslandLength[i] = ++curDist;
		}

		public override string ToString ( )
		{
			return $"{User}@{Priority}";
		}

		public override bool Equals ( object obj )
		{
			if ( obj is null ) return false;
			if ( ReferenceEquals ( this, obj ) ) return true;
			if ( obj is UserInfo info ) return Equals ( info );
			return false;
		}

		public override int GetHashCode ( )
		{
			return User.GetHashCode ( );
		}
	}
}