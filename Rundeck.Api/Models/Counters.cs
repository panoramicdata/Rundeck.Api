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

		/// <summary>
		/// RundeckServicesLogFileStorageServiceStorageRequestsFailed
		/// </summary>
		[DataMember(Name = "rundeck.services.LogFileStorageService.storageRequests.failed")]
		public Counter RundeckServicesLogFileStorageServiceStorageRequestsFailed { get; set; } = new Counter();

		/// <summary>
		/// RundeckServicesLogFileStorageServiceStorageRequestsPartial
		/// </summary>
		[DataMember(Name = "rundeck.services.LogFileStorageService.storageRequests.partial")]
		public Counter RundeckServicesLogFileStorageServiceStorageRequestsPartial { get; set; } = new Counter();

		/// <summary>
		/// RundeckServicesLogFileStorageServiceStorageRequestsQueued
		/// </summary>
		[DataMember(Name = "rundeck.services.LogFileStorageService.storageRequests.queued")]
		public Counter RundeckServicesLogFileStorageServiceStorageRequestsQueued { get; set; } = new Counter();

		/// <summary>
		/// RundeckServicesLogFileStorageServiceStorageRequestsRetries
		/// </summary>
		[DataMember(Name = "rundeck.services.LogFileStorageService.storageRequests.retries")]
		public Counter RundeckServicesLogFileStorageServiceStorageRequestsRetries { get; set; } = new Counter();

		/// <summary>
		/// RundeckServicesLogFileStorageServiceStorageRequestsRunning
		/// </summary>
		[DataMember(Name = "rundeck.services.LogFileStorageService.storageRequests.running")]
		public Counter RundeckServicesLogFileStorageServiceStorageRequestsRunning { get; set; } = new Counter();

		/// <summary>
		/// RundeckServicesLogFileStorageServiceStorageRequestsSucceeded
		/// </summary>
		[DataMember(Name = "rundeck.services.LogFileStorageService.storageRequests.succeeded")]
		public Counter RundeckServicesLogFileStorageServiceStorageRequestsSucceeded { get; set; } = new Counter();

		/// <summary>
		/// RundeckServicesLogFileStorageServiceStorageRequestsTotal
		/// </summary>
		[DataMember(Name = "rundeck.services.LogFileStorageService.storageRequests.total")]
		public Counter RundeckServicesLogFileStorageServiceStorageRequestsTotal { get; set; } = new Counter();

	}
}
