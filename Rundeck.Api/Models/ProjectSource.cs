using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class ProjectSource
	{
		[DataMember(Name = "index")]
		public int Index { get; set; }

		[DataMember(Name = "type")]
		public string Type { get; set; } = default!;

		[DataMember(Name = "resources")]
		public Resource Resource { get; set; } = new Resource();
	}
}