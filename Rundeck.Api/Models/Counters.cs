using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Counters
	/// </summary>
	[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]
	public class Counters
	{
		/// <summary>
		/// RundeckSchedulerQuartzScheduledJobs
		/// </summary>
		[JsonPropertyName("rundeck.scheduler.quartz.scheduledJobs")]
		public Counter RundeckSchedulerQuartzScheduledJobs { get; set; } = new Counter();

		/// <summary>
		/// RundeckServicesLogFileStorageServiceStorageRequestsFailed
		/// </summary>
		[JsonPropertyName("rundeck.services.LogFileStorageService.storageRequests.failed")]
		public Counter RundeckServicesLogFileStorageServiceStorageRequestsFailed { get; set; } = new Counter();

		/// <summary>
		/// RundeckServicesLogFileStorageServiceStorageRequestsPartial
		/// </summary>
		[JsonPropertyName("rundeck.services.LogFileStorageService.storageRequests.partial")]
		public Counter RundeckServicesLogFileStorageServiceStorageRequestsPartial { get; set; } = new Counter();

		/// <summary>
		/// RundeckServicesLogFileStorageServiceStorageRequestsQueued
		/// </summary>
		[JsonPropertyName("rundeck.services.LogFileStorageService.storageRequests.queued")]
		public Counter RundeckServicesLogFileStorageServiceStorageRequestsQueued { get; set; } = new Counter();

		/// <summary>
		/// RundeckServicesLogFileStorageServiceStorageRequestsRetries
		/// </summary>
		[JsonPropertyName("rundeck.services.LogFileStorageService.storageRequests.retries")]
		public Counter RundeckServicesLogFileStorageServiceStorageRequestsRetries { get; set; } = new Counter();

		/// <summary>
		/// RundeckServicesLogFileStorageServiceStorageRequestsRunning
		/// </summary>
		[JsonPropertyName("rundeck.services.LogFileStorageService.storageRequests.running")]
		public Counter RundeckServicesLogFileStorageServiceStorageRequestsRunning { get; set; } = new Counter();

		/// <summary>
		/// RundeckServicesLogFileStorageServiceStorageRequestsSucceeded
		/// </summary>
		[JsonPropertyName("rundeck.services.LogFileStorageService.storageRequests.succeeded")]
		public Counter RundeckServicesLogFileStorageServiceStorageRequestsSucceeded { get; set; } = new Counter();

		/// <summary>
		/// RundeckServicesLogFileStorageServiceStorageRequestsTotal
		/// </summary>
		[JsonPropertyName("rundeck.services.LogFileStorageService.storageRequests.total")]
		public Counter RundeckServicesLogFileStorageServiceStorageRequestsTotal { get; set; } = new Counter();

		/// <summary>
		/// Counters not named above; the names vary by server version and usage
		/// </summary>
		[JsonExtensionData]
		public IDictionary<string, object> AdditionalMetrics { get; set; } = new Dictionary<string, object>();
	}
}
