using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Counters
	/// </summary>
	[DataContract]
	public class Counters
	{
		/// <summary>
		/// RundeckSchedulerQuartzScheduledJobs
		/// </summary>
		[DataMember(Name = "rundeck.scheduler.quartz.scheduledJobs")]
		public Counter RundeckSchedulerQuartzScheduledJobs { get; set; } = new Counter();
	}
}
