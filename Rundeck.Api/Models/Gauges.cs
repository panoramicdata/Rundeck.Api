using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Gauges
	/// </summary>
	[DataContract]
	public class Gauges
	{
		/// <summary>
		/// DataSourceConnectionPingTime
		/// </summary>
		[DataMember(Name = "dataSource.connection.pingTime")]
		public IntValue DataSourceConnectionPingTime { get; set; } = new IntValue();

		/// <summary>
		/// RundeckSchedulerQuartzRunningExecutions
		/// </summary>
		[DataMember(Name = "rundeck.scheduler.quartz.runningExecutions")]
		public IntValue RundeckSchedulerQuartzRunningExecutions { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesAuthorizationServicesourceCacheEvictionCount
		/// </summary>
		[DataMember(Name = "rundeck.services.AuthorizationService.sourceCache.evictionCount")]
		public IntValue RundeckServicesAuthorizationServicesourceCacheEvictionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesAuthorizationServiceSourceCacheHitCount
		/// </summary>
		[DataMember(Name = "rundeck.services.AuthorizationService.sourceCache.hitCount")]
		public IntValue RundeckServicesAuthorizationServiceSourceCacheHitCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesAuthorizationServiceSourceCacheHitRate
		/// </summary>
		[DataMember(Name = "rundeck.services.AuthorizationService.sourceCache.hitRate")]
		public FloatValue RundeckServicesAuthorizationServiceSourceCacheHitRate { get; set; } = new FloatValue();

		/// <summary>
		/// RundeckServicesAuthorizationServicesourceCacheLoadExceptionCount
		/// </summary>
		[DataMember(Name = "rundeck.services.AuthorizationService.sourceCache.loadExceptionCount")]
		public IntValue RundeckServicesAuthorizationServicesourceCacheLoadExceptionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesAuthorizationServiceSourceCacheMissCount
		/// </summary>
		[DataMember(Name = "rundeck.services.AuthorizationService.sourceCache.missCount")]
		public IntValue RundeckServicesAuthorizationServiceSourceCacheMissCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesNodeServiceNodeCacheEvictionCount
		/// </summary>
		[DataMember(Name = "rundeck.services.NodeService.nodeCache.evictionCount")]
		public IntValue RundeckServicesNodeServiceNodeCacheEvictionCount { get; set; } = new IntValue();

		/// <summary>
		/// rundeckservicesNodeServicenodeCachehitCount
		/// </summary>
		[DataMember(Name = "rundeck.services.NodeService.nodeCache.hitCount")]
		public IntValue RundeckServicesNodeServiceNodeCacheHitCount { get; set; } = new IntValue();

		/// <summary>
		/// rundeckservicesNodeServicenodeCachehitRate
		/// </summary>
		[DataMember(Name = "rundeck.services.NodeService.nodeCache.hitRate")]
		public FloatValue RundeckServicesNodeServiceNodeCacheHitRate { get; set; } = new FloatValue();

		/// <summary>
		/// RundeckServicesNodeServiceNodeCacheLoadExceptionCount
		/// </summary>
		[DataMember(Name = "rundeck.services.NodeService.nodeCache.loadExceptionCount")]
		public IntValue RundeckServicesNodeServiceNodeCacheLoadExceptionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesNodeServiceNodeCacheMissCount
		/// </summary>
		[DataMember(Name = "rundeck.services.NodeService.nodeCache.missCount")]
		public IntValue RundeckServicesNodeServiceNodeCacheMissCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceFileCacheEvictionCount
		/// </summary>
		[DataMember(Name = "rundeck.services.ProjectManagerService.fileCache.evictionCount")]
		public IntValue RundeckServicesProjectManagerServiceFileCacheEvictionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceFileCacheHitCount
		/// </summary>
		[DataMember(Name = "rundeck.services.ProjectManagerService.fileCache.hitCount")]
		public IntValue RundeckServicesProjectManagerServiceFileCacheHitCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceFileCacheHitRate
		/// </summary>
		[DataMember(Name = "rundeck.services.ProjectManagerService.fileCache.hitRate")]
		public FloatValue RundeckServicesProjectManagerServiceFileCacheHitRate { get; set; } = new FloatValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceFileCacheLoadExceptionCount
		/// </summary>
		[DataMember(Name = "rundeck.services.ProjectManagerService.fileCache.loadExceptionCount")]
		public IntValue RundeckServicesProjectManagerServiceFileCacheLoadExceptionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceFileCacheMissCount
		/// </summary>
		[DataMember(Name = "rundeck.services.ProjectManagerService.fileCache.missCount")]
		public IntValue RundeckServicesProjectManagerServiceFileCacheMissCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceProjectCacheEvictionCount
		/// </summary>
		[DataMember(Name = "rundeck.services.ProjectManagerService.projectCache.evictionCount")]
		public IntValue RundeckServicesProjectManagerServiceProjectCacheEvictionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceProjectCacheHitCount
		/// </summary>
		[DataMember(Name = "rundeck.services.ProjectManagerService.projectCache.hitCount")]
		public IntValue RundeckServicesProjectManagerServiceProjectCacheHitCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceProjectCacheHitRate
		/// </summary>
		[DataMember(Name = "rundeck.services.ProjectManagerService.projectCache.hitRate")]
		public FloatValue RundeckServicesProjectManagerServiceProjectCacheHitRate { get; set; } = new FloatValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceProjectCacheLoadExceptionCount
		/// </summary>
		[DataMember(Name = "rundeck.services.ProjectManagerService.projectCache.loadExceptionCount")]
		public IntValue RundeckServicesProjectManagerServiceProjectCacheLoadExceptionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceProjectCacheMissCount
		/// </summary>
		[DataMember(Name = "rundeck.services.ProjectManagerService.projectCache.missCount")]
		public IntValue RundeckServicesProjectManagerServiceProjectCacheMissCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceSourceCacheEvictionCount
		/// </summary>
		[DataMember(Name = "rundeck.services.ProjectManagerService.sourceCache.evictionCount")]
		public IntValue RundeckServicesProjectManagerServiceSourceCacheEvictionCount { get; set; } = new IntValue();

		/// <summary>
		/// rundeckservicesProjectManagerServicesourceCachehitCount
		/// </summary>
		[DataMember(Name = "rundeck.services.ProjectManagerService.sourceCache.hitCount")]
		public IntValue RundeckServicesProjectManagerServiceSourceCacheHitCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceSourceCacheHitRate
		/// </summary>
		[DataMember(Name = "rundeck.services.ProjectManagerService.sourceCache.hitRate")]
		public FloatValue RundeckServicesProjectManagerServiceSourceCacheHitRate { get; set; } = new FloatValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceSourceCacheLoadExceptionCount
		/// </summary>
		[DataMember(Name = "rundeck.services.ProjectManagerService.sourceCache.loadExceptionCount")]
		public IntValue RundeckServicesProjectManagerServiceSourceCacheLoadExceptionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceSourceCacheMissCount
		/// </summary>
		[DataMember(Name = "rundeck.services.ProjectManagerService.sourceCache.missCount")]
		public IntValue RundeckServicesProjectManagerServiceSourceCacheMissCount { get; set; } = new IntValue();
	}
}
