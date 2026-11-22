using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Health check metrics
	/// </summary>
	public class HealthCheckMetrics
	{
		/// <summary>
		/// The DataSource connection time
		/// </summary>
		[JsonPropertyName("dataSource.connection.time")]
		public Health DataSourceConnectionTime { get; set; } = new Health();

		/// <summary>
		/// The Quartz scheduler thread pool
		/// </summary>
		[JsonPropertyName("quartz.scheduler.threadPool")]
		public Health QuartzSchedulerThreadPool { get; set; } = new Health();

		/// <summary>
		/// Overall Rundeck health, reported by Rundeck 5 and later in place of the checks above
		/// </summary>
		[JsonPropertyName("rundeck")]
		public Health Rundeck { get; set; } = new Health();
	}
}
