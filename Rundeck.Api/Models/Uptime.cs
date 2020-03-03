using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Uptime
	/// </summary>
	[DataContract]
	public class Uptime
	{
		/// <summary>
		/// Duration
		/// </summary>
		[DataMember(Name = "duration")]
		public int Duration { get; set; }

		/// <summary>
		/// Unit
		/// </summary>
		[DataMember(Name = "unit")]
		public string Unit { get; set; } = default!;

		/// <summary>
		/// Uptime since in SystemTimestamp
		/// </summary>
		[DataMember(Name = "since")]
		public SystemTimestamp Since { get; set; } = new SystemTimestamp();
	}
}