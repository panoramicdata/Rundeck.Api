using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// The links
	/// </summary>
	public class MetricLinks
	{
		/// <summary>
		/// The metrics
		/// </summary>
		[JsonPropertyName("metrics")]
		public Link Metrics { get; set; } = default!;

		/// <summary>
		/// The ping
		/// </summary>
		[JsonPropertyName("ping")]
		public Link Ping { get; set; } = default!;

		/// <summary>
		/// The threads
		/// </summary>
		[JsonPropertyName("threads")]
		public Link Threads { get; set; } = default!;

		/// <summary>
		/// The healthcheck
		/// </summary>
		[JsonPropertyName("healthcheck")]
		public Link Healthcheck { get; set; } = default!;
	}
}
