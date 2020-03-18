using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class Workflow
	{
		[DataMember(Name = "exec")]
		public string Exec { get; set; } = default!;

		[DataMember(Name = "description")]
		public string Description { get; set; } = default!;
	}
}
