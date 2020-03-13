using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class JobBulkDeleteResults
	{
		[DataMember(Name = "requestCount")]
		public int RequestCount { get; set; }

		[DataMember(Name = "allsuccessful")]
		public bool Allsuccessful { get; set; }

		[DataMember(Name = "succeeded")]
		public IList<JobDeletionResult> Succeeded { get; set; } = new List<JobDeletionResult>();

		[DataMember(Name = "failed")]
		public IList<JobDeletionResult> Failed { get; set; } = new List<JobDeletionResult>();

	}
}
