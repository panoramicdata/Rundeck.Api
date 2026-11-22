using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Histograms
	/// </summary>
	[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]
	public class Histograms
	{
		/// <summary>
		/// Histograms by name; the names vary by server version and usage
		/// </summary>
		[JsonExtensionData]
		public IDictionary<string, object> AdditionalMetrics { get; set; } = new Dictionary<string, object>();
	}
}
