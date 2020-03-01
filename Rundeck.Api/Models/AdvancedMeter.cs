using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{

	/// <summary>
	/// AdvancedMeter
	/// </summary>
	[DataContract]
	public class AdvancedMeter : Meter
	{

		/// <summary>
		/// Max
		/// </summary>
		[DataMember(Name = "max")]
		public float Max { get; set; }

		/// <summary>
		/// Mean
		/// </summary>
		[DataMember(Name = "mean")]
		public float Mean { get; set; }

		/// <summary>
		/// Min
		/// </summary>
		[DataMember(Name = "min")]
		public float Min { get; set; }

		/// <summary>
		/// P50
		/// </summary>
		[DataMember(Name = "p50")]
		public float P50 { get; set; }

		/// <summary>
		/// P75
		/// </summary>
		[DataMember(Name = "p75")]
		public float P75 { get; set; }

		/// <summary>
		/// P95
		/// </summary>
		[DataMember(Name = "p95")]
		public float P95 { get; set; }

		/// <summary>
		/// P98
		/// </summary>
		[DataMember(Name = "p98")]
		public float P98 { get; set; }

		/// <summary>
		/// P99
		/// </summary>
		[DataMember(Name = "p99")]
		public float P99 { get; set; }

		/// <summary>
		/// P999
		/// </summary>
		[DataMember(Name = "p999")]
		public float P999 { get; set; }

		/// <summary>
		/// Standard deviation
		/// </summary>
		[DataMember(Name = "stddev")]
		public float StandardDeviation { get; set; }
	}
}
