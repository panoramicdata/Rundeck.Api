using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Timers
	/// </summary>
	[DataContract]
	public class MetricsTimers
	{
		/// <summary>
		/// RundeckApiRequestsRequestTimer
		/// </summary>
		[DataMember(Name = "rundeck.api.requests.requestTimer")]
		public AdvancedMeter RundeckApiRequestsRequestTimer { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckQuartzJobsExecutionJobExecutionTimer
		/// </summary>
		[DataMember(Name = "rundeck.quartz.jobsExecutionJob.executionTimer")]
		public AdvancedMeter RundeckQuartzJobsExecutionJobExecutionTimer { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesAuthorizationServiceGetSystemAuthorization
		/// </summary>
		[DataMember(Name = "rundeck.services.AuthorizationService.getSystemAuthorization")]
		public AdvancedMeter RundeckServicesAuthorizationServiceGetSystemAuthorization { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesAuthorizationServiceSystemAuthorizationEvaluateSetTimer
		/// </summary>
		[DataMember(Name = "rundeck.services.AuthorizationService.systemAuthorization.evaluateSetTimer")]
		public AdvancedMeter RundeckServicesAuthorizationServiceSystemAuthorizationEvaluateSetTimer { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesAuthorizationServiceSystemAuthorizationEvaluateTimer
		/// </summary>
		[DataMember(Name = "rundeck.services.AuthorizationService.systemAuthorization.evaluateTimer")]
		public AdvancedMeter RundeckServicesAuthorizationServiceSystemAuthorizationEvaluateTimer { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeApplicationResource
		/// </summary>
		[DataMember(Name = "rundeck.services.FrameworkService.authorizeApplicationResource")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeApplicationResource { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeApplicationResourceType
		/// </summary>
		[DataMember(Name = "rundeck.services.FrameworkService.authorizeApplicationResourceType")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeApplicationResourceType { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceFilterNodeSet
		/// </summary>
		[DataMember(Name = "rundeck.services.FrameworkService.filterNodeSet")]
		public AdvancedMeter RundeckServicesFrameworkServiceFilterNodeSet { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesNodeServiceProjectBingoLoadNodes
		/// </summary>
		[DataMember(Name = "rundeck.services.NodeService.projectbingo.loadNodes")]
		public AdvancedMeter RundeckServicesNodeServiceProjectBingoLoadNodes { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckWebRequestsRequestTimer
		/// </summary>
		[DataMember(Name = "rundeck.web.requests.requestTimer")]
		public AdvancedMeter RundeckWebRequestsRequestTimer { get; set; } = new AdvancedMeter();
	}
}
