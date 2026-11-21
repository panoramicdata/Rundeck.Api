using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A health status
	/// </summary>
	public class Health
	{
		/// <summary>
		/// Whether the health is healthy
		/// </summary>
		[JsonPropertyName("healthy")]
		public bool Healthy { get; set; }

		/// <summary>
		/// The message
		/// </summary>
		[JsonPropertyName("message")]
		public string Message { get; set; } = string.Empty;
	}
}
