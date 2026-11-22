using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	public class WebHook : WebHookBase
	{
		[JsonPropertyName("authToken")]
		public string AuthToken { get; set; } = default!;

		[JsonPropertyName("creator")]
		public string Creator { get; set; } = default!;

		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("uuid")]
		public string Uuid { get; set; } = default!;

		/// <summary>
		/// Whether callers must send the auth token in the Authorization header
		/// </summary>
		[JsonPropertyName("useAuth")]
		public bool? UseAuth { get; set; }

		/// <summary>
		/// Whether to generate a new Authorization header secret
		/// </summary>
		[JsonPropertyName("regenAuth")]
		public bool? RegenAuth { get; set; }
	}
}
