using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Meter
	/// </summary>
	public class Meter
	{
		/// <summary>
		/// Count
		/// </summary>
		[JsonPropertyName("count")]
		public int Count { get; set; }

		/// <summary>
		/// M15Rate
		/// </summary>
		[JsonPropertyName("m15_rate")]
		public float M15Rate { get; set; }

		/// <summary>
		/// M1Rate
		/// </summary>
		[JsonPropertyName("m1_rate")]
		public float M1Rate { get; set; }

		/// <summary>
		/// M5Rate
		/// </summary>
		[JsonPropertyName("m5_rate")]
		public float M5Rate { get; set; }

		/// <summary>
		/// MeanRate
		/// </summary>
		[JsonPropertyName("mean_rate")]
		public float MeanRate { get; set; }

		/// <summary>
		/// Units
		/// </summary>
		[JsonPropertyName("units")]
		public string Units { get; set; } = default!;

		/// <summary>
		/// DurationUnits
		/// </summary>
		[JsonPropertyName("duration_units")]
		public string DurationUnits { get; set; } = default!;

		/// <summary>
		/// RateUnits
		/// </summary>
		[JsonPropertyName("rate_units")]
		public string RateUnits { get; set; } = default!;
	}
}
