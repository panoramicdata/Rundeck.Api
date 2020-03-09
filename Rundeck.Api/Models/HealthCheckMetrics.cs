using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Health check metrics
	/// </summary>
	[DataContract]
	public class HealthCheckMetrics
	{
		/// <summary>
		/// The DataSource connection time
		/// </summary>
		[DataMember(Name = "dataSource.connection.time")]
		public Health DataSourceConnectionTime { get; set; } = new Health();

		/// <summary>
		/// The Quartz scheduler thread pool
		/// </summary>
		[DataMember(Name = "quartz.scheduler.threadPool")]
		public Health QuartzSchedulerThreadPool { get; set; } = new Health();
	}
}
