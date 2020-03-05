using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class Resource
	{
		[DataMember(Name = "href")]
		public string Href { get; set; } = default!;

		[DataMember(Name = "description")]
		public string? Description { get; set; }

		[DataMember(Name = "writeable")]
		public bool Writeable { get; set; }

		[DataMember(Name = "empty")]
		public bool? Empty { get; set; }
	}
}
