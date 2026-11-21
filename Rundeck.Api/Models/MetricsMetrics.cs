using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Metrics: the metrics
	/// </summary>
	public class MetricsMetrics
	{
		/// <summary>
		/// Version
		/// </summary>
		[JsonPropertyName("version")]
		public string Version { get; set; } = default!;

		/// <summary>
		/// Gauges
		/// </summary>
		[JsonPropertyName("gauges")]
		public Gauges Gauges { get; set; } = default!;

		/// <summary>
		/// Counters
		/// </summary>
		[JsonPropertyName("counters")]
		public Counters Counters { get; set; } = default!;

		/// <summary>
		/// Histograms
		/// </summary>
		[JsonPropertyName("histograms")]
		public Histograms Histograms { get; set; } = default!;

		/// <summary>
		/// Meters
		/// </summary>
		[JsonPropertyName("meters")]
		public Meters Meters { get; set; } = default!;

		/// <summary>
		/// Timers
		/// </summary>
		[JsonPropertyName("timers")]
		public MetricsTimers Timers { get; set; } = default!;
	}
}
