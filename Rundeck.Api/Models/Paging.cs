using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class Paging
	{
		[DataMember(Name = "offset")]
		public int Offset { get; set; }

		[DataMember(Name = "max")]
		public int Max { get; set; }

		[DataMember(Name = "total")]
		public int Total { get; set; }

		[DataMember(Name = "count")]
		public int Count { get; set; }
	}
}