using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class WebHookConfig
	{
		[DataMember(Name = "argString")]
		public string? ArgString { get; set; }

		[DataMember(Name = "jobId")]
		public string JobId { get; set; } = default!;
	}
}