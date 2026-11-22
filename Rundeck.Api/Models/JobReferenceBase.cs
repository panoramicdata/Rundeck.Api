using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// The identifying details shared by a job and the result of importing one
	/// </summary>
	public abstract class JobReferenceBase
	{
		[JsonPropertyName("id")]
		public string Id { get; set; } = default!;

		[JsonPropertyName("name")]
		public string Name { get; set; } = default!;

		[JsonPropertyName("group")]
		public string Group { get; set; } = default!;

		[JsonPropertyName("project")]
		public string Project { get; set; } = default!;

		[JsonPropertyName("href")]
		public string Href { get; set; } = default!;

		[JsonPropertyName("permalink")]
		public string Permalink { get; set; } = default!;
	}
}
