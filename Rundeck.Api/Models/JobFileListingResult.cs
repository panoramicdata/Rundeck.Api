using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class JobFileListingResult
	{
		[JsonPropertyName("paging")]
		public Paging Paging { get; set; } = new Paging();

		[JsonPropertyName("files")]
		public IList<JobOptionFile> Files { get; set; } = new List<JobOptionFile>();
	}
}
