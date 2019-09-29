using System;
using Newtonsoft.Json;

namespace CommunityScheduler.Types
{
	public class OutputTimeSlot : TimeSlot, IEquatable<OutputTimeSlot>
	{
		[JsonProperty ( "user" )]
		public User User { get; set; }

		public bool Equals ( OutputTimeSlot other )
		{
			if ( other is null )
				return false;
			return User.Equals ( other.User ) && Start.Equals ( other.Start ) && End.Equals ( other.End );
		}

		public override bool Equals ( object obj )
		{
			if ( obj is null ) return false;
			if ( ReferenceEquals ( this, obj ) ) return true;
			if ( obj is OutputTimeSlot slot ) return Equals ( slot );
			return false;
		}

		public override int GetHashCode ( )
		{
			return 0;
		}

		public override string ToString ( )
		{
			return $"{User} {Start:t} => {End:t}";
		}
	}
}