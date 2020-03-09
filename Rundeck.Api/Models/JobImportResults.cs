using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class JobImportResults
	{
		[DataMember(Name = "succeeded")]
		public List<JobImportResult> Succeeded { get; set; } = new List<JobImportResult>();

		[DataMember(Name = "failed")]
		public List<JobImportResult> Failed { get; set; } = new List<JobImportResult>();

		[DataMember(Name = "skipped")]
		public List<JobImportResult> Skipped { get; set; } = new List<JobImportResult>();
	}
}
