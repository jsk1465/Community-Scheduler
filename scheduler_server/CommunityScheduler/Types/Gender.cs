using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CommunityScheduler
{
	[JsonConverter ( typeof ( StringEnumConverter ) )]
	public enum Gender
	{
		Male,
		Female
	}
}