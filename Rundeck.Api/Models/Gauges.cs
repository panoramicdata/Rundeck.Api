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
		/// dataSourceconnectionpingTime
		/// </summary>
		[DataMember(Name = "dataSourceconnectionpingTime")]
		public IntValue dataSourceconnectionpingTime { get; set; }

		/// <summary>
		/// rundeckschedulerquartzrunningExecutions
		/// </summary>
		[DataMember(Name = "rundeckschedulerquartzrunningExecutions")]
		public IntValue rundeckschedulerquartzrunningExecutions { get; set; }

		/// <summary>
		/// rundeckservicesAuthorizationServicesourceCacheevictionCount
		/// </summary>
		[DataMember(Name = "rundeckservicesAuthorizationServicesourceCacheevictionCount")]
		public IntValue rundeckservicesAuthorizationServicesourceCacheevictionCount { get; set; }

		/// <summary>
		/// rundeckservicesAuthorizationServicesourceCachehitCount
		/// </summary>
		[DataMember(Name = "rundeckservicesAuthorizationServicesourceCachehitCount")]
		public IntValue rundeckservicesAuthorizationServicesourceCachehitCount { get; set; }

		/// <summary>
		/// rundeckservicesAuthorizationServicesourceCachehitRate
		/// </summary>
		[DataMember(Name = "rundeckservicesAuthorizationServicesourceCachehitRate")]
		public FloatValue rundeckservicesAuthorizationServicesourceCachehitRate { get; set; }

		/// <summary>
		/// rundeckservicesAuthorizationServicesourceCacheloadExceptionCount
		/// </summary>
		[DataMember(Name = "rundeckservicesAuthorizationServicesourceCacheloadExceptionCount")]
		public IntValue rundeckservicesAuthorizationServicesourceCacheloadExceptionCount { get; set; }

		/// <summary>
		/// rundeckservicesAuthorizationServicesourceCachemissCount
		/// </summary>
		[DataMember(Name = "rundeckservicesAuthorizationServicesourceCachemissCount")]
		public IntValue rundeckservicesAuthorizationServicesourceCachemissCount { get; set; }

		/// <summary>
		/// rundeckservicesNodeServicenodeCacheevictionCount
		/// </summary>
		[DataMember(Name = "rundeckservicesNodeServicenodeCacheevictionCount")]
		public IntValue rundeckservicesNodeServicenodeCacheevictionCount { get; set; }

		/// <summary>
		/// rundeckservicesNodeServicenodeCachehitCount
		/// </summary>
		[DataMember(Name = "rundeckservicesNodeServicenodeCachehitCount")]
		public IntValue rundeckservicesNodeServicenodeCachehitCount { get; set; }

		/// <summary>
		/// rundeckservicesNodeServicenodeCachehitRate
		/// </summary>
		[DataMember(Name = "rundeckservicesNodeServicenodeCachehitRate")]
		public FloatValue rundeckservicesNodeServicenodeCachehitRate { get; set; }

		/// <summary>
		/// rundeckservicesNodeServicenodeCacheloadExceptionCount
		/// </summary>
		[DataMember(Name = "rundeckservicesNodeServicenodeCacheloadExceptionCount")]
		public IntValue rundeckservicesNodeServicenodeCacheloadExceptionCount { get; set; }

		/// <summary>
		/// rundeckservicesNodeServicenodeCachemissCount
		/// </summary>
		[DataMember(Name = "rundeckservicesNodeServicenodeCachemissCount")]
		public IntValue rundeckservicesNodeServicenodeCachemissCount { get; set; }

		/// <summary>
		/// rundeckservicesProjectManagerServicefileCacheevictionCount
		/// </summary>
		[DataMember(Name = "rundeckservicesProjectManagerServicefileCacheevictionCount")]
		public IntValue rundeckservicesProjectManagerServicefileCacheevictionCount { get; set; }

		/// <summary>
		/// rundeckservicesProjectManagerServicefileCachehitCount
		/// </summary>
		[DataMember(Name = "rundeckservicesProjectManagerServicefileCachehitCount")]
		public IntValue rundeckservicesProjectManagerServicefileCachehitCount { get; set; }

		/// <summary>
		/// rundeckservicesProjectManagerServicefileCachehitRate
		/// </summary>
		[DataMember(Name = "rundeckservicesProjectManagerServicefileCachehitRate")]
		public IntValue rundeckservicesProjectManagerServicefileCachehitRate { get; set; }

		/// <summary>
		/// rundeckservicesProjectManagerServicefileCacheloadExceptionCount
		/// </summary>
		[DataMember(Name = "rundeckservicesProjectManagerServicefileCacheloadExceptionCount")]
		public IntValue rundeckservicesProjectManagerServicefileCacheloadExceptionCount { get; set; }

		/// <summary>
		/// rundeckservicesProjectManagerServicefileCachemissCount
		/// </summary>
		[DataMember(Name = "rundeckservicesProjectManagerServicefileCachemissCount")]
		public IntValue rundeckservicesProjectManagerServicefileCachemissCount { get; set; }

		/// <summary>
		/// rundeckservicesProjectManagerServiceprojectCacheevictionCount
		/// </summary>
		[DataMember(Name = "rundeckservicesProjectManagerServiceprojectCacheevictionCount")]
		public IntValue rundeckservicesProjectManagerServiceprojectCacheevictionCount { get; set; }

		/// <summary>
		/// rundeckservicesProjectManagerServiceprojectCachehitCount
		/// </summary>
		[DataMember(Name = "rundeckservicesProjectManagerServiceprojectCachehitCount")]
		public IntValue rundeckservicesProjectManagerServiceprojectCachehitCount { get; set; }

		/// <summary>
		/// rundeckservicesProjectManagerServiceprojectCachehitRate
		/// </summary>
		[DataMember(Name = "rundeckservicesProjectManagerServiceprojectCachehitRate")]
		public FloatValue rundeckservicesProjectManagerServiceprojectCachehitRate { get; set; }

		/// <summary>
		/// rundeckservicesProjectManagerServiceprojectCacheloadExceptionCount
		/// </summary>
		[DataMember(Name = "rundeckservicesProjectManagerServiceprojectCacheloadExceptionCount")]
		public IntValue rundeckservicesProjectManagerServiceprojectCacheloadExceptionCount { get; set; }

		/// <summary>
		/// rundeckservicesProjectManagerServiceprojectCachemissCount
		/// </summary>
		[DataMember(Name = "rundeckservicesProjectManagerServiceprojectCachemissCount")]
		public IntValue rundeckservicesProjectManagerServiceprojectCachemissCount { get; set; }

		/// <summary>
		/// rundeckservicesProjectManagerServicesourceCacheevictionCount
		/// </summary>
		[DataMember(Name = "rundeckservicesProjectManagerServicesourceCacheevictionCount")]
		public IntValue rundeckservicesProjectManagerServicesourceCacheevictionCount { get; set; }

		/// <summary>
		/// rundeckservicesProjectManagerServicesourceCachehitCount
		/// </summary>
		[DataMember(Name = "rundeckservicesProjectManagerServicesourceCachehitCount")]
		public IntValue rundeckservicesProjectManagerServicesourceCachehitCount { get; set; }

		/// <summary>
		/// rundeckservicesProjectManagerServicesourceCachehitRate
		/// </summary>
		[DataMember(Name = "rundeckservicesProjectManagerServicesourceCachehitRate")]
		public IntValue rundeckservicesProjectManagerServicesourceCachehitRate { get; set; }

		/// <summary>
		/// rundeckservicesProjectManagerServicesourceCacheloadExceptionCount
		/// </summary>
		[DataMember(Name = "rundeckservicesProjectManagerServicesourceCacheloadExceptionCount")]
		public IntValue rundeckservicesProjectManagerServicesourceCacheloadExceptionCount { get; set; }

		/// <summary>
		/// rundeckservicesProjectManagerServicesourceCachemissCount
		/// </summary>
		[DataMember(Name = "rundeckservicesProjectManagerServicesourceCachemissCount")]
		public IntValue rundeckservicesProjectManagerServicesourceCachemissCount { get; set; }
	}
}
