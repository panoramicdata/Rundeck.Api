using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class BulkJobExecutionToggleResponse : BulkActionResponse
	{
		[DataMember(Name = "enabled")]
		public bool Enabled { get; set; }
	}
}
