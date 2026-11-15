using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class JobImportResult : JobReferenceBase
	{
		[DataMember(Name = "index")]
		public int Index { get; set; }

		[DataMember(Name = "error")]
		public string? Error { get; set; }
	}
}
