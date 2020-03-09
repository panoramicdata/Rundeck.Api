using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// System Stats
	/// </summary>
	[DataContract]
	public class Stats
	{
		/// <summary>
		/// Uptime
		/// </summary>
		[DataMember(Name = "uptime")]
		public Uptime Uptime { get; set; } = new Uptime();

		/// <summary>
		/// CPU info
		/// </summary>
		[DataMember(Name = "cpu")]
		public CpuInfo Cpu { get; set; } = new CpuInfo();

		/// <summary>
		/// Memory info
		/// </summary>
		[DataMember(Name = "memory")]
		public MemoryInfo Memory { get; set; } = new MemoryInfo();

		/// <summary>
		/// Scheduler
		/// </summary>
		[DataMember(Name = "scheduler")]
		public Scheduler Scheduler { get; set; } = new Scheduler();

		/// <summary>
		/// Threads
		/// </summary>
		[DataMember(Name = "threads")]
		public Threads Threads { get; set; } = new Threads();
	}
}