using System;
using CommunityScheduler.Interfaces;
using Newtonsoft.Json;

namespace CommunityScheduler.Types
{
	public class TimeSlot : ITimeSlot
	{
		[JsonProperty ( "start" )]
		public DateTime Start { get; set; }

		[JsonProperty ( "end" )]
		public DateTime End { get; set; }

		[JsonIgnore]
		public TimeSpan Duration => End - Start;

		public bool Equals ( ITimeSlot other )
		{
			if ( other is null )
				return false;
			return Start.Equals ( other.Start ) && End.Equals ( other.End );
		}

		public override bool Equals ( object obj )
		{
			if ( obj is null ) return false;
			if ( ReferenceEquals ( this, obj ) ) return true;
			if ( obj is ITimeSlot slot ) return Equals ( slot );
			return false;
		}

		public override int GetHashCode ( )
		{
			return 0;
		}

		public override string ToString ( )
		{
			return $"{Start:t} => {End:t}";
		}
	}
}