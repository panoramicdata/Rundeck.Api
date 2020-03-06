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

		/// <summary>
		/// RundeckControllersMenuControllerApiExecutionsRunningv14QueryQueue
		/// </summary>
		[DataMember(Name = "rundeck.controllers.MenuController.apiExecutionsRunningv14.queryQueue")]
		public AdvancedMeter RundeckControllersMenuControllerApiExecutionsRunningv14QueryQueue { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckControllersMenuControllerLoadSummaryProjectStatsOrig
		/// </summary>
		[DataMember(Name = "rundeck.controllers.MenuController.loadSummaryProjectStatsOrig")]
		public AdvancedMeter RundeckControllersMenuControllerLoadSummaryProjectStatsOrig { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckControllersReportsControllerIndexGetExecutionReports
		/// </summary>
		[DataMember(Name = "rundeck.controllers.ReportsController.index.getExecutionReports")]
		public AdvancedMeter RundeckControllersReportsControllerIndexGetExecutionReports { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeApplicationResourceAll
		/// </summary>
		[DataMember(Name = "rundeck.services.FrameworkService.authorizeApplicationResourceAll")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeApplicationResourceAll { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeApplicationResourceSet
		/// </summary>
		[DataMember(Name = "rundeck.services.FrameworkService.authorizeApplicationResourceSet")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeApplicationResourceSet { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeApplicationResourceTypeAll
		/// </summary>
		[DataMember(Name = "rundeck.services.FrameworkService.authorizeApplicationResourceTypeAll")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeApplicationResourceTypeAll { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeProjectJobAll
		/// </summary>
		[DataMember(Name = "rundeck.services.FrameworkService.authorizeProjectJobAll")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeProjectJobAll { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeProjectResource
		/// </summary>
		[DataMember(Name = "rundeck.services.FrameworkService.authorizeProjectResource")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeProjectResource { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeProjectResourceAll
		/// </summary>
		[DataMember(Name = "rundeck.services.FrameworkService.authorizeProjectResourceAll")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeProjectResourceAll { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeProjectResourceAny
		/// </summary>
		[DataMember(Name = "rundeck.services.FrameworkService.authorizeProjectResourceAny")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeProjectResourceAny { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesFrameworkServiceAuthorizeProjectResources
		/// </summary>
		[DataMember(Name = "rundeck.services.FrameworkService.authorizeProjectResources")]
		public AdvancedMeter RundeckServicesFrameworkServiceAuthorizeProjectResources { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesNodeServiceProjectTestLoadNodes
		/// </summary>
		[DataMember(Name = "rundeck.services.NodeService.project.Test.loadNodes")]
		public AdvancedMeter RundeckServicesNodeServiceProjectTestLoadNodes { get; set; } = new AdvancedMeter();

		/// <summary>
		/// RundeckServicesNodeServiceProjectTestingLoadNodes
		/// </summary>
		[DataMember(Name = "rundeck.services.NodeService.project.Testing.loadNodes")]
		public AdvancedMeter RundeckServicesNodeServiceProjectTestingLoadNodes { get; set; } = new AdvancedMeter();
	}
}
