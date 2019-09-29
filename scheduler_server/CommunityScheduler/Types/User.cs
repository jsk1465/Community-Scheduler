using System;
using Newtonsoft.Json;

namespace CommunityScheduler.Types
{
	public class User : IEquatable<User>
	{
		[JsonProperty ( "id" )]
		public string Id { get; set; }

		public bool Equals ( User other )
		{
			if ( other is null ) return false;
			return Id == other.Id;
		}

		public override string ToString ( )
		{
			return $"{Id}";
		}

		public override bool Equals ( object obj )
		{
			if ( obj is null ) return false;
			if ( ReferenceEquals ( this, obj ) ) return true;
			if ( obj is User user ) return Equals ( user );
			return false;
		}

		public override int GetHashCode ( )
		{
			return 0;
		}
	}
}