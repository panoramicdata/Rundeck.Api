using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{

	/// <summary>
	/// Metrics: the metrics
	/// </summary>
	[DataContract]
	public class MetricsMetrics
	{
		/// <summary>
		/// Version
		/// </summary>
		[DataMember(Name = "version")]
		public string Version { get; set; } = default!;

		/// <summary>
		/// Gauges
		/// </summary>
		[DataMember(Name = "gauges")]
		public Gauges Gauges { get; set; } = default!;

		/// <summary>
		/// Counters
		/// </summary>
		[DataMember(Name = "counters")]
		public Counters Counters { get; set; } = default!;

		/// <summary>
		/// Histograms
		/// </summary>
		[DataMember(Name = "histograms")]
		public Histograms Histograms { get; set; } = default!;

		/// <summary>
		/// Meters
		/// </summary>
		[DataMember(Name = "meters")]
		public Meters Meters { get; set; } = default!;

		/// <summary>
		/// Timers
		/// </summary>
		[DataMember(Name = "timers")]
		public Timers Timers { get; set; } = default!;
	}

}
