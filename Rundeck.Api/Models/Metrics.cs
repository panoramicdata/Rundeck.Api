using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{
	/// <summary>
	/// Metrics
	/// </summary>
	[DataContract]
	public class Metrics
	{
		/// <summary>
		/// The links
		/// </summary>
		[DataMember(Name = "_links")]
		public Links Links { get; set; } = default!;
	}

	/// <summary>
	/// Timers
	/// </summary>
	[DataContract]
	public class Timers
	{
		/// <summary>
		/// rundeckapirequestsrequestTimer
		/// </summary>
		[DataMember(Name = "rundeckapirequestsrequestTimer")]
		public AdvancedMeter rundeckapirequestsrequestTimer { get; set; }

		/// <summary>
		/// rundeckquartzjobsExecutionJobexecutionTimer
		/// </summary>
		[DataMember(Name = "rundeckquartzjobsExecutionJobexecutionTimer")]
		public AdvancedMeter rundeckquartzjobsExecutionJobexecutionTimer { get; set; }

		/// <summary>
		/// rundeckservicesAuthorizationServicegetSystemAuthorization
		/// </summary>
		[DataMember(Name = "rundeckservicesAuthorizationServicegetSystemAuthorization")]
		public AdvancedMeter rundeckservicesAuthorizationServicegetSystemAuthorization { get; set; }

		/// <summary>
		/// rundeckservicesAuthorizationServicesystemAuthorizationevaluateSetTimer
		/// </summary>
		[DataMember(Name = "rundeckservicesAuthorizationServicesystemAuthorizationevaluateSetTimer")]
		public AdvancedMeter rundeckservicesAuthorizationServicesystemAuthorizationevaluateSetTimer { get; set; }

		/// <summary>
		/// XXXXXXXXX
		/// </summary>
		[DataMember(Name = "rundeckservicesAuthorizationServicesystemAuthorizationevaluateTimer")]
		public AdvancedMeter rundeckservicesAuthorizationServicesystemAuthorizationevaluateTimer { get; set; }

		/// <summary>
		/// XXXXXXXXX
		/// </summary>
		[DataMember(Name = "rundeckservicesFrameworkServiceauthorizeApplicationResource")]
		public AdvancedMeter rundeckservicesFrameworkServiceauthorizeApplicationResource { get; set; }

		/// <summary>
		/// XXXXXXXXX
		/// </summary>
		[DataMember(Name = "rundeckservicesFrameworkServicefilterNodeSet")]
		public AdvancedMeter rundeckservicesFrameworkServicefilterNodeSet { get; set; }

		/// <summary>
		/// XXXXXXXXX
		/// </summary>
		[DataMember(Name = "rundeckservicesNodeServiceprojectbingoloadNodes")]
		public AdvancedMeter rundeckservicesNodeServiceprojectbingoloadNodes { get; set; }

		/// <summary>
		/// XXXXXXXXX
		/// </summary>
		[DataMember(Name = "rundeckwebrequestsrequestTimer")]
		public AdvancedMeter rundeckwebrequestsrequestTimer { get; set; }
	}
}
