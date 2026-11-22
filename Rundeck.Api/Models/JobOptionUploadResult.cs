using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class JobOptionUploadResult
	{
		[JsonPropertyName("options")]
		public Dictionary<string, string> Options { get; set; } = new Dictionary<string, string>();

		[JsonPropertyName("total")]
		public int Total { get; set; }
	}
}