using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// The links
	/// </summary>
	[DataContract]
	public class Links
	{
		/// <summary>
		/// The metrics
		/// </summary>
		[DataMember(Name = "metrics")]
		public Link Metrics { get; set; } = default!;

		/// <summary>
		/// The ping
		/// </summary>
		[DataMember(Name = "ping")]
		public Link Ping { get; set; } = default!;

		/// <summary>
		/// The threads
		/// </summary>
		[DataMember(Name = "threads")]
		public Link Threads { get; set; } = default!;

		/// <summary>
		/// The healthcheck
		/// </summary>
		[DataMember(Name = "healthcheck")]
		public Link Healthcheck { get; set; } = default!;
	}
}
