using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Scheduler
	/// </summary>
	[DataContract]
	public class Scheduler
	{
		/// <summary>
		/// Running
		/// </summary>
		[DataMember(Name = "running")]
		public int Running { get; set; }

		/// <summary>
		/// ThreadPoolSize
		/// </summary>
		[DataMember(Name = "threadPoolSize")]
		public int ThreadPoolSize { get; set; }
	}
}