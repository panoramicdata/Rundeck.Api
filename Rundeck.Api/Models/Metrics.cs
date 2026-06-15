using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Metrics
	/// </summary>
	[DataContract]
	[SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "Existing public API name")]
	public class Metrics
	{
		/// <summary>
		/// The links
		/// </summary>
		[DataMember(Name = "_links")]
		public MetricLinks Links { get; set; } = default!;
	}
}
