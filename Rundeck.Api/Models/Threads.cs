using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Threads
	/// </summary>
	[DataContract]
	public class Threads
	{
		/// <summary>
		/// Active
		/// </summary>
		[DataMember(Name = "active")]
		public int Active { get; set; }
	}
}