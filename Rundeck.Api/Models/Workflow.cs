using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class Workflow
	{
		[JsonPropertyName("exec")]
		public string Exec { get; set; } = default!;

		[JsonPropertyName("description")]
		public string Description { get; set; } = default!;
	}
}
