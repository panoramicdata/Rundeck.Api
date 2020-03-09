using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// A health status
	/// </summary>
	[DataContract]
	public class Health
	{
		/// <summary>
		/// Whether the health is healthy
		/// </summary>
		[DataMember(Name = "healthy")]
		public bool Healthy { get; set; }

		/// <summary>
		/// The message
		/// </summary>
		[DataMember(Name = "message")]
		public string Message { get; set; } = string.Empty;
	}
}
