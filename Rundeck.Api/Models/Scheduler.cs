using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Scheduler
	/// </summary>
	public class Scheduler
	{
		/// <summary>
		/// Running
		/// </summary>
		[JsonPropertyName("running")]
		public int Running { get; set; }

		/// <summary>
		/// ThreadPoolSize
		/// </summary>
		[JsonPropertyName("threadPoolSize")]
		public int ThreadPoolSize { get; set; }
	}
}