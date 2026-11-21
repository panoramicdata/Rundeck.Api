using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Metrics
	/// </summary>
	[SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "Existing public API name")]
	public class Metrics
	{
		/// <summary>
		/// The links
		/// </summary>
		[JsonPropertyName("_links")]
		public MetricLinks Links { get; set; } = default!;
	}
}
