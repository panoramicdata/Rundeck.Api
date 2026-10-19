using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Histograms
	/// </summary>
	[DataContract]
	[JsonObject(MemberSerialization.OptIn, MissingMemberHandling = MissingMemberHandling.Ignore)]
	public class Histograms
	{
		/// <summary>
		/// Histograms by name; the names vary by server version and usage
		/// </summary>
		[JsonExtensionData]
		public IDictionary<string, object> AdditionalMetrics { get; set; } = new Dictionary<string, object>();
	}
}
