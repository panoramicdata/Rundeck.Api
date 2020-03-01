using System.Runtime.Serialization;

namespace Rundeck.Api.Models
{

	/// <summary>
	/// Meters
	/// </summary>
	[DataContract]
	public class Meters
	{
		/// <summary>
		/// rundeckservicesAuthorizationServicesystemAuthorizationevaluateMeter
		/// </summary>
		[DataMember(Name = "rundeckservicesAuthorizationServicesystemAuthorizationevaluateMeter")]
		public Meter rundeckservicesAuthorizationServicesystemAuthorizationevaluateMeter { get; set; }

		/// <summary>
		/// rundeckservicesAuthorizationServicesystemAuthorizationevaluateSetMeter
		/// </summary>
		[DataMember(Name = "rundeckservicesAuthorizationServicesystemAuthorizationevaluateSetMeter")]
		public Meter rundeckservicesAuthorizationServicesystemAuthorizationevaluateSetMeter { get; set; }

		/// <summary>
		/// rundeckservicesExecutionServiceexecutionJobStartMeter
		/// </summary>
		[DataMember(Name = "rundeckservicesExecutionServiceexecutionJobStartMeter")]
		public Meter rundeckservicesExecutionServiceexecutionJobStartMeter { get; set; }

		/// <summary>
		/// rundeckservicesExecutionServiceexecutionStartMeter
		/// </summary>
		[DataMember(Name = "rundeckservicesExecutionServiceexecutionStartMeter")]
		public Meter rundeckservicesExecutionServiceexecutionStartMeter { get; set; }

		/// <summary>
		/// rundeckservicesExecutionServiceexecutionSuccessMeter
		/// </summary>
		[DataMember(Name = "rundeckservicesExecutionServiceexecutionSuccessMeter")]
		public Meter rundeckservicesExecutionServiceexecutionSuccessMeter { get; set; }
	}
}
