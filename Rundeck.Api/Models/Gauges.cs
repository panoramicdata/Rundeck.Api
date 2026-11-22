using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Gauges
	/// </summary>
	[JsonUnmappedMemberHandling(JsonUnmappedMemberHandling.Skip)]
	public class Gauges
	{
		/// <summary>
		/// DataSourceConnectionPingTime
		/// </summary>
		[JsonPropertyName("dataSource.connection.pingTime")]
		public IntValue DataSourceConnectionPingTime { get; set; } = new IntValue();

		/// <summary>
		/// RundeckSchedulerQuartzRunningExecutions
		/// </summary>
		[JsonPropertyName("rundeck.scheduler.quartz.runningExecutions")]
		public IntValue RundeckSchedulerQuartzRunningExecutions { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesAuthorizationServicesourceCacheEvictionCount
		/// </summary>
		[JsonPropertyName("rundeck.services.AuthorizationService.sourceCache.evictionCount")]
		public IntValue RundeckServicesAuthorizationServicesourceCacheEvictionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesAuthorizationServiceSourceCacheHitCount
		/// </summary>
		[JsonPropertyName("rundeck.services.AuthorizationService.sourceCache.hitCount")]
		public IntValue RundeckServicesAuthorizationServiceSourceCacheHitCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesAuthorizationServiceSourceCacheHitRate
		/// </summary>
		[JsonPropertyName("rundeck.services.AuthorizationService.sourceCache.hitRate")]
		public FloatValue RundeckServicesAuthorizationServiceSourceCacheHitRate { get; set; } = new FloatValue();

		/// <summary>
		/// RundeckServicesAuthorizationServicesourceCacheLoadExceptionCount
		/// </summary>
		[JsonPropertyName("rundeck.services.AuthorizationService.sourceCache.loadExceptionCount")]
		public IntValue RundeckServicesAuthorizationServicesourceCacheLoadExceptionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesAuthorizationServiceSourceCacheMissCount
		/// </summary>
		[JsonPropertyName("rundeck.services.AuthorizationService.sourceCache.missCount")]
		public IntValue RundeckServicesAuthorizationServiceSourceCacheMissCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesNodeServiceNodeCacheEvictionCount
		/// </summary>
		[JsonPropertyName("rundeck.services.NodeService.nodeCache.evictionCount")]
		public IntValue RundeckServicesNodeServiceNodeCacheEvictionCount { get; set; } = new IntValue();

		/// <summary>
		/// rundeckservicesNodeServicenodeCachehitCount
		/// </summary>
		[JsonPropertyName("rundeck.services.NodeService.nodeCache.hitCount")]
		public IntValue RundeckServicesNodeServiceNodeCacheHitCount { get; set; } = new IntValue();

		/// <summary>
		/// rundeckservicesNodeServicenodeCachehitRate
		/// </summary>
		[JsonPropertyName("rundeck.services.NodeService.nodeCache.hitRate")]
		public FloatValue RundeckServicesNodeServiceNodeCacheHitRate { get; set; } = new FloatValue();

		/// <summary>
		/// RundeckServicesNodeServiceNodeCacheLoadExceptionCount
		/// </summary>
		[JsonPropertyName("rundeck.services.NodeService.nodeCache.loadExceptionCount")]
		public IntValue RundeckServicesNodeServiceNodeCacheLoadExceptionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesNodeServiceNodeCacheMissCount
		/// </summary>
		[JsonPropertyName("rundeck.services.NodeService.nodeCache.missCount")]
		public IntValue RundeckServicesNodeServiceNodeCacheMissCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceFileCacheEvictionCount
		/// </summary>
		[JsonPropertyName("rundeck.services.ProjectManagerService.fileCache.evictionCount")]
		public IntValue RundeckServicesProjectManagerServiceFileCacheEvictionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceFileCacheHitCount
		/// </summary>
		[JsonPropertyName("rundeck.services.ProjectManagerService.fileCache.hitCount")]
		public IntValue RundeckServicesProjectManagerServiceFileCacheHitCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceFileCacheHitRate
		/// </summary>
		[JsonPropertyName("rundeck.services.ProjectManagerService.fileCache.hitRate")]
		public FloatValue RundeckServicesProjectManagerServiceFileCacheHitRate { get; set; } = new FloatValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceFileCacheLoadExceptionCount
		/// </summary>
		[JsonPropertyName("rundeck.services.ProjectManagerService.fileCache.loadExceptionCount")]
		public IntValue RundeckServicesProjectManagerServiceFileCacheLoadExceptionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceFileCacheMissCount
		/// </summary>
		[JsonPropertyName("rundeck.services.ProjectManagerService.fileCache.missCount")]
		public IntValue RundeckServicesProjectManagerServiceFileCacheMissCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceProjectCacheEvictionCount
		/// </summary>
		[JsonPropertyName("rundeck.services.ProjectManagerService.projectCache.evictionCount")]
		public IntValue RundeckServicesProjectManagerServiceProjectCacheEvictionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceProjectCacheHitCount
		/// </summary>
		[JsonPropertyName("rundeck.services.ProjectManagerService.projectCache.hitCount")]
		public IntValue RundeckServicesProjectManagerServiceProjectCacheHitCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceProjectCacheHitRate
		/// </summary>
		[JsonPropertyName("rundeck.services.ProjectManagerService.projectCache.hitRate")]
		public FloatValue RundeckServicesProjectManagerServiceProjectCacheHitRate { get; set; } = new FloatValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceProjectCacheLoadExceptionCount
		/// </summary>
		[JsonPropertyName("rundeck.services.ProjectManagerService.projectCache.loadExceptionCount")]
		public IntValue RundeckServicesProjectManagerServiceProjectCacheLoadExceptionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceProjectCacheMissCount
		/// </summary>
		[JsonPropertyName("rundeck.services.ProjectManagerService.projectCache.missCount")]
		public IntValue RundeckServicesProjectManagerServiceProjectCacheMissCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceSourceCacheEvictionCount
		/// </summary>
		[JsonPropertyName("rundeck.services.ProjectManagerService.sourceCache.evictionCount")]
		public IntValue RundeckServicesProjectManagerServiceSourceCacheEvictionCount { get; set; } = new IntValue();

		/// <summary>
		/// rundeckservicesProjectManagerServicesourceCachehitCount
		/// </summary>
		[JsonPropertyName("rundeck.services.ProjectManagerService.sourceCache.hitCount")]
		public IntValue RundeckServicesProjectManagerServiceSourceCacheHitCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceSourceCacheHitRate
		/// </summary>
		[JsonPropertyName("rundeck.services.ProjectManagerService.sourceCache.hitRate")]
		public FloatValue RundeckServicesProjectManagerServiceSourceCacheHitRate { get; set; } = new FloatValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceSourceCacheLoadExceptionCount
		/// </summary>
		[JsonPropertyName("rundeck.services.ProjectManagerService.sourceCache.loadExceptionCount")]
		public IntValue RundeckServicesProjectManagerServiceSourceCacheLoadExceptionCount { get; set; } = new IntValue();

		/// <summary>
		/// RundeckServicesProjectManagerServiceSourceCacheMissCount
		/// </summary>
		[JsonPropertyName("rundeck.services.ProjectManagerService.sourceCache.missCount")]
		public IntValue RundeckServicesProjectManagerServiceSourceCacheMissCount { get; set; } = new IntValue();

		/// <summary>
		/// Gauges not named above; the names vary by server version and usage
		/// </summary>
		[JsonExtensionData]
		public IDictionary<string, object> AdditionalMetrics { get; set; } = new Dictionary<string, object>();
	}
}
