using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Metrics
	/// </summary>
	[DataContract]
	public class Metrics
	{
		/// <summary>
		/// The links
		/// </summary>
		[DataMember(Name = "_links")]
		public MetricLinks Links { get; set; } = default!;
	}
}
