using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Timers
	/// </summary>
	[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]
	public class MetricsTimers
	{
		/// <summary>
		/// RundeckApiRequestsRequestTimer
		/// </summary>
		[JsonPropertyName("rundeck.api.requests.requestTimer")]
		public AdvancedMeter RundeckApiRequestsRequestTimer { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckQuartzJobsExecutionJobExecutionTimer
		/// </summary>
		[JsonPropertyName("rundeck.quartz.jobsExecutionJob.executionTimer")]
		public AdvancedMeter RundeckQuartzJobsExecutionJobExecutionTimer { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckQuartzjobsExecutionJobExecutionTimer
		/// </summary>
		[JsonPropertyName("rundeck.quartzjobs.ExecutionJob.executionTimer")]
		public AdvancedMeter RundeckQuartzjobsExecutionJobExecutionTimer { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesAuthorizationServiceGetSystemAuthorization
		/// </summary>
		[JsonPropertyName("rundeck.services.AuthorizationService.getSystemAuthorization")]
		public AdvancedMeter RundeckServicesAuthorizationServiceGetSystemAuthorization { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesAuthorizationServiceSystemAuthorizationEvaluateSetTimer
		/// </summary>
		[JsonPropertyName("rundeck.services.AuthorizationService.systemAuthorization.evaluateSetTimer")]
		public AdvancedMeter RundeckServicesAuthorizationServiceSystemAuthorizationEvaluateSetTimer { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesAuthorizationServiceSystemAuthorizationEvaluateTimer
		/// </summary>
		[JsonPropertyName("rundeck.services.AuthorizationService.systemAuthorization.evaluateTimer")]
		public AdvancedMeter RundeckServicesAuthorizationServiceSystemAuthorizationEvaluateTimer { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeApplicationResource
		/// </summary>
		[JsonPropertyName("rundeck.services.FrameworkService.authorizeApplicationResource")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeApplicationResource { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeApplicationResourceType
		/// </summary>
		[JsonPropertyName("rundeck.services.FrameworkService.authorizeApplicationResourceType")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeApplicationResourceType { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceFilterNodeSet
		/// </summary>
		[JsonPropertyName("rundeck.services.FrameworkService.filterNodeSet")]
		public AdvancedMeter RundeckServicesFrameworkServiceFilterNodeSet { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesNodeServiceProjectBingoLoadNodes
		/// </summary>
		[JsonPropertyName("rundeck.services.NodeService.projectbingo.loadNodes")]
		public AdvancedMeter RundeckServicesNodeServiceProjectBingoLoadNodes { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckWebRequestsRequestTimer
		/// </summary>
		[JsonPropertyName("rundeck.web.requests.requestTimer")]
		public AdvancedMeter RundeckWebRequestsRequestTimer { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckControllersMenuControllerApiExecutionsRunningv14QueryQueue
		/// </summary>
		[JsonPropertyName("rundeck.controllers.MenuController.apiExecutionsRunningv14.queryQueue")]
		public AdvancedMeter RundeckControllersMenuControllerApiExecutionsRunningv14QueryQueue { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckControllersMenuControllerLoadSummaryProjectStatsOrig
		/// </summary>
		[JsonPropertyName("rundeck.controllers.MenuController.loadSummaryProjectStatsOrig")]
		public AdvancedMeter RundeckControllersMenuControllerLoadSummaryProjectStatsOrig { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckControllersReportsControllerIndexGetExecutionReports
		/// </summary>
		[JsonPropertyName("rundeck.controllers.ReportsController.index.getExecutionReports")]
		public AdvancedMeter RundeckControllersReportsControllerIndexGetExecutionReports { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeApplicationResourceAll
		/// </summary>
		[JsonPropertyName("rundeck.services.FrameworkService.authorizeApplicationResourceAll")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeApplicationResourceAll { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeApplicationResourceSet
		/// </summary>
		[JsonPropertyName("rundeck.services.FrameworkService.authorizeApplicationResourceSet")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeApplicationResourceSet { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeApplicationResourceTypeAll
		/// </summary>
		[JsonPropertyName("rundeck.services.FrameworkService.authorizeApplicationResourceTypeAll")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeApplicationResourceTypeAll { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeProjectJobAll
		/// </summary>
		[JsonPropertyName("rundeck.services.FrameworkService.authorizeProjectJobAll")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeProjectJobAll { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeProjectResource
		/// </summary>
		[JsonPropertyName("rundeck.services.FrameworkService.authorizeProjectResource")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeProjectResource { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeProjectResourceAll
		/// </summary>
		[JsonPropertyName("rundeck.services.FrameworkService.authorizeProjectResourceAll")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeProjectResourceAll { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeProjectResourceAny
		/// </summary>
		[JsonPropertyName("rundeck.services.FrameworkService.authorizeProjectResourceAny")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeProjectResourceAny { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeProjectResources
		/// </summary>
		[JsonPropertyName("rundeck.services.FrameworkService.authorizeProjectResources")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeProjectResources { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesNodeServiceProjectTestLoadNodes
		/// </summary>
		[JsonPropertyName("rundeck.services.NodeService.project.Test.loadNodes")]
		public AdvancedMeter RundeckServicesNodeServiceProjectTestLoadNodes { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesNodeServiceProjectTestingLoadNodes
		/// </summary>
		[JsonPropertyName("rundeck.services.NodeService.project.Testing.loadNodes")]
		public AdvancedMeter RundeckServicesNodeServiceProjectTestingLoadNodes { get; set; } = new AdvancedMeter();

		/// <summary>
		/// Timers not named above; the names vary by server version, usage and project
		/// </summary>
		[JsonExtensionData]
		public IDictionary<string, object> AdditionalMetrics { get; set; } = new Dictionary<string, object>();
	}
}
