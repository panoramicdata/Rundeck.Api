using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class WebHookConfig
	{
		[JsonPropertyName("argString")]
		public string? ArgString { get; set; }

		[JsonPropertyName("jobId")]
		public string JobId { get; set; } = default!;
	}
}