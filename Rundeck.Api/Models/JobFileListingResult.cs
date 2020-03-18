using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class JobFileListingResult
	{
		[DataMember(Name = "paging")]
		public Paging Paging { get; set; } = new Paging();

		[DataMember(Name = "files")]
		public IList<JobOptionFile> Files { get; set; }
	}
}
