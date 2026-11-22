using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// System Stats
	/// </summary>
	public class Stats
	{
		/// <summary>
		/// Uptime
		/// </summary>
		[JsonPropertyName("uptime")]
		public Uptime Uptime { get; set; } = new Uptime();

		/// <summary>
		/// CPU info
		/// </summary>
		[JsonPropertyName("cpu")]
		public CpuInfo Cpu { get; set; } = new CpuInfo();

		/// <summary>
		/// Memory info
		/// </summary>
		[JsonPropertyName("memory")]
		public MemoryInfo Memory { get; set; } = new MemoryInfo();

		/// <summary>
		/// Scheduler
		/// </summary>
		[JsonPropertyName("scheduler")]
		public Scheduler Scheduler { get; set; } = new Scheduler();

		/// <summary>
		/// Threads
		/// </summary>
		[JsonPropertyName("threads")]
		public Threads Threads { get; set; } = new Threads();
	}
}