using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class JobImportResults
	{
		[JsonPropertyName("succeeded")]
		public List<JobImportResult> Succeeded { get; set; } = new List<JobImportResult>();

		[JsonPropertyName("failed")]
		public List<JobImportResult> Failed { get; set; } = new List<JobImportResult>();

		[JsonPropertyName("skipped")]
		public List<JobImportResult> Skipped { get; set; } = new List<JobImportResult>();
	}
}
