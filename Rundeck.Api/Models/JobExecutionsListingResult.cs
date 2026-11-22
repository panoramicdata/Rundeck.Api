using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class JobExecutionsListingResult
	{
		[JsonPropertyName("paging")]
		public Paging Paging { get; set; } = new Paging();

		[JsonPropertyName("executions")]
		public IList<Execution> Executions { get; set; } = new List<Execution>();
	}
}
