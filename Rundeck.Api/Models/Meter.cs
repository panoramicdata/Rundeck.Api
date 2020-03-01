using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Meter
	/// </summary>
	[DataContract]
	public class Meter
	{
		/// <summary>
		/// Count
		/// </summary>
		[DataMember(Name = "count")]
		public int Count { get; set; }

		/// <summary>
		/// M15Rate
		/// </summary>
		[DataMember(Name = "m15_rate")]
		public float M15Rate { get; set; }

		/// <summary>
		/// M1Rate
		/// </summary>
		[DataMember(Name = "m1_rate")]
		public float M1Rate { get; set; }

		/// <summary>
		/// M5Rate
		/// </summary>
		[DataMember(Name = "m5_rate")]
		public float M5Rate { get; set; }

		/// <summary>
		/// MeanRate
		/// </summary>
		[DataMember(Name = "mean_rate")]
		public float MeanRate { get; set; }

		/// <summary>
		/// Units
		/// </summary>
		[DataMember(Name = "units")]
		public string Units { get; set; } = default!;
	}
}
