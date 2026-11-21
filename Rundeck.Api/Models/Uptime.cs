using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Uptime
	/// </summary>
	public class Uptime
	{
		/// <summary>
		/// Duration
		/// </summary>
		[JsonPropertyName("duration")]
		public int Duration { get; set; }

		/// <summary>
		/// Unit
		/// </summary>
		[JsonPropertyName("unit")]
		public string Unit { get; set; } = default!;

		/// <summary>
		/// Uptime since in SystemTimestamp
		/// </summary>
		[JsonPropertyName("since")]
		public SystemTimestamp Since { get; set; } = new SystemTimestamp();
	}
}