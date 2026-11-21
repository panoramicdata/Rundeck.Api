using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class BulkActionResult
	{
		[JsonPropertyName("requestCount")]
		public int RequestCount { get; set; }

		[JsonPropertyName("successCount")]
		public int SuccessCount { get; set; }

		[JsonPropertyName("failedCount")]
		public int FailedCount { get; set; }

		[JsonPropertyName("allsuccessful")]
		public bool Allsuccessful { get; set; }

		[JsonPropertyName("enabled")]
		public bool? Enabled { get; set; }

		[JsonPropertyName("succeeded")]
		public IList<ActionResult> Succeeded { get; set; } = new List<ActionResult>();

		[JsonPropertyName("failed")]
		public IList<ActionResult> Failed { get; set; } = new List<ActionResult>();
	}
}
