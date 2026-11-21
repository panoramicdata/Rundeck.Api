using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class JobImportResult : JobReferenceBase
	{
		[JsonPropertyName("index")]
		public int Index { get; set; }

		[JsonPropertyName("error")]
		public string? Error { get; set; }
	}
}
