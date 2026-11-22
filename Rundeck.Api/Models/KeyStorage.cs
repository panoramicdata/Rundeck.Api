using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	[SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "<Pending>")]
	public class KeyStorage
	{
		[JsonPropertyName("resources")]
		public List<KeyResource> Resources { get; set; } = new List<KeyResource>();

		[JsonPropertyName("url")]
		public string Url { get; set; } = default!;

		[JsonPropertyName("type")]
		public KeyResourceType Type { get; set; } = default!;

		[JsonPropertyName("path")]
		public string Path { get; set; } = default!;

		[JsonPropertyName("name")]
		public string Name { get; set; } = default!;

		[JsonPropertyName("meta")]
		public KeyMeta Meta { get; set; } = new KeyMeta();
	}
}
