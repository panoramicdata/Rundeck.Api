using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class Resource
	{
		[JsonPropertyName("href")]
		public string Href { get; set; } = default!;

		[JsonPropertyName("description")]
		public string? Description { get; set; }

		[JsonPropertyName("writeable")]
		public bool Writeable { get; set; }

		[JsonPropertyName("empty")]
		public bool? Empty { get; set; }
	}
}
