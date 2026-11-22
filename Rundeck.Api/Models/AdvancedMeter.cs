using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// AdvancedMeter
	/// </summary>
	public class AdvancedMeter : Meter
	{
		/// <summary>
		/// Max
		/// </summary>
		[JsonPropertyName("max")]
		public float Max { get; set; }

		/// <summary>
		/// Mean
		/// </summary>
		[JsonPropertyName("mean")]
		public float Mean { get; set; }

		/// <summary>
		/// Min
		/// </summary>
		[JsonPropertyName("min")]
		public float Min { get; set; }

		/// <summary>
		/// P50
		/// </summary>
		[JsonPropertyName("p50")]
		public float P50 { get; set; }

		/// <summary>
		/// P75
		/// </summary>
		[JsonPropertyName("p75")]
		public float P75 { get; set; }

		/// <summary>
		/// P95
		/// </summary>
		[JsonPropertyName("p95")]
		public float P95 { get; set; }

		/// <summary>
		/// P98
		/// </summary>
		[JsonPropertyName("p98")]
		public float P98 { get; set; }

		/// <summary>
		/// P99
		/// </summary>
		[JsonPropertyName("p99")]
		public float P99 { get; set; }

		/// <summary>
		/// P999
		/// </summary>
		[JsonPropertyName("p999")]
		public float P999 { get; set; }

		/// <summary>
		/// Standard deviation
		/// </summary>
		[JsonPropertyName("stddev")]
		public float StandardDeviation { get; set; }
	}
}
