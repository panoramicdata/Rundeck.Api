using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	[DataContract]
	public class JobExecutionsListingResult
	{
		[DataMember(Name = "paging")]
		public Paging Paging { get; set; } = new Paging();

		[DataMember(Name = "executions")]
		public IList<Execution> Executions { get; set; } = new List<Execution>();
	}
}
