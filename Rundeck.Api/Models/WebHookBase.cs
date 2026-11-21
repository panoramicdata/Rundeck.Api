using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// The settings shared by a webhook and the request that creates one
	/// </summary>
	public abstract class WebHookBase
	{
		[JsonPropertyName("config")]
		public WebHookConfig Config { get; set; } = new WebHookConfig();

		[JsonPropertyName("enabled")]
		public bool Enabled { get; set; }

		[JsonPropertyName("eventPlugin")]
		public string EventPlugin { get; set; } = default!;

		[JsonPropertyName("name")]
		public string Name { get; set; } = default!;

		[JsonPropertyName("project")]
		public string Project { get; set; } = default!;

		[JsonPropertyName("roles")]
		public string Roles { get; set; } = default!;

		[JsonPropertyName("user")]
		public string User { get; set; } = default!;
	}
}
