using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class ProjectSource
	{
		[JsonPropertyName("index")]
		public int Index { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; } = default!;

		[JsonPropertyName("resources")]
		public Resource Resource { get; set; } = new Resource();
	}
}